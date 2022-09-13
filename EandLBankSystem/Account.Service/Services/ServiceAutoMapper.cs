using Account.Data;
using Account.Data.Entities;
using Account.Service.Models;
using AutoMapper;

namespace Account.Service;

public class ServiceAutoMapper : Profile
{
    public ServiceAutoMapper()
    {
        CreateMap<Data.Entities.Account, AccountModel>()
               .ReverseMap();
       
        CreateMap<Customer, CustomerModel>()
               .ReverseMap();

        CreateMap<OperationSecondSideModel, OperationModel>()
            .ForMember(des => des.SecondSideAccountId, opts => opts
            .MapFrom(src => src.SecondSideAccountId))
            .ReverseMap();
    }
}

