using System.ComponentModel.DataAnnotations;
using WebAppStudent.Models;

namespace WebAppStudent.ViewModels
{
    public class StudentCourseBaseVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name wajib diisi")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email wajib diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        public string Email { get; set; }

        [Range(10, 100, ErrorMessage = "Usia harus antara 10 - 100")]
        public int Age { get; set; }

        // ====== MULTI SELECT COURSE ======

        // [MinLength(1, ErrorMessage = "Pilih minimal satu course")]
        public List<int> SelectedCourseIds { get; set; } = new();

        // untuk render checkbox
        public List<Course> Courses { get; set; } = new();
    }
}
