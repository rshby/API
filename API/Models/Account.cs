﻿using System;
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
        public Employee Employee { get; set; }
        public Profiling Profiling { get; set; }
    }
}
