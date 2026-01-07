using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace core_mojo.models

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
        public string BeneficiaireId { get; set; } = null!;
        public virtual User Beneficiaire { get; set; } = null!;
        public string UserRhId { get; set; } = null!;
        public virtual User UserRH { get; set; } = null!;
    }
}