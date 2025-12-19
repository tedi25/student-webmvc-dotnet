using System.ComponentModel.DataAnnotations;
namespace WebAppStudent.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name harus diisi")]
        [StringLength(100, ErrorMessage = "Name tidak boleh lebih 100 karakter")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email harus diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        public string Email { get; set; } = string.Empty;

        [Range(18, 60, ErrorMessage = "Age harus diantara 18 sampai 60")]
        public int Age { get; set; }

        // Navigation
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
