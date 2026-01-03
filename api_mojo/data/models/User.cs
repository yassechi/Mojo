using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_mojo.data.models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LasttName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Hpassword { get; set; } = null!;
        public int Role { get; set; }
        public float TailleCm { get; set; }
        public bool IsActif { get; set; }
    }
}