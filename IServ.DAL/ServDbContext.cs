using IServ.Domain;
using Microsoft.EntityFrameworkCore;

namespace IServ.DAL
{
    public class ServDbContext : DbContext
    {
        public DbSet<University> Universities { get; set; }

        public ServDbContext()
        {
        }
        public ServDbContext(DbContextOptions<ServDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<University>().ToTable("University");
            modelBuilder.Entity<University>().HasKey("UniversityId");
            modelBuilder.Entity<University>().Property(it => it.UniversityId).HasDefaultValueSql("NEXT VALUE FOR UniversityIdSequence");
            modelBuilder.HasSequence<int>("UniversityIdSequence").IncrementsBy(1).HasMin(1).HasMax(100000).StartsAt(1);

            modelBuilder.Entity<University>().HasMany(u => u.WebPages);
            modelBuilder.Entity<University>().HasMany(u => u.WebPageDomains);

            base.OnModelCreating(modelBuilder);
        }
    }
}
