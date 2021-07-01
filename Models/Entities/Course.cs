namespace Asm.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            Assigned_Course = new HashSet<Assigned_Course>();
        }

        public int CourseID { get; set; }

        [Required(ErrorMessage = "Please select category of course!")]
        public int CatID { get; set; }

        [Required(ErrorMessage = "Please enter course name!")]
        [StringLength(50)]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Please enter the description!")]
        public string Description { get; set; }

        public int? TrainerID { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assigned_Course> Assigned_Course { get; set; }

        public virtual CourseCategory CourseCategory { get; set; }
    }
}
