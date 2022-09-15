using Account.Service.Models;
using Account.WebAPI.DTO;
using AutoMapper;
using Transaction.Messages.Events;

namespace Account.WebAPI;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        CreateMap<AccountModel, GetAccountDTO>()
               .ForMember(des => des.FirstName, opts => opts
                        .MapFrom(src => src.Customer.FirstName))
               .ForMember(des => des.LastName, opts => opts
                        .MapFrom(src => src.Customer.LastName))
               .ForMember(des => des.Email, opts => opts
                        .MapFrom(src => src.Customer.Email))
               .ReverseMap();
        
        CreateMap<CustomerModel, SignUpDTO>()
               .ReverseMap();

        CreateMap<TransactionSagaStarted, TransactionModel>()
            .ForMember(des => des.OperationTime, opts => opts
                        .MapFrom(any=> DateTime.UtcNow))
               .ReverseMap();

        CreateMap<OperationModel, GetOperationsDTO>()
            .ReverseMap();

        CreateMap<EmailVerificationModel, CheckVerificationCodeDTO>()
            .ReverseMap();
    }
}

