﻿namespace StudioData.Models.JWT
{
    public class UserToken 
    {
        public Guid Id { get; set; }
        public string? Token { get; set; }
        public string UserName { get; set; } = string.Empty;
        public TimeSpan Validity { get; set; } // Tiempó de validez que tiene este token
        public string? RefreshToken { get; set; }
        public string EmailId { get; set; } = string.Empty;
        public Guid GuidId { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
