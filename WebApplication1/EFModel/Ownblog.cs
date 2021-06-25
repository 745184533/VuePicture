using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("ownblog")]
    [Index(nameof(BId), Name = "b_id")]
    public partial class Ownblog
    {
        [Key]
        [Column("u_id")]
        [StringLength(8)]
        public string UId { get; set; }
        [Key]
        [Column("b_id")]
        [StringLength(8)]
        public string BId { get; set; }

        [ForeignKey(nameof(BId))]
        [InverseProperty(nameof(Blog.Ownblogs))]
        public virtual Blog BIdNavigation { get; set; }
        [ForeignKey(nameof(UId))]
        [InverseProperty(nameof(User.Ownblogs))]
        public virtual User UIdNavigation { get; set; }
    }
}
