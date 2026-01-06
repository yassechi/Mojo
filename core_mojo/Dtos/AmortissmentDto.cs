using System.ComponentModel.DataAnnotations.Schema;

namespace core_mojo.Dtos
{
    public class AmortissmentAddDto
    {
        public DateOnly DateDebut { get; set; }
        public decimal ValeurInit { get; set; }
        public int DureeMois { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValeurResiduelleFinale { get; set; }
        public int VeloId { get; set; }

    }

    public class AmortissmentUpdateDto
    {
        public int Id { get; set; }
        public DateOnly DateDebut { get; set; }
        public decimal ValeurInit { get; set; }
        public int DureeMois { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValeurResiduelleFinale { get; set; }
        public int VeloId { get; set; }
    }
}