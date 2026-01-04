using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_mojo.dtos
{
    public class ContratAddDto
    {
        [Column(TypeName = "date")]
        public DateOnly DateDebut { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly DateFin { get; set; }
        public decimal LoyerMensuelHT { get; set; }
        public bool StatutContrat { get; set; }
        public int VeloId { get; set; }
        public int BeneficiaireId { get; set; }
        public int UserRhId { get; set; }
    }
}