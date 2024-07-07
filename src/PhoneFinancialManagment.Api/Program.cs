using PhoneFinancialManagment.Api.Security;
using PhoneFinancialManagment.Infraestructure.Security;
using PhoneFinancialManagment.Ioc;

namespace PhoneFinancialManagment.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ExternalServicesInjection.Inject(builder.Services, builder.Configuration);
            ServiceInjection.Inject(builder.Services, builder.Configuration);
            DatabaseInjection.Inject(builder.Services, builder.Configuration);
            ValidatorInjection.Inject(builder.Services);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<AuthenticationMiddleware>();
            
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
