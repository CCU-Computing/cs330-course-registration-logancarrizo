using cs330_proj1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ICourseRepository, MySQLCourseRepository>();
builder.Services.AddScoped<ICourseServices, CourseServices>();

var app = builder.Build();

app.MapControllers();

app.Run();