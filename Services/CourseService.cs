using Microsoft.EntityFrameworkCore;
using WebAppStudent.Data;
using WebAppStudent.Models;

public class CourseService : ICourseService
{
    private readonly ApplicationDbContext _context;

    public CourseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Course>> GetAllAsync()
    {
        return await _context.Courses
            .Include(c => c.StudentCourses) // aman kalau nanti mau count
            .ToListAsync();
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        return await _context.Courses.FindAsync(id);
    }

    public async Task CreateAsync(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var course = await _context.Courses
            .Include(c => c.StudentCourses)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null) return;

        // hapus relasi pivot saja
        _context.StudentCourses.RemoveRange(course.StudentCourses);
        _context.Courses.Remove(course);

        await _context.SaveChangesAsync();
    }
}
