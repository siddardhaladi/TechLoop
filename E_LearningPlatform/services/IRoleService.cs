namespace E_LearningPlatform.services
{
    public interface IRoleService

    {
        Task<int> GetStudentIdByUserIdAsync(int userId);
        Task<int> GetInstructorIdByUserIdAsync(int userId);
    }
}