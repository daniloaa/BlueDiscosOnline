using System.ComponentModel.DataAnnotations;

namespace BlueDiscosOnline.Domain.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
