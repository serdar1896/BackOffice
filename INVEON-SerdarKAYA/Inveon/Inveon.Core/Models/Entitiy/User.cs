using System;

namespace Inveon.Core.Models
{
    public class User:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? RegistirationDate { get; set; }
        public int Role { get; set; }
        public bool Status { get; set; }
    }
}
