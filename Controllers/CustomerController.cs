﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OurBeautyReferralNetwork.Data;
using OurBeautyReferralNetwork.DataTransferObjects;
using OurBeautyReferralNetwork.Models;
using OurBeautyReferralNetwork.Repositories;
using OurBeautyReferralNetwork.Utilities;
using System;
using System.Threading.Tasks;

namespace OurBeautyReferralNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly CustomerRepo _customerRepo;

        public CustomerController(ApplicationDbContext context,
                                  IConfiguration configuration,
                                  CustomerRepo customerRepo)
        {
            _context = context;
            _configuration = configuration;
            _customerRepo = customerRepo;
        }

        [HttpGet("get-customers")]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            var customers = _customerRepo.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("get-customer/{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            var result = await _customerRepo.GetCustomerById(id);
            return result;
        }

        [HttpGet("get-customer-by-email")]
        public async Task<IActionResult> GetCustomerByEmail(string email)
        {
            var result = await _customerRepo.GetCustomerByEmail(email);
            return result;
        }

        [HttpPost("add-customer")]
        public async Task<IActionResult> AddCustomer(RegisterCustomerDTO model)
        {
            var result = await _customerRepo.AddCustomer(model);
            return result;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User model)
        {
            var result = await _customerRepo.Login(model);
            return result;
        }

        [HttpPost("edit-customer")]
        public async Task<IActionResult> EditCustomer(EditCustomerDTO customer)
        {
            var result = await _customerRepo.EditCustomer(customer);
            return result;
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword(EditPasswordDTO password)
        {
            var result = await _customerRepo.UpdatePassword(password);
            return result;
        }

        [HttpDelete("delete-customer/{id}")]
        public async Task<IActionResult> DeleteCustomerById(string id)
        {
            var result = await _customerRepo.DeleteCustomer(id);
            return result;
        }
    }
}