using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("favoritepicture")]
    [Index(nameof(PId), Name = "p_id")]
    public partial class Favoritepicture
    {
        [Key]
        [Column("u_id")]
        [StringLength(8)]
        public string UId { get; set; }
        [Key]
        [Column("p_id")]
        [StringLength(8)]
        public string PId { get; set; }
        [Column("fav_time", TypeName = "date")]
        public DateTime? FavTime { get; set; }

        [ForeignKey(nameof(PId))]
        [InverseProperty(nameof(Picture.Favoritepictures))]
        public virtual Picture PIdNavigation { get; set; }
        [ForeignKey(nameof(UId))]
        [InverseProperty(nameof(User.Favoritepictures))]
        public virtual User UIdNavigation { get; set; }
    }
}
