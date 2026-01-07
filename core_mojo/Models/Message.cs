using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace core_mojo.models
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