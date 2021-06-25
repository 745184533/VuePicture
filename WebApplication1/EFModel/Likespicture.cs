using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("likespicture")]
    [Index(nameof(PId), Name = "p_id")]
    public partial class Likespicture
    {
        [Key]
        [Column("u_id")]
        [StringLength(8)]
        public string UId { get; set; }
        [Key]
        [Column("p_id")]
        [StringLength(8)]
        public string PId { get; set; }
        [Column("like_time", TypeName = "date")]
        public DateTime? LikeTime { get; set; }
        [Column("like_type")]
        [StringLength(2)]
        public string LikeType { get; set; }

        [ForeignKey(nameof(PId))]
        [InverseProperty(nameof(Picture.Likespictures))]
        public virtual Picture PIdNavigation { get; set; }
        [ForeignKey(nameof(UId))]
        [InverseProperty(nameof(User.Likespictures))]
        public virtual User UIdNavigation { get; set; }
    }
}
