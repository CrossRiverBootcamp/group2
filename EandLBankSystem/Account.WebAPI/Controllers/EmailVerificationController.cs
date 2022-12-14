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
        public async Task<ActionResult> AddEmailVerificationAsync([FromBody]string email)
        {
            try 
            { 
                await _emailVerificationService.AddEmailVerificationProcAsync(email);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}
