using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("publishpicture")]
    [Index(nameof(PId), Name = "p_id")]
    public partial class Publishpicture
    {
        [Key]
        [Column("u_id")]
        [StringLength(8)]
        public string UId { get; set; }
        [Key]
        [Column("p_id")]
        [StringLength(8)]
        public string PId { get; set; }
        [Column("publish_time", TypeName = "date")]
        public DateTime? PublishTime { get; set; }

        [ForeignKey(nameof(PId))]
        [InverseProperty(nameof(Picture.Publishpictures))]
        public virtual Picture PIdNavigation { get; set; }
        [ForeignKey(nameof(UId))]
        [InverseProperty(nameof(User.Publishpictures))]
        public virtual User UIdNavigation { get; set; }
    }
}
