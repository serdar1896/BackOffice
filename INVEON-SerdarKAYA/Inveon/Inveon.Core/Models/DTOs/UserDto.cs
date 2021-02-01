using System;
using System.ComponentModel.DataAnnotations;

namespace Inveon.Core.Models.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "{0} Can't be null")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "{0} Can't be null")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "{0} Can't be null")]
        [StringLength(24, ErrorMessage = "{0} Minimum 6 digits", MinimumLength = 6)]
        public string Password { get; set; }
        [Required(ErrorMessage = "{0} Can't be null")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} Can't be null")]
        [StringLength(11, ErrorMessage = "{0} Minimum 10 digits", MinimumLength = 10)]
        public string Phone { get; set; }
        public DateTime? RegistirationDate { get; set; }
        [Required(ErrorMessage = "{0} Can't be null")]
        public int Role { get; set; }
        public bool Status { get; set; }
    }
}
