using Microsoft.AspNetCore.Mvc;
using WebAppStudent.Models;
using WebAppStudent.Services; // Pastikan namespace StudentService/IStudentService
using WebAppStudent.ViewModels;

namespace WebAppStudent.Controllers.Api
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StudentsApiController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsApiController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/StudentsApi
        [HttpGet]

        public async Task<ActionResult<IEnumerable<StudentDtoVM>>> GetStudents([FromQuery] string? keyword = null)
        {
            var students = await _studentService.GetAllAsync(keyword);

            var dtos = students.Select(s => new StudentDtoVM
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Age = s.Age,
                Courses = s.StudentCourses.Select(sc => sc.Course.CourseName).ToList()
            });

            return Ok(dtos);
        }

        // GET: api/StudentsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDtoVM>> GetStudent(int id)
        {
            var student = await _studentService.GetByIdWithCoursesAsync(id);
            if (student == null) return NotFound();

            var dto = new StudentDtoVM
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Age = student.Age,
                Courses = student.StudentCourses.Select(sc => sc.Course.CourseName).ToList()
            };

            return Ok(dto);
        }

        // POST: api/StudentsApi
        [HttpPost]
        public async Task<ActionResult<StudentDtoVM>> PostStudent(StudentCourseBaseVM vm)
        {
            await _studentService.CreateAsync(vm);

            // Ambil student terakhir yang dibuat
            var student = (await _studentService.GetAllAsync(null))
                .OrderByDescending(s => s.Id)
                .FirstOrDefault();

            if (student == null) return BadRequest();

            var dto = new StudentDtoVM
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Age = student.Age,
                Courses = student.StudentCourses.Select(sc => sc.Course.CourseName).ToList()
            };

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, dto);
        }

        // PUT: api/StudentsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, StudentCourseBaseVM vm)
        {
            if (id != vm.Id) return BadRequest();

            await _studentService.UpdateAsync(vm);
            return NoContent();
        }

        // DELETE: api/StudentsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentService.GetByIdWithCoursesAsync(id);
            if (student == null) return NotFound();

            await _studentService.DeleteAsync(id);
            return NoContent();
        }
    }


    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StudentsApiV2Controller : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsApiV2Controller(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/v2/StudentsApi
        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<IEnumerable<StudentSimpleDtoVm>>> GetStudentsV2()
        {
            var students = await _studentService.GetAllAsync(null);
            var result = students.Select(s => new StudentSimpleDtoVm
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email
            });
            return Ok(result);
        }
    }

    [ApiController]
    [Route("api/v{version:apiVersion}/courses")]
    [ApiVersion("3.0")]
    public class CoursesApiV3Controller : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesApiV3Controller(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: api/v3/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
        {
            var courses = await _courseService.GetAllAsync();

            var result = courses.Select(c => new CourseDto
            {
                Id = c.Id,
                CourseName = c.CourseName
            });

            return Ok(result);
        }

        // GET: api/v3/courses/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CourseDto>> GetById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return NotFound();

            return Ok(new CourseDto
            {
                Id = course.Id,
                CourseName = course.CourseName
            });
        }

        // POST: api/v3/courses
        [HttpPost]
        public async Task<ActionResult<CourseDto>> Create([FromBody] CourseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var course = new Course
            {
                CourseName = dto.CourseName
            };

            await _courseService.CreateAsync(course);

            var result = new CourseDto
            {
                Id = course.Id,
                CourseName = course.CourseName
            };

            return CreatedAtAction(nameof(GetById), new { id = course.Id }, result);
        }

        // PUT: api/v3/courses/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CourseDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Id mismatch");

            var existing = await _courseService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.CourseName = dto.CourseName;

            await _courseService.UpdateAsync(existing);

            return NoContent();
        }

        // DELETE: api/v3/courses/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _courseService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _courseService.DeleteAsync(id);
            return NoContent();
        }
    }
}
