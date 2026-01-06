
namespace core_mojo.Dtos
{
    public class InterventionAddDto
    {
        public DateTime DateIntervention { get; set; }
        public string TypeIntervention { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Cout { get; set; }
        public int VeloId { get; set; }
    }

    public class InterventionUpdateDto
    {
        public int Id { get; set; }
        public DateTime DateIntervention { get; set; }
        public string TypeIntervention { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Cout { get; set; }
        public int VeloId { get; set; }

    }
}