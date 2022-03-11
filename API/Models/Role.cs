using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_role")]
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string name { get; set; }

        // Relation
        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
