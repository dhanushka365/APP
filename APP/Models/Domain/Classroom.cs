namespace APP.Models.Domain
{
    public class Classroom
    {
        public int ClassroomId { get; set; }
        public string? RoomNumber { get; set; }
        public int Capacity { get; set; }
        public List<CourseClassroom>? CourseClassrooms { get; set; }
    }
}
