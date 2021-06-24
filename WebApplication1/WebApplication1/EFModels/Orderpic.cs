using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("orderpic")]
    [Index(nameof(PId), Name = "p_id")]
    public partial class Orderpic
    {
        [Key]
        [Column("o_id")]
        [StringLength(8)]
        public string OId { get; set; }
        [Key]
        [Column("p_id")]
        [StringLength(8)]
        public string PId { get; set; }

        [ForeignKey(nameof(OId))]
        [InverseProperty(nameof(Orderinfo.Orderpics))]
        public virtual Orderinfo OIdNavigation { get; set; }
        [ForeignKey(nameof(PId))]
        [InverseProperty(nameof(Picture.Orderpics))]
        public virtual Picture PIdNavigation { get; set; }
    }
}
