using System;
using System.Collections.Generic;
using System.Diagnostics;
using EfCoreBlogContext.Dal;

namespace EfCoreBlogContext
{
    class Program
    {        
        static void Main(string[] args)
        {
            IBlogDumper blogDumper = new BlogDumper(new ConsoleOuputWriter());
            IBlogContextFactory blogContextFactory = new BlogContextFactory();

            using (var demoContext = new DemoDataContext(blogContextFactory))
            {
                demoContext.InsertDemoData();

                IEnumerable<Blog> blogs = demoContext.FetchBlogs();
                foreach(var blog in blogs)
                {
                    blogDumper.DumpBlog(blog);
                }                                
            }

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Halted. Press any key to exit!");
                Console.ReadKey();
            }
        }
    }
}
