using WebAppStudent.Models;

namespace WebAppStudent.Services
{
    public interface IAttendanceService
    {
        List<Attendance> GetAllAttendances();
        Attendance GetAttendanceById(int id);
        void AddAttendance(Attendance attendance);
        void UpdateAttendance(Attendance attendance);
        void DeleteAttendance(int id);
    }
}