
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Model
{
    [Table("FRUIT")]
    public class Fruit
    {
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("NAME")]
        [Required(ErrorMessage = "Please inform a valid fruit name.")]
        [MinLength(2, ErrorMessage = "Please inform a valid fruit type.")]
        [MaxLength(100, ErrorMessage = "Max name length is 100 chars.")]
        public string Name { get; set; }

        [Column("DESCRIPTION")]
        [Required(ErrorMessage = "Please inform a valid fruit description.")]
        [MinLength(25, ErrorMessage = "Please inform a valid fruit description.")]
        [MaxLength(255, ErrorMessage = "Max description length is 255 chars.")]
        public string Description { get; set; }

        [ForeignKey("FruitType")]
        public long Type { get; set; }
    }
}
