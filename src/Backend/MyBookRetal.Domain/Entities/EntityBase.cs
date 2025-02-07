using System.ComponentModel.DataAnnotations.Schema;

namespace MyBookRental.Domain.Entities
{
    public class EntityBase
    {
        public long Id { get; set; }
        public byte Active { get; set; }

        [NotMapped]
        public bool IsActive
        {
            get => Active == 1;
            set => Active = value ? (byte)1 : (byte)0;
        }


        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        //public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;
    }
}
