using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("payment")]
    public partial class Payment
    {
        [Key]
        [Column("pay_id")]
        [StringLength(8)]
        public string PayId { get; set; }
        [Column("u_id")]
        [StringLength(8)]
        public string UId { get; set; }
        [Column("pay_time", TypeName = "date")]
        public DateTime? PayTime { get; set; }
        [Column("coin")]
        public int? Coin { get; set; }
        [Column("type")]
        [StringLength(2)]
        public string Type { get; set; }
    }
}
