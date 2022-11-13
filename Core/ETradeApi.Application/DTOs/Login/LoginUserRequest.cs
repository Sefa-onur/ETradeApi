using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Application.DTOs.Login
{
    public class LoginUserRequest
    {
        public string UserNameorEmail { get; set; }
        public string Password { get; set; }
    }
}
