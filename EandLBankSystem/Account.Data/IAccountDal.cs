using Account.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Data;

public interface IAccountDal
{
    Task<Customer> getCustomerByEmail(string email);
    Task<int> SignIn(string email, string password);
    Task<Entities.Account> GetAccountInfo(int id);
    Task<bool> SignUp(Customer customer);
}

