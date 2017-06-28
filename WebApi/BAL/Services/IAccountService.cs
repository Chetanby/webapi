using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.BAL.Services
{
    interface IAccountService
    {
        Int64 Authenticate(string username, string password);
        
    }
}
