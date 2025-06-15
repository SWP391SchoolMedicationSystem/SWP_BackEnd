using System.Text;
using System.Text.Json.Serialization;
using BussinessLayer.IService;
using BussinessLayer.Service;
using BussinessLayer.Utils.Configurations;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolMedicalSystem.Services.EmailService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));
var secretkey = builder.Configuration["AppSetting:SecretKey"];
if (string.IsNullOrEmpty(secretkey))
{
    throw new InvalidOperationException("AppSetting failed ");
}
var secretKeyByte = Encoding.UTF8.GetBytes(secretkey);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
    (options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyByte),
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDbContext<SchoolMedicalSystemContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SchoolMedicalSystemContext") 
        ?? throw new InvalidOperationException("Connection string 'SchoolMedicalSystemContext' not found.")));
builder.Services.AddScoped<IParentRepository, ParentRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailRepo, EmailRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<INotificationRepo, NotificationRepo>();
builder.Services.AddScoped<INotificationParentDetailRepo, NotificationParentDetailRepo>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IParentService, ParentService>();
builder.Services.AddScoped<IHealthRecordService, HealthRecordService>();
builder.Services.AddScoped<IHealthRecordRepository, HealthRecordRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IGenericRepository<Notification>, GenericRepository<Notification>>();
builder.Services.AddScoped<IGenericRepository<Parent>, GenericRepository<Parent>>();
builder.Services.AddScoped<IGenericRepository<NotificationParentDetail>, GenericRepository<NotificationParentDetail>>();
builder.Services.AddScoped<IParentRepository, ParentRepository>();
builder.Services.AddScoped<IBlogRepo, BlogRepo>();
builder.Services.AddScoped<IBlogService, BlogService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
