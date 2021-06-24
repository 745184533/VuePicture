using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("checks")]
    [Index(nameof(CId), Name = "c_id")]
    public partial class Check
    {
        [Key]
        [Column("u_id")]
        [StringLength(8)]
        public string UId { get; set; }
        [Key]
        [Column("c_id")]
        [StringLength(8)]
        public string CId { get; set; }
        [Column("c_time", TypeName = "date")]
        public DateTime? CTime { get; set; }

        [ForeignKey(nameof(CId))]
        [InverseProperty(nameof(Checkinfo.Checks))]
        public virtual Checkinfo CIdNavigation { get; set; }
        [ForeignKey(nameof(UId))]
        [InverseProperty(nameof(User.Checks))]
        public virtual User UIdNavigation { get; set; }
    }
}
