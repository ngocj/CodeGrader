﻿using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.UserDto
{
    public class UserUpdateDto
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string? Birthday { get; set; }
        public string? Bio { get; set; }
        public string? GithubLink { get; set; }
        public string? LinkedInLink { get; set; }
    }
}
