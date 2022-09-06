using Account.Service.Models;
using Account.WebAPI.DTO;
using AutoMapper;

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

    }
}

