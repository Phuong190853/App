namespace Asm.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            Assigned_Course = new HashSet<Assigned_Course>();
            Courses = new HashSet<Course>();
        }

        [Key]
        public int AccID { get; set; }

        [Required(ErrorMessage = "Please enter your name!")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please enter your Email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your Password!")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please select your role!")]
        [StringLength(10)]
        public string Role { get; set; }

        [Required(ErrorMessage = "Please select your date of birth")]
        [Column(TypeName = "date")]
        public DateTime DoB { get; set; }

        [Required(ErrorMessage = "Please enter your PhoneNumber!")]
        [RegularExpression(@"^(\d{4,8})$", ErrorMessage = "Not a valid phone number")]
        [StringLength(50)]
        public string PhoneNo { get; set; }

        public int? ToeicScore { get; set; }

        public virtual Role Role1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assigned_Course> Assigned_Course { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course> Courses { get; set; }
    }
}
