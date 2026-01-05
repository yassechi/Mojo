using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace data_mojo.models
{
    public class Discussion
    {
        [Key]
        public int Id { get; set; }
        public string Objet { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime DateCreation { get; set; }

        [ForeignKey(nameof(User))]
        public string ClientId { get; set; } = null!;
        public User? Client { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public string MojoId { get; set; } = null!;
        public User? Mojo { get; set; }

        public List<Message> Messages = [];
    }
}