using Account.Service.Models;
using Account.Service.Services;
using Account.WebAPI.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Account.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailVerificationController : ControllerBase
    {
        private readonly IEmailVerificationService _emailVerificationService;
        private readonly IMapper _mapper;
        public EmailVerificationController(IEmailVerificationService emailVerificationService, IMapper mapper)
        {
            _emailVerificationService = emailVerificationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AddEmailVerificationAsync(string email)
        {
            await _emailVerificationService.AddEmailVerificationAsync(email);
            return Ok();
        }

        [HttpPost("code")]
        public async Task<ActionResult<bool>> CheckVerificationCodeAsync(CheckVerificationCodeDTO verification)
        {
            return Ok(await _emailVerificationService.CheckVerificationCodeAsync(_mapper.Map<EmailVerificationModel>(verification)));
        }
    }
}
