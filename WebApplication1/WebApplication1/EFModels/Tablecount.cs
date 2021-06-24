using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("tablecount")]
    public partial class Tablecount
    {
        [Key]
        [Column("tableKey")]
        public int TableKey { get; set; }
        [Column("blog")]
        public int? Blog { get; set; }
        [Column("picture")]
        public int? Picture { get; set; }
        [Column("users")]
        public int? Users { get; set; }
    }
}
