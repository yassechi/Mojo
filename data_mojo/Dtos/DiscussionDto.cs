
namespace data_mojo.dtos
{
    public class DiscussionAddDto
    {
        public string Objet { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime DateCreation { get; set; }
        public int UserId { get; set; }
    }

    public class DiscussionUpdateDto
    {
        public int Id { get; set; }
        public string Objet { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime DateCreation { get; set; }
        public int UserId { get; set; }

    }
}