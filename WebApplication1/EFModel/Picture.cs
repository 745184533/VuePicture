using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("picture")]
    public partial class Picture
    {
        public Picture()
        {
            Checkinfos = new HashSet<Checkinfo>();
            Favoritepictures = new HashSet<Favoritepicture>();
            Likespictures = new HashSet<Likespicture>();
            Orderpics = new HashSet<Orderpic>();
            Owntags = new HashSet<Owntag>();
            Piccomments = new HashSet<Piccomment>();
            Publishpictures = new HashSet<Publishpicture>();
        }

        [Key]
        [Column("p_id")]
        [StringLength(8)]
        public string PId { get; set; }
        [Column("p_width")]
        public int? PWidth { get; set; }
        [Column("p_height")]
        public int? PHeight { get; set; }
        [Column("p_info")]
        [StringLength(250)]
        public string PInfo { get; set; }
        [Column("p_url")]
        [StringLength(200)]
        public string PUrl { get; set; }
        [Column("p_status")]
        [StringLength(2)]
        public string PStatus { get; set; }
        [Column("likes")]
        public int? Likes { get; set; }
        [Column("dislikes")]
        public int? Dislikes { get; set; }
        [Column("comm_num")]
        public int? CommNum { get; set; }
        [Column("price")]
        public int Price { get; set; }

        [InverseProperty(nameof(Checkinfo.PIdNavigation))]
        public virtual ICollection<Checkinfo> Checkinfos { get; set; }
        [InverseProperty(nameof(Favoritepicture.PIdNavigation))]
        public virtual ICollection<Favoritepicture> Favoritepictures { get; set; }
        [InverseProperty(nameof(Likespicture.PIdNavigation))]
        public virtual ICollection<Likespicture> Likespictures { get; set; }
        [InverseProperty(nameof(Orderpic.PIdNavigation))]
        public virtual ICollection<Orderpic> Orderpics { get; set; }
        [InverseProperty(nameof(Owntag.PIdNavigation))]
        public virtual ICollection<Owntag> Owntags { get; set; }
        [InverseProperty(nameof(Piccomment.PIdNavigation))]
        public virtual ICollection<Piccomment> Piccomments { get; set; }
        [InverseProperty(nameof(Publishpicture.PIdNavigation))]
        public virtual ICollection<Publishpicture> Publishpictures { get; set; }
    }
}
