using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_mojo.data.models
{
    public class Contrat
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "date")]
        public DateOnly DateDebut { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly DateFin { get; set; }
        public decimal LoyerMensuelHT { get; set; }
        public bool StatutContrat { get; set; }

        [ForeignKey(nameof(Velo))]
        public int VeloId { get; set; }
        public Velo? Velo { get; set; }
        
        // Remplace public List<Accessoire> Accessoires = []; par :
        public virtual List<Accessoire> Accessoires { get; set; } = [];

        // LIEN 1 : L'utilisateur standard (Bénéficiaire - le "0")
        public int BeneficiaireId { get; set; }
        public virtual User Beneficiaire { get; set; } = null!;

        // LIEN 2 : Le Chef (User RH - le "1")
        public int UserRhId { get; set; }
        public virtual User UserRH { get; set; } = null!;
    }
}