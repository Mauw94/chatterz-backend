using Chatterz.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Chatterz.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Chatroom> Chatrooms { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<WordGuesser> WordGuessers { get; set; }
        public DbSet<SpaceInvaders> SpaceInvaders { get; set; }
    }
}
