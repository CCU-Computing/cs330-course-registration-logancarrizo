using System.Collections.Generic;

namespace cs330_proj1
{
    public class CoreGoal
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}