namespace Asm.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Assigned_Course
    {
        public int id { get; set; }

        public int CourseID { get; set; }

        public int TraineeID { get; set; }

        public virtual Account Account { get; set; }

        public virtual Course Course { get; set; }
    }
}
