﻿namespace LVCore.LVApp.Shared.Dtos.AuthDtos
{
    public class UserUpdateDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
