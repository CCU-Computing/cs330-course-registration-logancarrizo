using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace cs330_proj1
{
    public class MySQLCourseRepository : ICourseRepository
    {
        private readonly string connectionString =
            "server=localhost;userID=csci330user;password=csci330pass;database=CourseRegistration;";

        public bool UpdateCourseByName(string name, Course modifiedCourse)
        {
            using var con = new MySqlConnection(connectionString);
            con.Open();

            string sql = @"UPDATE Courses
                           SET Name=@newName, Title=@title, Credits=@credits, Description=@description
                           WHERE Name=@oldName";

            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@newName", modifiedCourse.Name);
            cmd.Parameters.AddWithValue("@title", modifiedCourse.Title);
            cmd.Parameters.AddWithValue("@credits", modifiedCourse.Credits);
            cmd.Parameters.AddWithValue("@description", modifiedCourse.Description);
            cmd.Parameters.AddWithValue("@oldName", name);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteCourseByName(string name)
        {
            using var con = new MySqlConnection(connectionString);
            con.Open();

            string deleteGoalCourses = "DELETE FROM CoreGoalCourses WHERE CourseName=@name";
            using (var cmd1 = new MySqlCommand(deleteGoalCourses, con))
            {
                cmd1.Parameters.AddWithValue("@name", name);
                cmd1.ExecuteNonQuery();
            }

            string deleteOfferings = "DELETE FROM CourseOfferings WHERE Course=@name";
            using (var cmd2 = new MySqlCommand(deleteOfferings, con))
            {
                cmd2.Parameters.AddWithValue("@name", name);
                cmd2.ExecuteNonQuery();
            }

            string deleteCourse = "DELETE FROM Courses WHERE Name=@name";
            using var cmd3 = new MySqlCommand(deleteCourse, con);
            cmd3.Parameters.AddWithValue("@name", name);

            return cmd3.ExecuteNonQuery() > 0;
        }

        public IEnumerable<CoreGoal> GetAllCoreGoals()
        {
            List<CoreGoal> goals = new List<CoreGoal>();

            using var con = new MySqlConnection(connectionString);
            con.Open();

            string sql = "SELECT * FROM CoreGoals";
            using var cmd = new MySqlCommand(sql, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                goals.Add(new CoreGoal
                {
                    Id = reader["Id"].ToString() ?? "",
                    Name = reader["Name"].ToString() ?? "",
                    Description = reader["Description"].ToString() ?? "",
                    Courses = new List<Course>()
                });
            }

            return goals;
        }

        public CoreGoal? GetCoreGoalById(string id)
        {
            using var con = new MySqlConnection(connectionString);
            con.Open();

            string sql = "SELECT * FROM CoreGoals WHERE Id=@id";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new CoreGoal
                {
                    Id = reader["Id"].ToString() ?? "",
                    Name = reader["Name"].ToString() ?? "",
                    Description = reader["Description"].ToString() ?? "",
                    Courses = new List<Course>()
                };
            }

            return null;
        }

        public CoreGoal? GetCoreGoalWithCoursesById(string id)
        {
            CoreGoal? goal = GetCoreGoalById(id);
            if (goal == null) return null;

            goal.Courses = new List<Course>(GetCoursesForCoreGoalById(id));
            return goal;
        }

        public IEnumerable<Course> GetCoursesForCoreGoalById(string id)
        {
            List<Course> courses = new List<Course>();

            using var con = new MySqlConnection(connectionString);
            con.Open();

            string sql = @"
                SELECT c.Name, c.Title, c.Credits, c.Description
                FROM Courses c
                INNER JOIN CoreGoalCourses cgc ON c.Name = cgc.CourseName
                WHERE cgc.GoalId = @id";

            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                courses.Add(new Course
                {
                    Name = reader["Name"].ToString() ?? "",
                    Title = reader["Title"].ToString() ?? "",
                    Credits = reader["Credits"] == DBNull.Value ? 0 : System.Convert.ToDouble(reader["Credits"]),
                    Description = reader["Description"].ToString() ?? ""
                });
            }

            return courses;
        }

        public CoreGoal InsertCoreGoal(CoreGoal newGoal)
        {
            using var con = new MySqlConnection(connectionString);
            con.Open();

            string sql = "INSERT INTO CoreGoals (Id, Name, Description) VALUES (@id, @name, @description)";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", newGoal.Id);
            cmd.Parameters.AddWithValue("@name", newGoal.Name);
            cmd.Parameters.AddWithValue("@description", newGoal.Description);

            cmd.ExecuteNonQuery();
            return newGoal;
        }

        public bool UpdateCoreGoal(string id, CoreGoal modifiedGoal)
        {
            using var con = new MySqlConnection(connectionString);
            con.Open();

            string sql = @"UPDATE CoreGoals
                           SET Name=@name, Description=@description
                           WHERE Id=@id";

            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", modifiedGoal.Name);
            cmd.Parameters.AddWithValue("@description", modifiedGoal.Description);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool AddCourseToCoreGoal(string id, Course newCourse)
        {
            using var con = new MySqlConnection(connectionString);
            con.Open();

            string sql = "INSERT INTO CoreGoalCourses (GoalId, CourseName) VALUES (@goalId, @courseName)";
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@goalId", id);
            cmd.Parameters.AddWithValue("@courseName", newCourse.Name);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteCoreGoal(string id)
        {
            using var con = new MySqlConnection(connectionString);
            con.Open();

            string deleteJoin = "DELETE FROM CoreGoalCourses WHERE GoalId=@id";
            using (var cmd1 = new MySqlCommand(deleteJoin, con))
            {
                cmd1.Parameters.AddWithValue("@id", id);
                cmd1.ExecuteNonQuery();
            }

            string deleteGoal = "DELETE FROM CoreGoals WHERE Id=@id";
            using var cmd2 = new MySqlCommand(deleteGoal, con);
            cmd2.Parameters.AddWithValue("@id", id);

            return cmd2.ExecuteNonQuery() > 0;
        }
    }
}