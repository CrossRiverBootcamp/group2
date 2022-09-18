using Account.Service;
using Account.Service.Models;
using Account.WebAPI.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Account.WebAPI.Controllers;

[Route("api/")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;
    public AccountController(IAccountService accountService , IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    [HttpPost("Login/")]
    public async Task<ActionResult<int>> SignInAsync(SignInDTO signInDTO)
    {
        try
        {
            return Ok(await _accountService.SignInAsync(signInDTO.Email, signInDTO.Password));
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }    
    }

    [HttpGet("Account/{id}")]
    public async Task<ActionResult<GetAccountDTO>> GetAccountInfoAsync(int id)
    {
        try
        {
            return Ok(_mapper.Map<GetAccountDTO>(await _accountService.GetAccountInfoAsync(id)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("SignUp/")]
    public async Task<ActionResult> SignUpAsync([FromBody] SignUpDTO signUpDTO)
    {
        try
        {
            await _accountService.SignUpAsync(_mapper.Map<CustomerModel>(signUpDTO), signUpDTO.VerificationCode);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}

