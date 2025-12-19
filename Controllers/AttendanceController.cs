using Microsoft.AspNetCore.Mvc;
using WebAppStudent.Models;
using WebAppStudent.Services;

namespace WebAppStudent.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _attendanceService;

        // Dependency Injection melalui konstruktor
        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        // GET: Attendance
        public IActionResult Index()
        {
            var attendances = _attendanceService.GetAllAttendances();
            return View(attendances);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                // Simpan data attendance ke database atau lakukan proses lainnya
                _attendanceService.AddAttendance(attendance);
                return RedirectToAction(nameof(Index));
            }
            return View(attendance);
        }

        // GET: Attendance/Edit/{id}
        public IActionResult Edit(int id)
        {
            var attendance = _attendanceService.GetAttendanceById(id);
            if (attendance == null)
            {
                return NotFound();
            }
            return View(attendance);
        }

        // POST: Attendance/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _attendanceService.UpdateAttendance(attendance);
                return RedirectToAction(nameof(Index));
            }
            return View(attendance);
        }

        // GET: Attendance/Delete/{id}
        public IActionResult Delete(int id)
        {
            var attendance = _attendanceService.GetAttendanceById(id);
            if (attendance == null)
            {
                return NotFound();
            }
            return View(attendance);
        }

        // POST: Attendance/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _attendanceService.DeleteAttendance(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Attendance/Details/{id}
        public IActionResult Details(int id)
        {
            var attendance = _attendanceService.GetAttendanceById(id);
            if (attendance == null)
            {
                return NotFound();
            }
            return View(attendance);
        }
    }
}