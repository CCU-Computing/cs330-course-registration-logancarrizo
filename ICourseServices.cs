using System.Collections.Generic;

namespace cs330_proj1
{
    public interface ICourseServices
    {
        bool UpdateCourseByName(string name, Course modifiedCourse);
        bool DeleteCourseByName(string name);

        IEnumerable<CoreGoal> GetAllCoreGoals();
        CoreGoal? GetCoreGoalById(string id);
        CoreGoal? GetCoreGoalWithCoursesById(string id);
        IEnumerable<Course> GetCoursesForCoreGoalById(string id);
        CoreGoal InsertCoreGoal(CoreGoal newGoal);
        bool UpdateCoreGoal(string id, CoreGoal modifiedGoal);
        bool AddCourseToCoreGoal(string id, Course newCourse);
        bool DeleteCoreGoal(string id);
    }
}