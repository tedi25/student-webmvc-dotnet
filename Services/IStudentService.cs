using WebAppStudent.Models;
using WebAppStudent.ViewModels;

public interface IStudentService
{
    Task<List<Student>> GetAllAsync(string? keyword);
    Task<Student?> GetByIdWithCoursesAsync(int id);

    Task<StudentCourseBaseVM> GetCreateVMAsync();
    Task CreateAsync(StudentCourseBaseVM vm);

    Task<StudentCourseBaseVM?> GetEditVMAsync(int id);
    Task UpdateAsync(StudentCourseBaseVM vm);

    Task<Student?> GetDeleteAsync(int id);
    Task DeleteAsync(int id);

    Task PatchAsync(int id, StudentPatchDTOVM vm);

}
