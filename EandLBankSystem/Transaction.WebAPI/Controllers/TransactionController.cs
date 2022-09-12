using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Transaction.Service;
using Transaction.Service.Models;
using Transaction.WebAPI.DTO;

namespace Transaction.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        public TransactionController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostTransaction(PostTransactionDTO postTransactionDTO)
        {

            var result = await _transactionService.PostTransactionAsync(_mapper.Map<TransactionModel>(postTransactionDTO));
            return result? Ok(result): BadRequest(result);
        }
    }
}
