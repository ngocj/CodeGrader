﻿namespace Application.Dtos.UserDto
{
    public class UserCreateDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
