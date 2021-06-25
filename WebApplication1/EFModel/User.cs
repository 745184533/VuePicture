using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApplication1.EFModels
{
    [Table("users")]
    public partial class User
    {
        public User()
        {
            Checks = new HashSet<Check>();
            Commentlikes = new HashSet<Commentlike>();
            Favoritepictures = new HashSet<Favoritepicture>();
            FollowFans = new HashSet<Follow>();
            FollowFollowNavigations = new HashSet<Follow>();
            Likespictures = new HashSet<Likespicture>();
            Orders = new HashSet<Order>();
            Ownblogs = new HashSet<Ownblog>();
            Piccomments = new HashSet<Piccomment>();
            Publishpictures = new HashSet<Publishpicture>();
        }

        [Key]
        [Column("u_id")]
        [StringLength(8)]
        public string UId { get; set; }
        [Column("u_password")]
        [StringLength(10)]
        public string UPassword { get; set; }
        [Column("u_name")]
        [StringLength(20)]
        public string UName { get; set; }
        [Column("u_type")]
        [StringLength(2)]
        public string UType { get; set; }
        [Column("u_status")]
        [StringLength(2)]
        public string UStatus { get; set; }
        [Column("create_time", TypeName = "date")]
        public DateTime? CreateTime { get; set; }

        [InverseProperty("UIdNavigation")]
        public virtual Userinfo Userinfo { get; set; }
        [InverseProperty("UIdNavigation")]
        public virtual Wallet Wallet { get; set; }
        [InverseProperty(nameof(Check.UIdNavigation))]
        public virtual ICollection<Check> Checks { get; set; }
        [InverseProperty(nameof(Commentlike.UIdNavigation))]
        public virtual ICollection<Commentlike> Commentlikes { get; set; }
        [InverseProperty(nameof(Favoritepicture.UIdNavigation))]
        public virtual ICollection<Favoritepicture> Favoritepictures { get; set; }
        [InverseProperty(nameof(Follow.Fans))]
        public virtual ICollection<Follow> FollowFans { get; set; }
        [InverseProperty(nameof(Follow.FollowNavigation))]
        public virtual ICollection<Follow> FollowFollowNavigations { get; set; }
        [InverseProperty(nameof(Likespicture.UIdNavigation))]
        public virtual ICollection<Likespicture> Likespictures { get; set; }
        [InverseProperty(nameof(Order.UIdNavigation))]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty(nameof(Ownblog.UIdNavigation))]
        public virtual ICollection<Ownblog> Ownblogs { get; set; }
        [InverseProperty(nameof(Piccomment.UIdNavigation))]
        public virtual ICollection<Piccomment> Piccomments { get; set; }
        [InverseProperty(nameof(Publishpicture.UIdNavigation))]
        public virtual ICollection<Publishpicture> Publishpictures { get; set; }
    }
}
