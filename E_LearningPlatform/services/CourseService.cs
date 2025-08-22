using E_LearningPlatform.Exceptions;
using E_LearningPlatform.Models;
using E_LearningPlatform.Repository;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_LearningPlatform.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllCoursesAsync();
        }

        public async Task CreateCourseAsync(Course course)
        {
            var existingCourses = await _courseRepository.GetAllCoursesAsync();
            if (existingCourses.Any(c => c.Title == course.Title))
            {
                throw new Exception("Course name must be unique.");
            }
            await _courseRepository.CreateCourseAsync(course);
            //

        }

        public async Task<Course> GetCourseByIdAsync(int courseId)
        {
            Course course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course == null)
            {
                throw new DetailsNotFoundException($"Course with id {courseId} does not exist");
            }
            return course;
        }

        public async Task<List<Course>> GetCoursesByInstructorAsync(int instructorId)
        {
            return await _courseRepository.GetCoursesByInstructorAsync(instructorId);
        }

        public async Task UpdateCourseAsync(int courseId, Course course)
        {
            var existingCourse = await _courseRepository.GetCourseByIdAsync(courseId);
            if (existingCourse == null)
            {
                throw new DetailsNotFoundException($"Course with id {course.CourseId} does not exist");
            }
            await _courseRepository.UpdateCourseAsync(courseId, course);
        }

        public async Task DeleteCourseAsync(int courseId)
        {
            Course existingCourse = await _courseRepository.GetCourseByIdAsync(courseId);
            if (existingCourse == null)
            {
                throw new DetailsNotFoundException($"Course with id {courseId} does not exist");
            }
            await _courseRepository.DeleteCourseAsync(courseId);
        }


        public async Task<IEnumerable<Course>> SearchCoursesByTitleAsync(string title)
        {
            return await _courseRepository.SearchCoursesByTitleAsync(title);
        }
        public Task<IEnumerable<Course>> GetCoursesByIdsAsync(IEnumerable<int> courseIds)
        {
            return _courseRepository.GetCoursesByIdsAsync(courseIds);
        }
    }
}
