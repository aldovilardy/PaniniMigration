using System.Data.Entity;

namespace PaniniMigration.DataAccess
{
    public class PaniniDB : DbContext
    {
        public DbSet<Sticker> Stickers { get; set; }
    }
}
