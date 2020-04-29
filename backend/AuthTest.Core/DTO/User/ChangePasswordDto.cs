using System;
using System.Collections.Generic;
using System.Text;

namespace AuthTest.Core.DTO.User
{
    public class ChangePasswordDto

    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
    }
}
