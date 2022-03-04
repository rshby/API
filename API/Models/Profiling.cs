using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_profiling")]
    public class Profiling
    {
        [Key]
        [ForeignKey("Account")]
        public string NIK { get; set; }
        
        [Required]
        [ForeignKey("Education")]
        public int Education_Id { get; set; }

        // Relation
        public Education Education { get; set; }
        public Account Account { get; set; }
    }
}
