using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("orders")]
    [Index(nameof(OId), Name = "o_id")]
    public partial class Order
    {
        [Key]
        [Column("u_id")]
        [StringLength(8)]
        public string UId { get; set; }
        [Key]
        [Column("o_id")]
        [StringLength(8)]
        public string OId { get; set; }

        [ForeignKey(nameof(OId))]
        [InverseProperty(nameof(Orderinfo.Orders))]
        public virtual Orderinfo OIdNavigation { get; set; }
        [ForeignKey(nameof(UId))]
        [InverseProperty(nameof(User.Orders))]
        public virtual User UIdNavigation { get; set; }
    }
}
