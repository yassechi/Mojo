using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace data_mojo.models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Contenu { get; set; } = null!;
        public DateTime DateEnvoi { get; set; }

        [ForeignKey(nameof(Discussion))]
        public int DiscussionId { get; set; }
        public Discussion? Discussion { get; set; }
    }
}