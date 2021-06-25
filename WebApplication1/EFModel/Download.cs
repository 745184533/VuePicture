using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("download")]
    public partial class Download
    {
        [Key]
        [Column("u_id")]
        [StringLength(8)]
        public string UId { get; set; }
        [Key]
        [Column("p_id")]
        [StringLength(8)]
        public string PId { get; set; }
        [Column("price")]
        public int? Price { get; set; }
        [Column("downloadTime", TypeName = "datetime")]
        public DateTime? DownloadTime { get; set; }
    }
}
