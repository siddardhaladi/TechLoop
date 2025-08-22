namespace E_LearningPlatform.Repository
{
    public interface IRoleRepository
    {

        Task<int> GetStudentIdByUserIdAsync(int userId);
        Task<int> GetInstructorIdByUserIdAsync(int userId);

    }
}