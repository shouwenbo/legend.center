using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Legend.Api.Models
{
    public partial class Test20200518Context : DbContext
    {
        public Test20200518Context()
        {
        }

        public Test20200518Context(DbContextOptions<Test20200518Context> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=172.20.66.253;Initial Catalog=Test20200518;User ID=sa;Password=shyrsql2008$;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasColumnName("userID")
                    .HasComment("用户编号");

                entity.Property(e => e.UserAccount)
                    .IsRequired()
                    .HasColumnName("userAccount")
                    .HasMaxLength(500)
                    .HasComment("用户登录账号");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasColumnName("userPassword")
                    .HasMaxLength(500)
                    .HasComment("用户登录密码");

                entity.Property(e => e.UserPaswordMd5)
                    .IsRequired()
                    .HasColumnName("userPaswordMd5")
                    .HasMaxLength(500)
                    .HasComment("用户密码 (MD5加密)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
