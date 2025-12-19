using WebAppStudent.Models;

namespace WebAppStudent.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly List<Attendance> _attendances;

        public AttendanceService()
        {
            _attendances = new List<Attendance>
            {
                new Attendance { Id = 1, Name = "Andi", Date = DateTime.Now, Notes = "Hadir tepat waktu", Status = AttendanceStatus.Present },
                new Attendance { Id = 2, Name = "Budi", Date = DateTime.Now, Notes = "Datang terlambat", Status = AttendanceStatus.Late }
            };
        }

        public List<Attendance> GetAllAttendances()
        {
            return _attendances;
        }

        public Attendance GetAttendanceById(int id)
        {
            return _attendances.FirstOrDefault(s => s.Id == id);
        }
        public void AddAttendance(Attendance attendance)
        {
            attendance.Id = _attendances.Any() ? _attendances.Max(s => s.Id) + 1 : 1;
            _attendances.Add(attendance);
        }
        public void UpdateAttendance(Attendance attendance)
        {
            var existingAttendance = _attendances.FirstOrDefault(s => s.Id == attendance.Id);
            if (existingAttendance != null)
            {
                existingAttendance.Name = attendance.Name;
                existingAttendance.Date = attendance.Date;
                existingAttendance.Notes = attendance.Notes;
                existingAttendance.Status = attendance.Status;
            }
        }

        public void DeleteAttendance(int id)
        {
            _attendances.RemoveAll(s => s.Id == id);
        }

    }
}