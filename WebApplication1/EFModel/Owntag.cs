using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("owntag")]
    [Index(nameof(TagName), Name = "tag_name")]
    public partial class Owntag
    {
        [Key]
        [Column("p_id")]
        [StringLength(8)]
        public string PId { get; set; }
        [Key]
        [Column("tag_name")]
        [StringLength(100)]
        public string TagName { get; set; }

        [ForeignKey(nameof(PId))]
        [InverseProperty(nameof(Picture.Owntags))]
        public virtual Picture PIdNavigation { get; set; }
        [ForeignKey(nameof(TagName))]
        [InverseProperty(nameof(Tag.Owntags))]
        public virtual Tag TagNameNavigation { get; set; }
    }
}
