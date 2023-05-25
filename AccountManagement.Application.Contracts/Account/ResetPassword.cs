﻿using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account
{
    public class ResetPassword
    {
        public long Id { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [DataType(DataType.Password)]
        [Compare("ReNewPassword", ErrorMessage = ValidationMessage.PasswordsNotMatched)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = ValidationMessage.PasswordsNotMatched)]
        public string ReNewPassword { get; set; }

        public DateTime RedirectionTime { get; set; }

        public string Token { get; set; }
    }
}
