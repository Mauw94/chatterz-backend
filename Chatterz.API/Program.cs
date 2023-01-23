using Chatterz.API.InMemoryDb;
using Chatterz.HUBS;

namespace Chatterz.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR().AddMessagePackProtocol();
            builder.Services.AddSingleton(typeof(IChatroomDb), typeof(ChatroomDb));
            builder.Services.AddSingleton<IUsersDb, UsersDb>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // if (app.Environment.IsDevelopment())
            // {
            app.UseSwagger();
            app.UseSwaggerUI();
            //  }

            app.UseCors(config => config
                .SetIsOriginAllowed(origin => true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();
            app.MapHub<ChatHub>("/signalr");
            app.Run();
        }
    }
}