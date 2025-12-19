using System.ComponentModel.DataAnnotations;
namespace WebAppStudent.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Course Name harus diisi")]
        [StringLength(100, ErrorMessage = "Course Name tidak boleh lebih 100 karakter")]
        public string CourseName { get; set; } = string.Empty;

        // Satu Course bisa memiliki banyak Students
        // Navigation
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}