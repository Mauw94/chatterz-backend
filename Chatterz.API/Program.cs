using Chatterz.API.Manages.Interfaces;
using Chatterz.API.Manages.Managers;
using Chatterz.DataAccess;
using Chatterz.DataAccess.Interfaces;
using Chatterz.DataAccess.Repositories;
using Chatterz.HUBS;
using Chatterz.Services.Interfaces;
using Chatterz.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace Chatterz.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR().AddMessagePackProtocol();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IChatroomRepository, ChatroomRepository>();
            builder.Services.AddScoped<IChatroomService, ChatroomService>();
            builder.Services.AddScoped<IWordGuesserRepository, WordGuesserRepository>();
            builder.Services.AddScoped<IWordGuesserService, WordGuesserService>();
            builder.Services.AddScoped<ISpaceInvadersRepository, SpaceInvadersRepository>();
            builder.Services.AddScoped<ISpaceInvadersService, SpaceInvadersService>();
            builder.Services.AddScoped<ISignalRManager, SignalRManager>();
            builder.Services.AddScoped<IGameManager, GameManager>();
            builder.Services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            builder.Services.AddScoped<IChatMessageService, ChatMessageService>();

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
            app.MapHub<GameHub>("/signalr_game");
            app.Run();
        }
    }
}