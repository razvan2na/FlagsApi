﻿namespace FlagsApi.Dtos
{
    public class PasswordChangeRequest
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}