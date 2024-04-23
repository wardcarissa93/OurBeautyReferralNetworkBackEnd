﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OurBeautyReferralNetwork.Data;
using OurBeautyReferralNetwork.Models;
using OurBeautyReferralNetwork.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OurBeautyReferralNetwork.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class FeeController : ControllerBase
    {

        private readonly obrnDbContext _obrnContext;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public FeeController(ApplicationDbContext context, obrnDbContext obrnContext,
                              UserManager<IdentityUser> userManager,
                              IConfiguration configuration)
        {
            _obrnContext = obrnContext;
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("/fee/{feeId}")]

        public virtual IActionResult GetFee([FromRoute][Required] string feeId)
        {
            FeeRepo feeRepo = new FeeRepo(_context, _obrnContext);
            var fee = feeRepo.GetFeeById(feeId);
            if (fee != null)
            {
                return Ok(fee);

            }
            return NotFound("Fee not found");
        }

        [HttpGet]
        [Route("/fee")]
        //[ValidateModelState]
        [SwaggerOperation("FeeGet")]
        public virtual IActionResult FeeGet()
        {
            FeeRepo feeRepo = new FeeRepo(_context, _obrnContext);
            var fees = feeRepo.GetAllFees();
            return Ok(fees);
        }
    }
}
