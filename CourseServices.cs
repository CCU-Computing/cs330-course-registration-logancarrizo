using System.Collections.Generic;

namespace cs330_proj1
{
    public class CourseServices : ICourseServices
    {
        private readonly ICourseRepository repo;

        public CourseServices(ICourseRepository repo)
        {
            this.repo = repo;
        }

        public bool UpdateCourseByName(string name, Course modifiedCourse)
        {
            return repo.UpdateCourseByName(name, modifiedCourse);
        }

        public bool DeleteCourseByName(string name)
        {
            return repo.DeleteCourseByName(name);
        }

        public IEnumerable<CoreGoal> GetAllCoreGoals()
        {
            return repo.GetAllCoreGoals();
        }

        public CoreGoal? GetCoreGoalById(string id)
        {
            return repo.GetCoreGoalById(id);
        }

        public CoreGoal? GetCoreGoalWithCoursesById(string id)
        {
            return repo.GetCoreGoalWithCoursesById(id);
        }

        public IEnumerable<Course> GetCoursesForCoreGoalById(string id)
        {
            return repo.GetCoursesForCoreGoalById(id);
        }

        public CoreGoal InsertCoreGoal(CoreGoal newGoal)
        {
            return repo.InsertCoreGoal(newGoal);
        }

        public bool UpdateCoreGoal(string id, CoreGoal modifiedGoal)
        {
            return repo.UpdateCoreGoal(id, modifiedGoal);
        }

        public bool AddCourseToCoreGoal(string id, Course newCourse)
        {
            return repo.AddCourseToCoreGoal(id, newCourse);
        }

        public bool DeleteCoreGoal(string id)
        {
            return repo.DeleteCoreGoal(id);
        }
    }
}