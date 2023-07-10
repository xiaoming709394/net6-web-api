using Microsoft.EntityFrameworkCore;
using net6_web_api.DB;
using NLog.Extensions.Logging;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//将EF core上下文类注册到了依赖项注入容器
builder.Services.AddDbContext<SchoolContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));//SchoolContext是appsetting的数据库配置参数名称

// 配置NLog
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Logging.AddNLog();

// 配置跨域处理，允许所有来源
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithExposedHeaders("X-Pagination"));
});

var app = builder.Build();

//跨域
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//EF相关的初始化
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<SchoolContext>();
    //新建数据库
    context.Database.EnsureCreated();
    //初始化数据（设定数据库种子）
    DbInitializer.Initialize(context);
}

app.Run();
