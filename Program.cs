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

//��EF core��������ע�ᵽ��������ע������
builder.Services.AddDbContext<SchoolContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));//SchoolContext��appsetting�����ݿ����ò�������

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

var app = builder.Build();

//����
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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
