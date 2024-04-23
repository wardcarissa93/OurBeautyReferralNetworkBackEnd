﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OurBeautyReferralNetwork.Data;
using OurBeautyReferralNetwork.Models;
using OurBeautyReferralNetwork.Utilities;


namespace OurBeautyReferralNetwork.Repositories
{
    public class CustomerRepo
    {
        private readonly JWTUtilities _jWTUtilities;
        private readonly obrnDbContext _obrnDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomerRepo(JWTUtilities jWTUtilities,
                            obrnDbContext obrnDbContext,
                            UserManager<IdentityUser> userManager)
        {
            _jWTUtilities = jWTUtilities;
            _obrnDbContext = obrnDbContext;
            _userManager = userManager;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            IEnumerable<Customer> customers = _obrnDbContext.Customers.ToList();
            return customers;
        }

        public async Task<IActionResult> AddCustomer(RegisterCustomer customer)
        {
            try
            {
                Customer newCustomer = new Customer
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

                _obrnDbContext.Customers.Add(newCustomer);
                await _obrnDbContext.SaveChangesAsync();

                var user = new IdentityUser
                {
                    UserName = customer.PkCustomerId,
                    Email = customer.Email
                };

                var result = await _userManager.CreateAsync(user, customer.Password);

                if (result.Succeeded)
                {
                    // Generate JWT for the added customer
                    var token = _jWTUtilities.GenerateJwtToken(customer.Email);

                    return new OkObjectResult(new { Message = "Customer added successfully", Token = token });
                }

                return new BadRequestObjectResult(new { Errors = result.Errors });
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
                var token = _jWTUtilities.GenerateJwtToken(user.Email);

                return new OkObjectResult(new { Message = "Login successful", Token = token });
            }

            return new BadRequestObjectResult(new { Message = "Invalid username or password" });
        }

    }
}