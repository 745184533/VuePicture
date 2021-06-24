using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("orderinfo")]
    public partial class Orderinfo
    {
        public Orderinfo()
        {
            Orderpics = new HashSet<Orderpic>();
            Orders = new HashSet<Order>();
        }

        [Key]
        [Column("o_id")]
        [StringLength(8)]
        public string OId { get; set; }
        [Column("o_type")]
        [StringLength(4)]
        public string OType { get; set; }
        [Column("o_status")]
        [StringLength(2)]
        public string OStatus { get; set; }
        [Column("o_description")]
        [StringLength(50)]
        public string ODescription { get; set; }
        [Column("reward")]
        public int? Reward { get; set; }

        [InverseProperty(nameof(Orderpic.OIdNavigation))]
        public virtual ICollection<Orderpic> Orderpics { get; set; }
        [InverseProperty(nameof(Order.OIdNavigation))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
