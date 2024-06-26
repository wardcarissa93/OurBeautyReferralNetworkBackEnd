﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBitcoin.Protocol;
using OurBeautyReferralNetwork.Data;
using OurBeautyReferralNetwork.DataTransferObjects;
using OurBeautyReferralNetwork.Models;
using OurBeautyReferralNetwork.Utilities;
using System.Diagnostics;


namespace OurBeautyReferralNetwork.Repositories
{
    public class CustomerRepo
    {
        private readonly JWTUtilities _jwtUtilities;
        private readonly obrnDbContext _obrnDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ReferralRepo _referralRepo;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserRepo _userRepo;
        private readonly ApplicationDbContext _context;

        public CustomerRepo(JWTUtilities jwtUtilities,
                            obrnDbContext obrnDbContext,
                            UserManager<IdentityUser> userManager,
                            ReferralRepo referralRepo,
                            SignInManager<IdentityUser> signInManager,
                            UserRepo userRepo,
                            ApplicationDbContext context)
        {
            _jwtUtilities = jwtUtilities;
            _obrnDbContext = obrnDbContext;
            _userManager = userManager;
            _referralRepo = referralRepo;
            _signInManager = signInManager;
            _userRepo = userRepo;
            _context = context;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            IEnumerable<Customer> customers = _obrnDbContext.Customers.ToList();
            return customers;
        }

        public async Task<IActionResult> GetCustomerById(string customerId)
        {
            try
            {
                Customer customer = await _obrnDbContext.Customers.FirstOrDefaultAsync(c => c.PkCustomerId == customerId);
                if (customer != null)
                {
                    return new OkObjectResult(customer);
                }
                else
                {
                    return new NotFoundObjectResult("Customer not found");
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error getting customer: {ex.Message}");
            }
        }

        public async Task<IActionResult> GetCustomerByEmail(string email)
        {
            try
            {
                Customer customer = await _obrnDbContext.Customers.FirstOrDefaultAsync(c => c.Email == email);
                if (customer != null)
                {
                    return new OkObjectResult(customer);
                }
                else
                {
                    return new NotFoundObjectResult("Customer not found");
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error getting customer: {ex.Message}");
            }
        }

        public async Task<IActionResult> AddCustomer(RegisterCustomerDTO customer)
        {
            try
            {
                using (var dbContext = new obrnDbContext())
                {
                    var isCustomerUsernameAvailable = await IsCustomerUsernameAvailable(customer.PkCustomerId);
                    if (!isCustomerUsernameAvailable)
                    {
                        return new BadRequestObjectResult("Username unavailable. Please enter a different username.");
                    }

                    Customer newCustomer = CreateNewCustomer(customer);
                    dbContext.Customers.Add(newCustomer);
                    await dbContext.SaveChangesAsync();
                    Console.WriteLine("New customer added");

                    var user = await _userRepo.AddNewUser(customer.PkCustomerId, customer.Email, customer.Password, "customer");

                    var token = await _jwtUtilities.GenerateJwtToken(customer.Email);

                    var referralResult = await HandleCustomerReferral(customer, user);
                    if (referralResult is OkObjectResult referralOkResult)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        Console.WriteLine("User logged in");
                        return new OkObjectResult(new { Message = "Customer added successfully", Token = token, ReferralId = referralOkResult.Value });
                    }
                    return referralResult;
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error adding customer: {ex.Message}");
            }
        }

        public async Task<IActionResult> Login(User model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                // Login successful, generate JWT token
                var token = _jwtUtilities.GenerateJwtToken(user.Email);

                return new OkObjectResult(new { Message = "Login successful", Token = token });
            }

            return new BadRequestObjectResult(new { Message = "Invalid email or password" });
        }

        public async Task<IActionResult> EditCustomer(EditCustomerDTO customer)
        {
            try
            {
                // Check if the customer exists in the database
                Customer existingCustomer = await _obrnDbContext.Customers.FirstOrDefaultAsync(c => c.Email == customer.Email);
                if (existingCustomer == null)
                {
                    return new NotFoundObjectResult("Customer not found");
                }

                // Update the customer's properties
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.Address = customer.Address;
                existingCustomer.City = customer.City;
                existingCustomer.Province = customer.Province;
                existingCustomer.PostalCode = customer.PostalCode;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.Birthdate = customer.Birthdate;
                //existingCustomer.Email = customer.Email;
                existingCustomer.Vip = customer.Vip;
                existingCustomer.Photo = customer.Photo;

                // Save changes to the database
                await _obrnDbContext.SaveChangesAsync();

                return new OkObjectResult("Customer updated successfully");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error editing customer: {ex.Message}");
            }
        }

        public async Task<IActionResult> UpdatePassword(EditPasswordDTO password)
        {
            try
            {
                // Find the user by ID
                var user = await _userManager.FindByIdAsync(password.UserId);
                if (user == null)
                {
                    return new NotFoundObjectResult("User not found");
                }

                // Check if the current password matches
                var passwordCheck = await _userManager.CheckPasswordAsync(user, password.CurrentPassword);
                if (!passwordCheck)
                {
                    return new BadRequestObjectResult("Incorrect current password");
                }

                // Check if the new password matches the confirmation password
                if (password.NewPassword != password.ConfirmPassword)
                {
                    return new BadRequestObjectResult("New password and confirmation password do not match");
                }

                // Update the user's password
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, password.NewPassword);

                if (result.Succeeded)
                {
                    return new OkObjectResult("Password updated successfully");
                }
                return new BadRequestObjectResult("Error updating password");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error updating password: {ex.Message}");
            }
        }

