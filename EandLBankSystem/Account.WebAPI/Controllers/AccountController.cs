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
    public async Task<ActionResult<int>> SignIn(SignInDTO signInDTO)
    {
        var res = await _accountService.SignInAsync(signInDTO.Email, signInDTO.Password);
        return Ok(res);
       
    }
    [HttpGet("Account/{id}")]
    public async Task<ActionResult<GetAccountDTO>> GetAccountInfo(int id)
    {
        return Ok(_mapper.Map<GetAccountDTO>(await _accountService.GetAccountInfoAsync(id)));
    }

    [HttpPost("SignUp/")]
    public async Task<ActionResult<bool>> SignUp([FromBody] SignUpDTO signUpDTO)
    {
        bool result = await _accountService.SignUpAsync(_mapper.Map<CustomerModel>(signUpDTO));
        return result ? Ok(result) : BadRequest(result);
    }
}

