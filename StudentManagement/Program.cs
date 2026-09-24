using Microsoft.EntityFrameworkCore;
using StudentManagement.AzureStorage;
using StudentManagement.DbContextFolder;
using StudentManagement.Repository;
using StudentManagement.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<BlobService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<MyDbConext>((options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection"));
});
    
    
    
    
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
