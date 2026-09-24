var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("StudentAPI", client =>
{
    client.BaseAddress = new Uri("https://student-management-prem-bbe6bhfxbeh6buaz.centralindia-01.azurewebsites.net/");
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
