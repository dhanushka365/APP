namespace APP.Models.Domain
{
    public class CourseClassroom
    {
        public int CourseId { get; set; }
        public Course? Course { get; set; }

        public int ClassroomId { get; set; }
        public Classroom? Classroom { get; set; }
    }
}
