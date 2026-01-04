using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_mojo.dtos
{
    public class OrganisationUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public bool IsActif { get; set; }
    }
}