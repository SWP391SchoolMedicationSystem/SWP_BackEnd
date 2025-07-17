using System.Text;
using System.Text.Json.Serialization;
using BussinessLayer.Hubs;
using BussinessLayer.IService;
using BussinessLayer.QuartzJob.Job;
using BussinessLayer.QuartzJob.Scheduler;
using BussinessLayer.Service;
using BussinessLayer.Utils;
using BussinessLayer.Utils.Configurations;
using CloudinaryDotNet;
using DataAccessLayer.Entity;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using Quartz;
using SchoolMedicalSystem.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:3000") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
#region Quartz Scheduler Configuration
// SETUP QUARTZ SCHEDULER
builder.Services.AddQuartz();
builder.Services.AddTransient<NotifyScheduler>();
builder.Services.AddQuartzHostedService(opt =>
{
    opt.WaitForJobsToComplete = true;
});
builder.Services.AddTransient<NotifyJob>();
#endregion
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));
var googleclient = builder.Configuration["AppSetting:GoogleClientId"];
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

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddSingleton(provider =>
{
    var config = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;
    var account = new Account(config.CloudName, config.ApiKey, config.ApiSecret);
    return new Cloudinary(account);
});

#region AddScoped
builder.Services.AddScoped<IParentRepository, ParentRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IEmailRepo, EmailRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IParentService, ParentService>();
builder.Services.AddScoped<IBlogRepo, BlogRepo>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IGenericRepository<NotificationParentDetail>, GenericRepository<NotificationParentDetail>>();
builder.Services.AddScoped<IHealthRecordService, HealthRecordService>();
builder.Services.AddScoped<IHealthRecordRepository, HealthRecordRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationRepo, NotificationRepo>();
builder.Services.AddScoped<IGenericRepository<Notification>, GenericRepository<Notification>>();
builder.Services.AddScoped<INotificationParentDetailRepo, NotificationParentDetailRepo>();
builder.Services.AddScoped<INotificationStaffDetailRepo, NotificationStaffDetailRepo>();
builder.Services.AddScoped<IClassRoomRepository, ClassRoomRepository>();
builder.Services.AddScoped<IClassRoomService, ClassroomService>();
builder.Services.AddScoped<IStudentRepo, StudentRepo>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IVaccinationRecordRepository, VaccinationRecordRepository>();
builder.Services.AddScoped<IVaccinationRecordService, VaccinationRecordService>();
builder.Services.AddScoped<IVaccinationEventRepository, VaccinationEventRepository>();
builder.Services.AddScoped<IVaccinationEventService, VaccinationEventService>();
builder.Services.AddScoped<IOtpRepo, OtpRepo>();
builder.Services.AddScoped<IPersonalmedicineService, PersonalmedicineService>();
builder.Services.AddScoped<IPersonalMedicineRepository, PersonalMedicineRepository>();
builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
builder.Services.AddScoped<IMedicineService, MedicineService>();
builder.Services.AddScoped<IMedicineCategoryRepository, MedicineCategoryRepository>();
builder.Services.AddScoped<IMedicineScheduleRepository, MedicineScheduleRepository>();
builder.Services.AddScoped<IHealthCheckRepo, HealthCheckRepository>();
builder.Services.AddScoped<IHealthCheckService, HealthCheckService>();
builder.Services.AddScoped<IScheduleDetailRepo, ScheduleDetailRepo>();
builder.Services.AddScoped<IHealthCheckService, HealthCheckService>();
builder.Services.AddScoped<IHealthCheckRepo, HealthCheckRepository>();
builder.Services.AddScoped<IConsultationTypeRepo, ConsultationTypeRepo>();
builder.Services.AddScoped<IConsultationTypeService, ConsultationTypeService>();
builder.Services.AddScoped<IConsulationRepository, ConsultationRepository>();
builder.Services.AddScoped<IConsultationService, ConsultationRequestService>();
builder.Services.AddScoped<IScheduleDetailService, ScheduleDetailService>();
builder.Services.AddScoped<IHealthCategoryRepo, HealthCategoryRepository>();
builder.Services.AddScoped<IStudentSpecialNeedService, StudentSpecialNeedService>();
builder.Services.AddScoped<IStudentSpecialNeedRepo, StudentSpecialNeedRepo>();
builder.Services.AddScoped<IStudentSpecialNeedService, StudentSpecialNeedService>();
builder.Services.AddScoped<IStudentSpecialNeedCategoryRepo, StudentSpecialNeedCategoryRepo>();
builder.Services.AddScoped<ISpecialNeedCategoryService, SpecialNeedCategoryService>();
builder.Services.AddScoped<IStudentSpecialNeedCategoryRepo, StudentSpecialNeedCategoryRepo>();
builder.Services.AddScoped<IFormRepository, FormRepository>();
builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddScoped<FileHandler>();
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();
app.UseCors("AllowAllOrigins");
app.UseCors("AllowReact");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

//app.UseAuthentication();
app.UseAuthorization();

    app.MapControllers();
app.UseStaticFiles();
app.MapHub<NotificationHub>("/hubs/notifications");
app.Run();
