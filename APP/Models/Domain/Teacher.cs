namespace APP.Models.Domain
{
    public class Teacher
    {
        public int? TeacherId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public int? SchoolId { get; set; }
        public School? School { get; set; }
        public List<Course>? Courses { get; set; }
    }
}
