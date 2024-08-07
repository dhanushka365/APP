using System.Diagnostics;

namespace APP.Models.Domain
{
    public class Student
    {
        public int? StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public int? SchoolId { get; set; }
        public School? School { get; set; }
        public List<Enrollment>? Enrollments { get; set; }
        public List<Grade>? Grades { get; set; }
    }
}
