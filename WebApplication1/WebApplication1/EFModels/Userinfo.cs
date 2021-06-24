using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("userinfo")]
    public partial class Userinfo
    {
        [Key]
        [Column("u_id")]
        [StringLength(8)]
        public string UId { get; set; }
        [Column("u_name")]
        [StringLength(20)]
        public string UName { get; set; }
        [Column("birthday", TypeName = "date")]
        public DateTime? Birthday { get; set; }
        [Column("phone_number")]
        [StringLength(12)]
        public string PhoneNumber { get; set; }
        [Column("mail")]
        [StringLength(40)]
        public string Mail { get; set; }
        [Column("message")]
        [StringLength(50)]
        public string Message { get; set; }

        [ForeignKey(nameof(UId))]
        [InverseProperty(nameof(User.Userinfo))]
        public virtual User UIdNavigation { get; set; }
    }
}
