using _3ch.Hubs;
using Microsoft.Extensions.FileProviders;
using _3ch.Services;

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
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddFileManager();
// добавляем контекст ApplicationContext в качестве сервиса в приложение

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