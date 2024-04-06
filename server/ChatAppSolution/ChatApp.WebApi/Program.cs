using ChatApp.WebApi.DataService;
using ChatApp.WebApi.Hubs;

namespace ChatApp.WebApi
{
    public class Program 
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<SharedDB>();
            builder.Services.AddControllers();
            builder.Services.AddAuthorization();
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("reactApp", builder =>
                {
                    builder.WithOrigins(Environment.GetEnvironmentVariable("AllowedCORSWeb"))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            var app = builder.Build();
            app.UseCors("reactApp");
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
            app.MapHub<ChatHub>("/chat");
            

            
            app.Run();
        }
    }
}
