using Microsoft.EntityFrameworkCore;

namespace IServ.ETL.DAL
{
    public class ServDbContext : DbContext
    {
        public DbSet<University> Universities { get; set; }
        public DbSet<UniversityRawData> UniversityRawDatas { get; set; }
        public DbSet<Country> Countries { get; set; }

        public ServDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=IServApp;Integrated Security=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<University>().ToTable("University");
            modelBuilder.Entity<University>().HasKey("UniversityId");
            modelBuilder.Entity<University>().Property(it => it.UniversityId).HasDefaultValueSql("NEXT VALUE FOR UniversityIdSequence");
            modelBuilder.HasSequence<int>("UniversityIdSequence").IncrementsBy(1).HasMin(1).HasMax(100000).StartsAt(1);

            modelBuilder.Entity<UniversityRawData>().ToTable("UniversityRawData");
            modelBuilder.Entity<UniversityRawData>().HasKey("UniversityRawDataId");
            modelBuilder.Entity<UniversityRawData>().Property(it => it.UniversityRawDataId).HasDefaultValueSql("NEXT VALUE FOR UniversityRawDataIdSequence");
            modelBuilder.HasSequence<int>("UniversityRawDataIdSequence").IncrementsBy(1).HasMin(1).HasMax(100000).StartsAt(1);

            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<Country>().HasKey("CountryId");
            modelBuilder.Entity<Country>().Property(it => it.CountryId).HasDefaultValueSql("NEXT VALUE FOR CountryIdSequence");
            modelBuilder.HasSequence<int>("CountryIdSequence").IncrementsBy(1).HasMin(1).HasMax(100000).StartsAt(1);

            base.OnModelCreating(modelBuilder);
        }
    }
}
