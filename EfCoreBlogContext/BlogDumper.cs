using System;
using EfCoreBlogContext.Dal;

namespace EfCoreBlogContext
{
    class BlogDumper : IBlogDumper
    {
        private readonly IOutputWriter outputWriter;

        public BlogDumper(IOutputWriter outputWriter)
        {
            this.outputWriter = outputWriter ?? throw new ArgumentNullException(nameof(outputWriter));
        }

        public void DumpBlog(Blog blog)
        {
            if (blog == null) throw new ArgumentNullException(nameof(blog));
            switch (blog)
            {
                case RssBlog rssBlog:
                    outputWriter.WriteLine($"RssBlog (Id: {rssBlog.Id}, Url: {rssBlog.Url}, RssUrl: {rssBlog.RssUrl})");
                    break;
                case Blog _:
                    outputWriter.WriteLine($"Blog (Id: {blog.Id}, Url: {blog.Url})");
                    break;
            }

            if (blog.Posts != null)
            {
                foreach (var post in blog.Posts)
                {
                    outputWriter.WriteLine($"  Post (Id: {post.Id}, Title: {post.Title}, Content: {post.Content})");
                    if (post.PostTags != null)
                    {
                        foreach (var posttag in post.PostTags)
                        {
                            Tag tag = posttag.Tag;
                            if (tag != null)
                            {
                                Console.WriteLine($"    Tag (Id: {tag.Id}, Name: {tag.Name})");
                            }
                        }
                    }
                }                
            }
        }
    }
}
