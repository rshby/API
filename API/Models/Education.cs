using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_education")]
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
        [JsonIgnore]
        public virtual ICollection<Profiling> Profiling { get; set; }
        [JsonIgnore]
        public virtual University University { get; set; }
    }
}
