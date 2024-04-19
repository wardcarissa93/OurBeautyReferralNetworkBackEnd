﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OurBeautyReferralNetwork.Data;
using OurBeautyReferralNetwork.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

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
            var fee = _obrnContext.Feeandcommissions.FirstOrDefault();
            return Ok(fee);
        }

        [HttpGet]
        [Route("/Fee")]
        //[ValidateModelState]
        [SwaggerOperation("FeeGet")]
        public virtual IActionResult FeeGet()
        {
            var fees = _obrnContext.Feeandcommissions.ToList();
            return Ok(fees);
        }
    }
}