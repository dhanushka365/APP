﻿namespace APP.Models.Domain
{
    public class User
    {
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }  // "Admin", "Teacher", "Student"
    }
}
