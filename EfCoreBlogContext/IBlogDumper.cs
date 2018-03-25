using EfCoreBlogContext.Dal;

namespace EfCoreBlogContext
{
    interface IBlogDumper
    {
        void DumpBlog(Blog blog);
    }
}