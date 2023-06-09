using _3ch.Hubs;
using Microsoft.Extensions.FileProviders;
using _3ch.Services;
using _3ch.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using _3ch.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
//builder.Services.AddAuthentication();
//builder.Services.AddAuthorization();
builder.Services.AddHealthChecks();
builder.Services.AddRouting();
builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientPermission", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
// Relation services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAllServices();

// ��������� �������� ApplicationContext � �������� ������� � ����������
//string connection = builder.Configuration.GetConnectionString("connection");
//builder.Services.AddDbContext<ApplicationContext>(options =>
//    options.UseNpgsql(connection));


//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Authorization";
//        options.AccessDeniedPath = "/Authorization";
//    });
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseCookiePolicy();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Files")),
    RequestPath = "/Files"
});
app.UseForwardedHeaders();
app.UseRouting();
app.UseCors("ClientPermission");
app.UseFileServer();
app.MapHub<CommentHub>("/CommentHub");
//app.UseAuthentication();
//app.UseAuthorization();
app.MapControllers();
app.Run();