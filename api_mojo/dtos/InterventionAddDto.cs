using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_mojo.dtos
{
    public class InterventionAddDto
    {
        public DateTime DateIntervention { get; set; }
        public string TypeIntervention { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Cout { get; set; }
        public int VeloId { get; set; }
    }
}