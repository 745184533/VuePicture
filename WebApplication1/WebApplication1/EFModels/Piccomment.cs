using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("piccomment")]
    [Index(nameof(PId), Name = "p_id")]
    public partial class Piccomment
    {
        public Piccomment()
        {
            Commentlikes = new HashSet<Commentlike>();
        }

        [Key]
        [Column("u_id")]
        [StringLength(8)]
        public string UId { get; set; }
        [Key]
        [Column("p_id")]
        [StringLength(8)]
        public string PId { get; set; }
        [Column("p_comment")]
        [StringLength(50)]
        public string PComment { get; set; }
        [Column("likes")]
        public int? Likes { get; set; }

        [ForeignKey(nameof(PId))]
        [InverseProperty(nameof(Picture.Piccomments))]
        public virtual Picture PIdNavigation { get; set; }
        [ForeignKey(nameof(UId))]
        [InverseProperty(nameof(User.Piccomments))]
        public virtual User UIdNavigation { get; set; }
        [InverseProperty(nameof(Commentlike.Piccomment))]
        public virtual ICollection<Commentlike> Commentlikes { get; set; }
    }
}
