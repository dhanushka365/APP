using APP.Models.Domain;

namespace APP.Repository
{
    public interface IClassroomRepository
    {
        List<Classroom> GetClassrooms();
        Classroom GetClassroomById(int id);
        void AddClassroom(Classroom classroom);
        void UpdateClassroom(Classroom classroom);
        void DeleteClassroom(int id);
    }
}
