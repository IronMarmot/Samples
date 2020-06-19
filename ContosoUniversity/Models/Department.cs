using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [StringLength(20,MinimumLength =3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName ="money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Start Date"),DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime StartDate { get; set; }

        public int? InstructorID { get; set; }

        public Instructor Administrator { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
