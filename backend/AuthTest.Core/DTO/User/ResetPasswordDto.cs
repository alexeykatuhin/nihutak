using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AuthTest.Core.DTO.User
{
    public  class ResetPasswordDto
    {
        [Required]
        public string Email { get; set; }
    }
}
