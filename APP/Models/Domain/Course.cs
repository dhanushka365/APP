namespace APP.Models.Domain
{
    public class Course
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public int Credits { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public List<Enrollment>? Enrollments { get; set; }
        public List<Grade>? Grades { get; set; }  // Added missing property
        public List<CourseClassroom>? CourseClassrooms { get; set; }
    }
}
