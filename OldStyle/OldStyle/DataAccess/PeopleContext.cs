using Microsoft.EntityFrameworkCore;

namespace DateTimeProblems.DataAccess
{
    public class PeopleContext : DbContext
    {
        public PeopleContext(DbContextOptions<PeopleContext> options)
            :base(options)
        {

        }

        public DbSet<Person> People { get; set; } = null!;
    }
}
