using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("follow")]
    [Index(nameof(FollowId), Name = "follow_id")]
    public partial class Follow
    {
        [Key]
        [Column("fans_id")]
        [StringLength(8)]
        public string FansId { get; set; }
        [Key]
        [Column("follow_id")]
        [StringLength(8)]
        public string FollowId { get; set; }

        [ForeignKey(nameof(FansId))]
        [InverseProperty(nameof(User.FollowFans))]
        public virtual User Fans { get; set; }
        [ForeignKey(nameof(FollowId))]
        [InverseProperty(nameof(User.FollowFollowNavigations))]
        public virtual User FollowNavigation { get; set; }
    }
}
