
using E_LearningPlatform.Repository;

namespace E_LearningPlatform.services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public Task<int> GetInstructorIdByUserIdAsync(int userId)
        {
            return _roleRepository.GetInstructorIdByUserIdAsync(userId);

        }

        public Task<int> GetStudentIdByUserIdAsync(int userId)
        {
            return _roleRepository.GetStudentIdByUserIdAsync(userId);

        }
    }
}
