using Microsoft.EntityFrameworkCore;
using WebAppStudent.Data;
using WebAppStudent.Models;
using WebAppStudent.ViewModels;

public class StudentService : IStudentService
{
    private readonly ApplicationDbContext _context;

    public StudentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Student>> GetAllAsync(string? keyword)
    {
        var query = _context.Students
            .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(s =>
                s.Name.Contains(keyword) ||
                s.Email.Contains(keyword));
        }

        return await query.ToListAsync();
    }

    public async Task<Student?> GetByIdWithCoursesAsync(int id)
    {
        return await _context.Students
            .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<StudentCourseBaseVM> GetCreateVMAsync()
    {
        return new StudentCourseBaseVM
        {
            Courses = await _context.Courses.ToListAsync()
        };
    }

    public async Task CreateAsync(StudentCourseBaseVM vm)
    {
        var student = new Student
        {
            Name = vm.Name,
            Email = vm.Email,
            Age = vm.Age
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        var relations = vm.SelectedCourseIds.Select(cid =>
            new StudentCourse
            {
                StudentId = student.Id,
                CourseId = cid
            });

        _context.StudentCourses.AddRange(relations);
        await _context.SaveChangesAsync();
    }

    public async Task<StudentCourseBaseVM?> GetEditVMAsync(int id)
    {
        var student = await _context.Students
            .Include(s => s.StudentCourses)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null) return null;

        return new StudentCourseBaseVM
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            Age = student.Age,
            SelectedCourseIds = student.StudentCourses
                .Select(sc => sc.CourseId)
                .ToList(),
            Courses = await _context.Courses.ToListAsync()
        };
    }

    public async Task UpdateAsync(StudentCourseBaseVM vm)
    {
        var student = await _context.Students
            .Include(s => s.StudentCourses)
            .FirstOrDefaultAsync(s => s.Id == vm.Id);

        if (student == null) return;

        student.Name = vm.Name;
        student.Email = vm.Email;
        student.Age = vm.Age;

        _context.StudentCourses.RemoveRange(student.StudentCourses);

        var relations = vm.SelectedCourseIds.Select(cid =>
            new StudentCourse
            {
                StudentId = student.Id,
                CourseId = cid
            });

        _context.StudentCourses.AddRange(relations);
        await _context.SaveChangesAsync();
    }

    public async Task<Student?> GetDeleteAsync(int id)
    {
        return await GetByIdWithCoursesAsync(id);
    }

    public async Task DeleteAsync(int id)
    {
        var student = await _context.Students
            .Include(s => s.StudentCourses)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null) return;

        _context.StudentCourses.RemoveRange(student.StudentCourses);
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }
}
