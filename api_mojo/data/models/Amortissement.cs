using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_mojo.data.models
{
    public class Amortissement
    {
        [Key]
        public int Id { get; set; }
        public DateOnly DateDebut { get; set; }
        public decimal ValeurInit { get; set; }
        public int DureeMois { get; set; }
        [Column(TypeName ="decimal(10,2)")]
        public decimal ValeurResiduelleFinale { get; set; }
    }
}