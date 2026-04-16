using Xunit;
using cs330_proj1;
using System.Collections.Generic;

public class CourseRegistrationTests
{
    [Fact]
    public void GetCourses_Returns_All_Available_Courses()
    {
        CourseServices service = new CourseServices();

        List<Course> result = service.getCourses();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void GetCourses_Contains_Known_Course()
    {
        CourseServices service = new CourseServices();

        List<Course> result = service.getCourses();

        Assert.Contains(result, c => c.Name.StartsWith("CSCI") || c.Name.StartsWith("ENGL") || c.Name.StartsWith("MATH"));
    }

    [Fact]
    public void GetCourseOfferingsBySemester_Returns_Only_Requested_Semester()
    {
        CourseServices service = new CourseServices();
        string semester = "Fall 2024";

        List<CourseOffering> result = service.getCourseOfferingsBySemester(semester);

        Assert.NotNull(result);
        Assert.All(result, o => Assert.Equal(semester, o.Semester));
    }

    [Fact]
    public void GetCourseOfferingsBySemester_Returns_Empty_List_For_Invalid_Semester()
    {
        CourseServices service = new CourseServices();
        string semester = "Winter 2099";

        List<CourseOffering> result = service.getCourseOfferingsBySemester(semester);

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void GetCourseOfferingsBySemesterAndDept_Returns_Only_Requested_Semester_And_Department()
    {
        CourseServices service = new CourseServices();
        string semester = "Fall 2024";
        string dept = "CSCI";

        List<CourseOffering> result = service.getCourseOfferingsBySemesterAndDept(semester, dept);

        Assert.NotNull(result);
        Assert.All(result, o =>
        {
            Assert.Equal(semester, o.Semester);
            Assert.StartsWith(dept, o.TheCourse.Name);
        });
    }

    [Fact]
    public void GetCourseOfferingsBySemesterAndDept_Returns_Empty_List_For_Invalid_Department()
    {
        CourseServices service = new CourseServices();
        string semester = "Fall 2024";
        string dept = "ZZZ";

        List<CourseOffering> result = service.getCourseOfferingsBySemesterAndDept(semester, dept);

        Assert.NotNull(result);
        Assert.Empty(result);
    }
}