        public async Task<IActionResult> DeleteCustomer(string customerId)
        {
            try
            {
                // Find the customer by ID
                Customer customer = await _obrnDbContext.Customers.FirstOrDefaultAsync(c => c.PkCustomerId == customerId);
                if (customer == null)
                {
                    return new NotFoundObjectResult("Customer not found");
                }

                // Find the corresponding AspNetUser by email
                var user = await _userManager.FindByEmailAsync(customer.Email);
                if (user == null)
                {
                    return new NotFoundObjectResult("User not found");
                }

                // Find and delete the customer's referral code
                Referral referralCode = await _obrnDbContext.Referrals.FirstOrDefaultAsync(r => r.FkReferredCustomerId == customerId);
                _obrnDbContext.Referrals.Remove(referralCode);

                // Find and delete any referrals made by this customer
                var referrals = _obrnDbContext.Referrals.Where(r => r.FkReferredCustomerId == customerId);
                if (referrals.Any())
                {
                    _obrnDbContext.Referrals.RemoveRange(referrals);
                }

                // Find and delete transactions associated with customer
                TransactionRepo transactionRepo = new TransactionRepo(_context, _obrnDbContext);
                var message = transactionRepo.DeleteTransactionsFromUser(customerId);

                // Delete the customer
                _obrnDbContext.Customers.Remove(customer);
                await _obrnDbContext.SaveChangesAsync();

                // Delete the AspNetUser
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    // Handle delete user error if needed
                    return new BadRequestObjectResult("Error deleting user");
                }

                return new OkObjectResult("Customer and associated user and referrals deleted successfully");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error deleting customer: {ex.Message}");
            }
        }


        private async Task<bool> IsCustomerUsernameAvailable(string username)
        {
            obrnDbContext dbContext = new obrnDbContext();
            Customer existingCustomer = await dbContext.Customers.FirstOrDefaultAsync(c => c.PkCustomerId == username);
            return existingCustomer == null;
        }

        private Customer CreateNewCustomer(RegisterCustomerDTO customer)
        {
            return new Customer
            {
                PkCustomerId = customer.PkCustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Phone = customer.Phone,
                Birthdate = customer.Birthdate,
                Email = customer.Email,
                Vip = customer.Vip,
                Confirm18 = customer.Confirm18
            };
        }

        private async Task<IActionResult> HandleCustomerReferral(RegisterCustomerDTO customer, IdentityUser user)
        {
            if (customer.FkReferralId != null)
            {
                ReferralRepo referralRepo = new ReferralRepo(_context, _obrnDbContext);
                var referralType = await referralRepo.GetReferralTypeById(customer.FkReferralId);
                switch (referralType.ToString())
                {
                    case "C":
                        return await HandleCustomerReferralByCustomer(customer, user);
                    case "B":
                        return await HandleCustomerReferralByBusiness(customer, user);
                    default:
                        return await HandleDefaultCustomerReferral(customer);
                }
            }
            else
            {
                return await HandleDefaultCustomerReferral(customer);
            }
        }

        private async Task<IActionResult> HandleCustomerReferralByCustomer(RegisterCustomerDTO customer, IdentityUser user)
        {
            ReferralRepo referralRepo1 = new ReferralRepo(_context, _obrnDbContext);
            var referrerCustomerId = await referralRepo1.GetFkReferredCustomerId(customer.FkReferralId);
            ReferralDTO referralDTO = new ReferralDTO
            {
                FkReferrerCustomerId = referrerCustomerId.ToString(),
                FkReferredCustomerId = customer.PkCustomerId,
            };

            ReferralRepo referralRepo2 = new ReferralRepo(_context, _obrnDbContext);
            var referralResult = await referralRepo2.CreateReferralCodeForCustomer(referralDTO);
            if (referralResult is OkObjectResult referralOkResult)
            {
                Console.WriteLine("Referral code created");
                return new OkObjectResult(new { Message = "Referral completed successfully", ReferralId = referralOkResult.Value });
            }
            return referralResult;
        }

        private async Task<IActionResult> HandleCustomerReferralByBusiness(RegisterCustomerDTO customer, IdentityUser user)
        {
            ReferralRepo referralRepo1 = new ReferralRepo(_context, _obrnDbContext);

            var referrerBusinessId = await referralRepo1.GetFkReferredBusinessId(customer.FkReferralId);
            ReferralDTO referralDTO = new ReferralDTO
            {
                FkReferrerBusinessId = referrerBusinessId.ToString(),
                FkReferredCustomerId = customer.PkCustomerId,
            };

            ReferralRepo referralRepo2 = new ReferralRepo(_context, _obrnDbContext);
            var referralResult = await referralRepo2.CreateReferralCodeForCustomer(referralDTO);
            if (referralResult is OkObjectResult referralOkResult)
            {
                Console.WriteLine("Referral code created");
                return new OkObjectResult(new { Message = "Referral completed successfully", ReferralId = referralOkResult.Value });
            }
            return referralResult;
        }

        private async Task<IActionResult> HandleDefaultCustomerReferral(RegisterCustomerDTO customer)
        {
            ReferralRepo referralRepo = new ReferralRepo(_context, _obrnDbContext);
            ReferralDTO referralDTO = new ReferralDTO
            {
                FkReferredCustomerId = customer.PkCustomerId,
            };

            var referralResult = await referralRepo.CreateReferralCodeForCustomer(referralDTO);
            if (referralResult is OkObjectResult referralOkResult)
            {
                Console.WriteLine("Referral code created");
                return new OkObjectResult(new { Message = "Referral completed successfully", ReferralId = referralOkResult.Value });
            }
            return referralResult;
        }
    }
}