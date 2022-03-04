using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_education")]
    public class Education
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Degree { get; set; }

        [Required]
        public string GPA { get; set; }

        [Required]
        [ForeignKey("University")]
        public int University_Id { get; set; }

        // Relation
        public ICollection<Profiling> Profiling { get; set; }
        public University University { get; set; }
    }
}
