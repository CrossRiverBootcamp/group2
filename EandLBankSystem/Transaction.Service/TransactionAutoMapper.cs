using Transaction.Service.Models;
using AutoMapper;

namespace Transaction.Service;

public class TransactionAutoMapper : Profile
{
    public TransactionAutoMapper()
    {
        CreateMap<Data.Entities.Transaction, TransactionModel>()
               .ReverseMap();
    }
}

