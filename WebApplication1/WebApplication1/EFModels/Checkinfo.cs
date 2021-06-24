using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("checkinfo")]
    [Index(nameof(PId), Name = "p_id")]
    public partial class Checkinfo
    {
        public Checkinfo()
        {
            Checks = new HashSet<Check>();
        }

        [Key]
        [Column("c_id")]
        [StringLength(8)]
        public string CId { get; set; }
        [Column("p_id")]
        [StringLength(8)]
        public string PId { get; set; }
        [Column("c_status")]
        [StringLength(2)]
        public string CStatus { get; set; }
        [Column("pass_num")]
        public int? PassNum { get; set; }
        [Column("fail_num")]
        public int? FailNum { get; set; }

        [ForeignKey(nameof(PId))]
        [InverseProperty(nameof(Picture.Checkinfos))]
        public virtual Picture PIdNavigation { get; set; }
        [InverseProperty(nameof(Check.CIdNavigation))]
        public virtual ICollection<Check> Checks { get; set; }
    }
}
