using Account.Data;
using Account.Data.Entities;
using Account.Service.Models;
using AutoMapper;

namespace Account.Service;

public class OperationService : IOperationService
{
    private readonly IAccountDal _accountDal;
    private readonly IMapper _mapper;
    public OperationService(IAccountDal accountDal)
    {
        _accountDal = accountDal;
        _mapper = ConfigureAutoMapper();
    }

    
    public async Task<List<OperationModel>> GetOperationsByAccountIdAsync(int accountId, int currentPage, int pageSize)
    {
        List<OperationSecondSideModel> l = await _accountDal.GetOperationsByAccountIdAsync(accountId, currentPage, pageSize);
        return _mapper.Map<List<OperationModel>>(l);
    }
    private static IMapper ConfigureAutoMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ServiceAutoMapper>();
        });
        return config.CreateMapper();
    }
}

