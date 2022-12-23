
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Model
{
    [Table("FRUIT_TYPE")]
    public class FruitType
    {
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("NAME")]
        [Required(ErrorMessage = "Please inform a valid fruit type.")]
        [MinLength(2, ErrorMessage = "Please inform a valid fruit type.")]
        [MaxLength(100, ErrorMessage = "Max name length is 100 chars.")]
        public string Name { get; set; }

        [Column("DESCRIPTION")]
        [Required(ErrorMessage = "Please inform a valid fruit type description.")]
        [MinLength(25, ErrorMessage = "Please inform a valid fruit type description.")]
        [MaxLength(255, ErrorMessage = "Max type description length is 255 chars.")]
        public string Description { get; set; }
    }
}
