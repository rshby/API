﻿namespace API.ViewModel
{
    public class ChangePasswordVM
    {
        public string Email { get; set; }
        public int OTP { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
