using Microsoft.AspNetCore.Mvc;

namespace cs330_proj1
{
    [ApiController]
    [Route("coregoals")]
    public class CoreGoalsController : ControllerBase
    {
        private readonly ICourseServices service;

        public CoreGoalsController(ICourseServices service)
        {
            this.service = service;
        }

        // GET localhost:5001/coregoals/
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(service.GetAllCoreGoals());
        }

        // GET localhost:5001/coregoals/CG1
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var goal = service.GetCoreGoalById(id);
            if (goal == null) return NotFound();
            return Ok(goal);
        }

        // GET localhost:5001/coregoals/CG1/details
        [HttpGet("{id}/details")]
        public IActionResult GetWithCourses(string id)
        {
            var goal = service.GetCoreGoalWithCoursesById(id);
            if (goal == null) return NotFound();
            return Ok(goal);
        }

        // GET localhost:5001/coregoals/CG1/courses
        [HttpGet("{id}/courses")]
        public IActionResult GetCourses(string id)
        {
            return Ok(service.GetCoursesForCoreGoalById(id));
        }

        // POST localhost:5001/coregoals/
        [HttpPost]
        public IActionResult Create([FromBody] CoreGoal goal)
        {
            var created = service.InsertCoreGoal(goal);
            return Ok(created);
        }

        // PUT localhost:5001/coregoals/CG1
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] CoreGoal goal)
        {
            if (!service.UpdateCoreGoal(id, goal))
                return NotFound();

            return Ok(goal);
        }

        // PUT localhost:5001/coregoals/CG1/courses
        [HttpPut("{id}/courses")]
        public IActionResult AddCourse(string id, [FromBody] Course course)
        {
            if (!service.AddCourseToCoreGoal(id, course))
                return NotFound();

            return Ok(course);
        }

        // DELETE localhost:5001/coregoals/CG1
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!service.DeleteCoreGoal(id))
                return NotFound();

            return Ok();
        }
    }
}