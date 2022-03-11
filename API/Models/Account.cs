using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_tr_account")]
    public class Account
    {
        [Key]
        [ForeignKey("Employee")]
        public string NIK { get; set; }

        [Required]
        public string Password { get; set; }
        public int OTP { get; set; }
        public DateTime ExpiredToken { get; set; }
        public bool IsUsed { get; set; }

        // Relation
        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        [JsonIgnore]
        public virtual Profiling Profiling { get; set; }
        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}
