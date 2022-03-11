using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_accountrole")]
    public class AccountRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Account")]
        public string NIK { get; set; }

        [Required]
        [ForeignKey("Role")]
        public int Role_Id { get; set; }

        // Relation
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual Role Role { get; set; }

    }
}
