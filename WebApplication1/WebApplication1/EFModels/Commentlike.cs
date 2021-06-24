using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("commentlike")]
    [Index(nameof(CommUId), nameof(PId), Name = "comm_u_id")]
    public partial class Commentlike
    {
        [Key]
        [Column("u_id")]
        [StringLength(8)]
        public string UId { get; set; }
        [Key]
        [Column("comm_u_id")]
        [StringLength(8)]
        public string CommUId { get; set; }
        [Key]
        [Column("p_id")]
        [StringLength(8)]
        public string PId { get; set; }

        [ForeignKey("CommUId,PId")]
        [InverseProperty("Commentlikes")]
        public virtual Piccomment Piccomment { get; set; }
        [ForeignKey(nameof(UId))]
        [InverseProperty(nameof(User.Commentlikes))]
        public virtual User UIdNavigation { get; set; }
    }
}
