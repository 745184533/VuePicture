using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication1.EFModels
{
    public partial class picturecommunityContext : DbContext
    {
        
        public picturecommunityContext(DbContextOptions<picturecommunityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Check> Checks { get; set; }
        public virtual DbSet<Checkinfo> Checkinfos { get; set; }
        public virtual DbSet<Commentlike> Commentlikes { get; set; }
        public virtual DbSet<Download> Downloads { get; set; }
        public virtual DbSet<Favoritepicture> Favoritepictures { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }
        public virtual DbSet<Likespicture> Likespictures { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Orderinfo> Orderinfos { get; set; }
        public virtual DbSet<Orderpic> Orderpics { get; set; }
        public virtual DbSet<Ownblog> Ownblogs { get; set; }
        public virtual DbSet<Owntag> Owntags { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Piccomment> Piccomments { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Publishpicture> Publishpictures { get; set; }
        public virtual DbSet<Tablecount> Tablecounts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Totalinfo> Totalinfos { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Userinfo> Userinfos { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=picturecommunity;uid=root;pwd=526874Lyf;sslmode=none", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("uft8");

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasKey(e => e.BId)
                    .HasName("PRIMARY");

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");
            });

            modelBuilder.Entity<Check>(entity =>
            {
                entity.HasKey(e => new { e.UId, e.CId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");

                entity.HasOne(d => d.CIdNavigation)
                    .WithMany(p => p.Checks)
                    .HasForeignKey(d => d.CId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("checks_ibfk_2");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Checks)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("checks_ibfk_1");
            });

            modelBuilder.Entity<Checkinfo>(entity =>
            {
                entity.HasKey(e => e.CId)
                    .HasName("PRIMARY");

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");

                entity.HasOne(d => d.PIdNavigation)
                    .WithMany(p => p.Checkinfos)
                    .HasForeignKey(d => d.PId)
                    .HasConstraintName("checkInfo_ibfk_1");
            });

            modelBuilder.Entity<Commentlike>(entity =>
            {
                entity.HasKey(e => new { e.UId, e.CommUId, e.PId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Commentlikes)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("commentLike_ibfk_1");

                entity.HasOne(d => d.Piccomment)
                    .WithMany(p => p.Commentlikes)
                    .HasForeignKey(d => new { d.CommUId, d.PId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("commentLike_ibfk_2");
            });

            modelBuilder.Entity<Download>(entity =>
            {
                entity.HasKey(e => new { e.UId, e.PId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
            });

            modelBuilder.Entity<Favoritepicture>(entity =>
            {
                entity.HasKey(e => new { e.UId, e.PId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");

                entity.HasOne(d => d.PIdNavigation)
                    .WithMany(p => p.Favoritepictures)
                    .HasForeignKey(d => d.PId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favoritePicture_ibfk_2");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Favoritepictures)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favoritePicture_ibfk_1");
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.HasKey(e => new { e.FansId, e.FollowId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");

                entity.HasOne(d => d.Fans)
                    .WithMany(p => p.FollowFans)
                    .HasForeignKey(d => d.FansId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("follow_ibfk_1");

                entity.HasOne(d => d.FollowNavigation)
                    .WithMany(p => p.FollowFollowNavigations)
                    .HasForeignKey(d => d.FollowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("follow_ibfk_2");
            });

            modelBuilder.Entity<Likespicture>(entity =>
            {
                entity.HasKey(e => new { e.UId, e.PId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");

                entity.HasOne(d => d.PIdNavigation)
                    .WithMany(p => p.Likespictures)
                    .HasForeignKey(d => d.PId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("likesPicture_ibfk_2");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Likespictures)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("likesPicture_ibfk_1");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => new { e.UId, e.OId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");

                entity.HasOne(d => d.OIdNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orders_ibfk_2");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orders_ibfk_1");
            });

            modelBuilder.Entity<Orderinfo>(entity =>
            {
                entity.HasKey(e => e.OId)
                    .HasName("PRIMARY");

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");
            });

            modelBuilder.Entity<Orderpic>(entity =>
            {
                entity.HasKey(e => new { e.OId, e.PId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");

                entity.HasOne(d => d.OIdNavigation)
                    .WithMany(p => p.Orderpics)
                    .HasForeignKey(d => d.OId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderPic_ibfk_1");

                entity.HasOne(d => d.PIdNavigation)
                    .WithMany(p => p.Orderpics)
                    .HasForeignKey(d => d.PId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderPic_ibfk_2");
            });

            modelBuilder.Entity<Ownblog>(entity =>
            {
                entity.HasKey(e => new { e.UId, e.BId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");

                entity.HasOne(d => d.BIdNavigation)
                    .WithMany(p => p.Ownblogs)
                    .HasForeignKey(d => d.BId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ownBlog_ibfk_2");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Ownblogs)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ownBlog_ibfk_1");
            });

            modelBuilder.Entity<Owntag>(entity =>
            {
                entity.HasKey(e => new { e.PId, e.TagName })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasCharSet("utf8mb4");

                entity.HasOne(d => d.PIdNavigation)
                    .WithMany(p => p.Owntags)
                    .HasForeignKey(d => d.PId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ownTag_ibfk_1");

                entity.HasOne(d => d.TagNameNavigation)
                    .WithMany(p => p.Owntags)
                    .HasForeignKey(d => d.TagName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ownTag_ibfk_2");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PayId)
                    .HasName("PRIMARY");

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");
            });

            modelBuilder.Entity<Piccomment>(entity =>
            {
                entity.HasKey(e => new { e.UId, e.PId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");

                entity.HasOne(d => d.PIdNavigation)
                    .WithMany(p => p.Piccomments)
                    .HasForeignKey(d => d.PId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("picComment_ibfk_2");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Piccomments)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("picComment_ibfk_1");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.HasKey(e => e.PId)
                    .HasName("PRIMARY");

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");
            });

            modelBuilder.Entity<Publishpicture>(entity =>
            {
                entity.HasKey(e => new { e.UId, e.PId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");

                entity.HasOne(d => d.PIdNavigation)
                    .WithMany(p => p.Publishpictures)
                    .HasForeignKey(d => d.PId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("publishPicture_ibfk_2");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Publishpictures)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("publishPicture_ibfk_1");
            });

            modelBuilder.Entity<Tablecount>(entity =>
            {
                entity.HasKey(e => e.TableKey)
                    .HasName("PRIMARY");

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");

                entity.Property(e => e.TableKey).ValueGeneratedNever();
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.TagName)
                    .HasName("PRIMARY");

                entity.HasCharSet("utf8mb4");
            });

            modelBuilder.Entity<Totalinfo>(entity =>
            {
                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PRIMARY");

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");
            });

            modelBuilder.Entity<Userinfo>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PRIMARY");

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");

                entity.HasOne(d => d.UIdNavigation)
                    .WithOne(p => p.Userinfo)
                    .HasForeignKey<Userinfo>(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userInfo_ibfk_1");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PRIMARY");

                entity.HasCharSet("uft8")
                    .UseCollation("utf8_bin");

                entity.HasOne(d => d.UIdNavigation)
                    .WithOne(p => p.Wallet)
                    .HasForeignKey<Wallet>(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("wallet_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
