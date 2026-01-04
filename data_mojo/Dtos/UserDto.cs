
namespace data_mojo.dtos
{
    public class UserAddDto
    {
        public string FirstName { get; set; } = null!;
        public string LasttName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Hpassword { get; set; } = null!;
        public int Role { get; set; }
        public float TailleCm { get; set; }
        public bool IsActif { get; set; }
        public int OrganisationId { get; set; }
    }

    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LasttName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Hpassword { get; set; } = null!;
        public int Role { get; set; }
        public float TailleCm { get; set; }
        public bool IsActif { get; set; }
        public int OrganisationId { get; set; }

    }
}