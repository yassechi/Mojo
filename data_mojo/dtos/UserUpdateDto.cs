using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data_mojo.dtos
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LasttName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Hpassword { get; set; } = null!;
        public int Role { get; set; }
        public float TailleCm { get; set; }
        public bool IsActif { get; set; }
        public int OrganisationId { get; set; }

    }
}