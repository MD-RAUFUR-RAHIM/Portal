using Autofac;
using Portal.Application.Services;
using Portal.Domain;
using Portal.Domain.Repositories;
using Portal.Domain.Services;
using Portal.Infrastructure;
using Portal.Infrastructure.Repositories;
namespace Portal.Web
{
    public class WebModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;
        public WebModule(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssembly)
                .InstancePerLifetimeScope();
            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
               .InstancePerLifetimeScope();
            builder.RegisterType<StudentService>().As<IStudentService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<StudentRepository>().As<IStudentRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
