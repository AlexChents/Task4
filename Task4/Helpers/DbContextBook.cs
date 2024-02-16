using Microsoft.EntityFrameworkCore;
using Task4.Models;

namespace Task4.Helpers
{
    public class DbContextBook:DbContext
    {
        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        public DbContextBook()
        {
            Database.EnsureCreated();
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BookConnection"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Author[] authors = new Author[] {
                new Author{
                    Id = Guid.NewGuid(),
                    LastName = "Zeph",
                    FirstName = "Berger",
                    MiddleName = "Ava",
                    Birthday = new DateTime(1910,12,22)
                },
                new Author{
                    Id = Guid.NewGuid(),
                    LastName = "Yuli",
                    FirstName = "Horton",
                    MiddleName = "Brittany",
                    Birthday = new DateTime(1908,11,04)
                },
                new Author{
                    Id = Guid.NewGuid(),
                    LastName = "Lavinia",
                    FirstName = "Byrd",
                    MiddleName = "Anika",
                    Birthday = new DateTime(1973,04,27)
                },
                new Author{
                    Id = Guid.NewGuid(),
                    LastName = "Marvin",
                    FirstName = "Little",
                    MiddleName = "Peter",
                    Birthday = new DateTime(1960,12,20)
                },
                new Author{
                    Id = Guid.NewGuid(),
                    LastName = "Lilah",
                    FirstName = "Velasquez",
                    MiddleName = "Regan",
                    Birthday = new DateTime(1967,12,21)
                }
            };
            modelBuilder.Entity<Author>().HasData(authors);

            Genre[] genres = new Genre[]
            {
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "трагедія"
                },
                new Genre
                {
                    Id= Guid.NewGuid(),
                    Name = "комедія"
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "трагедія"
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "драма"
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "епопея"
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "байка"
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "казка"
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "оповідання"
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "повість"
                },
                new Genre
                {
                    Id = Guid.NewGuid(),
                    Name = "роман"
                }
            };

            modelBuilder.Entity<Genre>().HasData(genres);

            Random random = new Random();
            Book[] books = new Book[30];
            for (int i = 0; i < 30; i++)
            {
                books[i] = new Book
                {
                    Id = Guid.NewGuid(),
                    CountPages = (i + 1) * 20,
                    Name = "НазваКнижки_" + i,
                    GenreId = genres[random.Next(genres.Length)].Id,
                    AuthorId = authors[random.Next(authors.Length)].Id
                };
            }
            modelBuilder.Entity<Book>().HasData(books);
        }
    }
}
