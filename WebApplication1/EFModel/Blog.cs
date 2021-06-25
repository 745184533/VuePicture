using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("blog")]
    public partial class Blog
    {
        public Blog()
        {
            Ownblogs = new HashSet<Ownblog>();
        }

        [Key]
        [Column("b_id")]
        [StringLength(8)]
        public string BId { get; set; }
        [Column("b_date", TypeName = "date")]
        public DateTime? BDate { get; set; }
        [Column("b_type")]
        [StringLength(5)]
        public string BType { get; set; }
        [Column("b_text")]
        [StringLength(100)]
        public string BText { get; set; }

        [InverseProperty(nameof(Ownblog.BIdNavigation))]
        public virtual ICollection<Ownblog> Ownblogs { get; set; }
    }
}
