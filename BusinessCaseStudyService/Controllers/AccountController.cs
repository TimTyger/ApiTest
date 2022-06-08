using BusinessCaseStudyService.Models;
using BusinessCaseStudyService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessCaseStudyService.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _accountService = accountService;
        }
        [Route("create-transaction-request")]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseObject<string>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> CreateRequest(TransactionRequest request) => await _accountService.CreateTransactionRequest(request, Request.Path.Value);
        
        [Route("check-transaction-status")]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseObject<StatusCheckerRes>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> CheckStatus(StatusReq request) => await _accountService.CheckTransactionStatus(request, Request.Path.Value);

    }
}
