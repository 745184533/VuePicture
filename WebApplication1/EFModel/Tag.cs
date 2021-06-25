using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("tag")]
    public partial class Tag
    {
        public Tag()
        {
            Owntags = new HashSet<Owntag>();
        }

        [Key]
        [Column("tag_name")]
        [StringLength(100)]
        public string TagName { get; set; }

        [InverseProperty(nameof(Owntag.TagNameNavigation))]
        public virtual ICollection<Owntag> Owntags { get; set; }
    }
}
