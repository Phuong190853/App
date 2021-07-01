namespace Asm.Models.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Manage_Context : DbContext
    {
        public Manage_Context()
            : base("name=Manage_Context6")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Assigned_Course> Assigned_Course { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseCategory> CourseCategories { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<Account>()
                .Property(e => e.Role)
                .IsFixedLength();

            modelBuilder.Entity<Account>()
                .Property(e => e.PhoneNo)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Assigned_Course)
                .WithRequired(e => e.Account)
                .HasForeignKey(e => e.TraineeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Courses)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.TrainerID);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Assigned_Course)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseCategory>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.CourseCategory)
                .HasForeignKey(e => e.CatID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleName)
                .IsFixedLength();

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.Role1)
                .HasForeignKey(e => e.Role)
                .WillCascadeOnDelete(false);
        }
    }
}
