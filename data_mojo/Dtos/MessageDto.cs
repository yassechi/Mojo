
namespace data_mojo.dtos
{
    public class MessageAddDto
    {
        public string Contenu { get; set; } = null!;
        public DateTime DateEnvoi { get; set; }
        public int UserId { get; set; }
        public int DiscussionId { get; set; }
    }

    public class MessageUpdateDto
    {
        public int Id { get; set; }
        public string Contenu { get; set; } = null!;
        public DateTime DateEnvoi { get; set; }
        public int UserId { get; set; }
        public int DiscussionId { get; set; }

    }
}