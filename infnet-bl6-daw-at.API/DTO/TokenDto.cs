﻿namespace AT.API.DTOs.Users
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
