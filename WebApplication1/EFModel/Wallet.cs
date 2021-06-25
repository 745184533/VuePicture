using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("wallet")]
    public partial class Wallet
    {
        [Key]
        [Column("u_id")]
        [StringLength(8)]
        public string UId { get; set; }
        [Column("coin")]
        public int? Coin { get; set; }
        [Column("publish_num")]
        public int? PublishNum { get; set; }
        [Column("buy_num")]
        public int? BuyNum { get; set; }

        [ForeignKey(nameof(UId))]
        [InverseProperty(nameof(User.Wallet))]
        public virtual User UIdNavigation { get; set; }
    }
}
