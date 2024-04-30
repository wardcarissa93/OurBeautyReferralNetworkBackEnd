﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OurBeautyReferralNetwork.Data;
using OurBeautyReferralNetwork.Models;
using OurBeautyReferralNetwork.Repositories;

namespace OurBeautyReferralNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferralController : ControllerBase
    {
        private readonly ReferralRepo _referralRepo;
        private readonly obrnDbContext _obrnDbContext;

        public ReferralController(ReferralRepo referralRepo,
                                  obrnDbContext obrnDbContext)
        {
            _referralRepo = referralRepo;
            _obrnDbContext = obrnDbContext;
        }

        [HttpGet("getreferrals")]
        public ActionResult<IEnumerable<Referral>> GetReferrals()
        {
            var referrals = _referralRepo.GetAllReferrals();
            return Ok(referrals);
        }

        [HttpGet("getreferral/{id}")]
        public async Task<IActionResult> GetReferralById(string id)
        {
            var result = await _referralRepo.GetReferralById(id);
            return result;
        }

        [HttpPost("createreferralcustomer")]
        public async Task<IActionResult> CreateReferralCodeForCustomer(string customerId)
        {
            var result = await _referralRepo.CreateReferralCodeForCustomer(customerId);
            return result;
        }


    }
}