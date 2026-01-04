using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_mojo.data.models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LasttName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Hpassword { get; set; } = null!;
        public int Role { get; set; }
        public float TailleCm { get; set; }
        public bool IsActif { get; set; }

        [ForeignKey(nameof(Organisation))]
        public int OrganisationId { get; set; }
        public Organisation? Organisation { get; set; }
        // Pour l'utilisateur "0" : les contrats dont il profite
        public virtual List<Contrat> ContratsRecus { get; set; } = new();

        // Pour le chef "1" : les contrats qu'il gère
        public virtual List<Contrat> ContratsGeres { get; set; } = new();
        public List<Message> Messages = [];
        public List<Discussion> Discussions = [];
        
    }
}