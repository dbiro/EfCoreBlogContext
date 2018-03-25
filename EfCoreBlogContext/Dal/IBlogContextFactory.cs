namespace EfCoreBlogContext.Dal
{
    interface IBlogContextFactory
    {
        BlogContext CreateDbContext();
    }
}