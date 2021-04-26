using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    public class Login:User
    {
        public bool verifyLogin(Login login)
        {
           return ConnectDb.Authentication(login);

        }
    }
}
