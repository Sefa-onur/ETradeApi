using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Application.DTOs.Login
{
    public class LoginUserResponse
    {
        public Token token { get; set; }
        public string Message { get; set; }
    }
}
