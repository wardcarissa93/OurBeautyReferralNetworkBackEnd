﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OurBeautyReferralNetwork.Models;
using OurBeautyReferralNetwork.Repositories;

namespace OurBeautyReferralNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly BusinessRepo _businessRepo;

        public BusinessController(BusinessRepo businessRepo)
        {
            _businessRepo = businessRepo;
        }

        [HttpGet("getbusinesses")]
        public ActionResult<IEnumerable<Business>> GetBusinesses()
        {
            var businesses = _businessRepo.GetAllBusinesses();
            return Ok(businesses);
        }

        [HttpPost("addbusiness")]
        public async Task<IActionResult> AddBusiness(RegisterBusiness model)
        {
            var result = await _businessRepo.AddBusiness(model);
            return result;
        }
    }
}