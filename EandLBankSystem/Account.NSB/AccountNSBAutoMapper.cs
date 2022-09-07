using Account.Service.Models;
using AutoMapper;
using Transaction.Messages.Events;

namespace Account.NSB;

public class AccountNSBAutoMapper : Profile
{
    public AccountNSBAutoMapper()
    {
        CreateMap<TransactionSagaStarted, TransactionModel>()
               .ReverseMap();
    }
}

