namespace WebAppStudent.ViewModels
{
    public class StudentPatchDTOVM
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int? Age { get; set; }

        // optional: update course sekaligus
        public List<int>? CourseIds { get; set; }
    }
}
