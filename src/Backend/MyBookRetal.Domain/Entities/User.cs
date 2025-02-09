using System.ComponentModel.DataAnnotations.Schema;

namespace MyBookRental.Domain.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [Column("Telefone")]
        public string Phone { get; set; } = string.Empty;
        [Column("Perfil")]
        public string Profile { get; set; } = "Usuário";
        public string Password { get; set; } = string.Empty;
        public Guid UserIdentifier { get; set; } = Guid.NewGuid();
    }
}
