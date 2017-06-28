using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.BAL.Services;

namespace WebApi.BAL
{
    public class Account : IAccountService
    {
        public long Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
