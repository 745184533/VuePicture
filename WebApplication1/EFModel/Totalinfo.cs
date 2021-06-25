using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Keyless]
    [Table("totalinfo")]
    public partial class Totalinfo
    {
        [Column("user_num", TypeName = "mediumtext")]
        public string UserNum { get; set; }
        [Column("blog_num", TypeName = "mediumtext")]
        public string BlogNum { get; set; }
        [Column("pic_num", TypeName = "mediumtext")]
        public string PicNum { get; set; }
        [Column("check_num", TypeName = "mediumtext")]
        public string CheckNum { get; set; }
        [Column("order_num", TypeName = "mediumtext")]
        public string OrderNum { get; set; }
    }
}
