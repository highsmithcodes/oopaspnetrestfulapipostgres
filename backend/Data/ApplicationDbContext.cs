using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet properties for your models
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<ChatRoom> ChatRoom { get; set; }
        public DbSet<UserChatRoom> UserChatRooms { get; set; }


    }
}