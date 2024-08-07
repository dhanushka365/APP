using APP.Data;
using APP.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace APP.Repository
{
    public class ClassroomRepository : IClassroomRepository
    {
        private readonly SchoolContext _context;

        public ClassroomRepository(SchoolContext context)
        {
            _context = context;
        }

        public List<Classroom> GetClassrooms() => _context.Classrooms.Include(c => c.CourseClassrooms).ToList();
        public Classroom GetClassroomById(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.Classrooms.Include(c => c.CourseClassrooms).FirstOrDefault(c => c.ClassroomId == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void AddClassroom(Classroom classroom)
        {
            _context.Classrooms.Add(classroom);
            _context.SaveChanges();
        }
        public void UpdateClassroom(Classroom classroom)
        {
            _context.Classrooms.Update(classroom);
            _context.SaveChanges();
        }
        public void DeleteClassroom(int id)
        {
            var classroom = _context.Classrooms.Find(id);
            if (classroom != null)
            {
                _context.Classrooms.Remove(classroom);
                _context.SaveChanges();
            }
        }
    }
}
