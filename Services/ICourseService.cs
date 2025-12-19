using WebAppStudent.Models;

public interface ICourseService
{
    Task<List<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(int id);
    Task CreateAsync(Course course);
    Task UpdateAsync(Course course);
    Task DeleteAsync(int id);
}
