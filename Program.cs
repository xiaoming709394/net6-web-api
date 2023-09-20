using Microsoft.EntityFrameworkCore;
using net6_web_api.DB;
using NLog.Extensions.Logging;
using NLog.Web;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//��EF core��������ע�ᵽ��������ע������
//builder.Services.AddDbContext<SchoolContext>(options =>
// options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));//SchoolContext��appsetting�����ݿ����ò�������
//Mysql
//ĿǰMySql.Data.EntityFrameworkCore��8.0.22��ֻ֧��Microsoft.EntityFrameworkCore(3.11)�����°汾��Microsoft.EntityFrameworkCore(5.0.12)(6.0.0)����֧��
builder.Services.AddDbContext<SchoolContext>(options =>
 options.UseMySQL(builder.Configuration.GetConnectionString("SchoolContext")));

// ����NLog
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Logging.AddNLog();

// ���ÿ���������������Դ
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithExposedHeaders("X-Pagination"));
});

//ע�⣺��Ҫ��dockerfile���Ŷ˿�һ��
builder.WebHost.UseUrls(new[] { "http://*:83" });

var app = builder.Build();

//����
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//������������SwaggerUI
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//EF��صĳ�ʼ��
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<SchoolContext>();
    //�½����ݿ�
    context.Database.EnsureCreated();
    //��ʼ�����ݣ��趨���ݿ����ӣ�
    DbInitializer.Initialize(context);
}

app.Run();
