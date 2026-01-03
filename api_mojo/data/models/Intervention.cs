using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_mojo.data.models
{
    public class Intervention
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateIntervention { get; set; }
        public string TypeIntervention { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Cout { get; set; }
    }
}