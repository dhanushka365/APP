namespace APP.Models.Domain
{
    public class School
    {
        public int? SchoolId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public List<Teacher>? Teachers { get; set; }
        public List<Student>? Students { get; set; }
        public List<Course>? Courses { get; set; }
        public List<Classroom>? Classrooms { get; set; }
    }
}
