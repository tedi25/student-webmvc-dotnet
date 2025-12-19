using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppStudent.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama harus diisi")]
        [StringLength(100, ErrorMessage = "Nama maksimal 100 karakter")]
        [Display(Name = "Nama")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tanggal harus diisi")]
        [DataType(DataType.Date)]
        [Display(Name = "Tanggal Kehadiran")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Catatan harus diisi")]
        [StringLength(100, ErrorMessage = "Catatan maksimal 100 karakter")]
        [Display(Name = "Catatan")]
        public string Notes { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status harus dipilih")]
        [Display(Name = "Status Kehadiran")]
        public AttendanceStatus Status { get; set; }
    }

    public enum AttendanceStatus
    {
        Present,
        Late,
        Sick,
        Absent
    }
}