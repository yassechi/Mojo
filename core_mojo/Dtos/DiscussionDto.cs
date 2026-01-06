
namespace core_mojo.Dtos
{
    public class DiscussionAddDto
    {
        public string Objet { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime DateCreation { get; set; }
        public string ClientId { get; set; } = null!;
        public string MojoId { get; set; } = null!;
    }

    public class DiscussionUpdateDto
    {
        public int Id { get; set; }
        public string Objet { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime DateCreation { get; set; }
        public string ClientId { get; set; } = null!;
        public string MojoId { get; set; } = null!;
    }
}

//    [ForeignKey(nameof(User))]
//         public string ClientId { get; set; }
//         public User? Client { get; set; } = null!;

//         [ForeignKey(nameof(User))]
//         public string MojoId { get; set; } = null!;
//         public User? Mojo { get; set; }