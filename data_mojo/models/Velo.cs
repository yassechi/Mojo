using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace data_mojo.models
{
    public class Velo
    {
        [Key]
        public int Id { get; set; }
        public string NumeroSerie { get; set; } = null!;
        public string Marque { get; set; } = null!;
        public string Modele { get; set; } = null!;
        public decimal PrixAchat { get; set; }
        public bool Status { get; set; }
        public List<Intervention> Interventions { get; set; } = [];
        public List<Amortissement> Amortissements { get; set; } = [];
        public List<Contrat> Contrats { get; set; } = [];
    }
}