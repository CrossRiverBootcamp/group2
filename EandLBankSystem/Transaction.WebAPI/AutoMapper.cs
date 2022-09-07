using Transaction.Service.Models;
using Transaction.WebAPI.DTO;
using AutoMapper;

namespace Transaction.WebAPI;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<TransactionModel, PostTransactionDTO>().ReverseMap();
    }
}

