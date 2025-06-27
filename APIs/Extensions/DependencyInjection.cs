using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Application.Services;
using Application.Setting;
using Infrastructure.Repositories;
using Infrastructure.Security;


namespace APIs.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepo<>));
            services.AddScoped<LevelService>();
            services.AddScoped<TokenOptions>();
            services.AddScoped<TokenService>();
            services.AddScoped<LoginServices>();
            services.AddScoped<CourseServices>();
            services.AddScoped<SemesterServices>();
            services.AddScoped<DepartmentService>();
            services.AddScoped<CreatedUseresServices>();
            services.AddScoped<OfferedCousreServices>();
            services.AddScoped<IUnitOfWork, UniteOfWork>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ILevelRepositiry, LevelReopsitoriy>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISemesterRepository, SemesterRepository>();
            services.AddScoped<IDepartmentRepositiry, DepartmetRepository>();
            services.AddScoped<ISuperAdminRepository, SuperAdminRepository>();
            services.AddScoped<IOfferedCourseRepository, OfferedCourseRepository>();


            return services;
        }
    }
}
