using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace core_mojo.models
{
    public class Intervention
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateIntervention { get; set; }
        public string TypeIntervention { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Cout { get; set; }

        [ForeignKey(nameof(Velo))]
        public int VeloId { get; set; }
        public Velo? Velo { get; set; }
    }
}