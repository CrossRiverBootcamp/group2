using Account.Service;
using Account.WebAPI.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;


namespace Account.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IOperationService _operationService;
        private readonly IMapper _mapper;
        public OperationController(IOperationService operationService , IMapper mapper)
        {
            _operationService = operationService;
            _mapper = mapper;
        }

        [HttpGet("{accountId}")]
        public async Task<ActionResult<List<GetOperationsDTO>>> GetOperationsByAccountIdAsync( int accountId,[FromQuery] int position, [FromQuery] int pageSize)
        {
            return Ok(_mapper.Map<List<GetOperationsDTO>>(await _operationService.GetOperationsByAccountIdAsync(accountId, position, pageSize)));
        }
    }
}
