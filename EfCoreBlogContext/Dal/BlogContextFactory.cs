using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EfCoreBlogContext.Dal
{
    class BlogContextFactory : IDesignTimeDbContextFactory<BlogContext>, IBlogContextFactory
    {
        private const string DESIGNTIME_CONNECTIONSTRING = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=BlogDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public BlogContext CreateDbContext(string[] args)
        {
            return CreateDbContext(DESIGNTIME_CONNECTIONSTRING);
        }

        public BlogContext CreateDbContext()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BlogContext"].ConnectionString;
            return CreateDbContext(connectionString);
        }

        private BlogContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BlogContext>().UseSqlServer(connectionString);
            return new BlogContext(optionsBuilder.Options);
        }
    }
}
