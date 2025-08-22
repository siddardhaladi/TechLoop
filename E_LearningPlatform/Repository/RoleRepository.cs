using E_LearningPlatform.Data;
using Microsoft.EntityFrameworkCore;

namespace E_LearningPlatform.Repository
{
    public class RoleRepository: IRoleRepository
    {
            private readonly ELearningDbContext _context;
            public RoleRepository(ELearningDbContext context)
            {
                _context = context;
            }

        public async Task<int> GetInstructorIdByUserIdAsync(int userId)
        {
            var instructor = await _context.Instructors.FirstOrDefaultAsync(s => s.UserId == userId);
            return instructor?.InstructorId ?? 0;
        }

        public async Task<int> GetStudentIdByUserIdAsync(int userId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(i => i.UserId == userId);
            return student?.StudentId ?? 0;
        }
    }
}
