using System;
using System.Collections.Generic;
using System.Linq;
using EfCoreBlogContext.Dal;
using Microsoft.EntityFrameworkCore;

namespace EfCoreBlogContext
{
    class DemoDataContext : IDisposable
    {
        private readonly IBlogContextFactory blogContextFactory;

        private const string BLOG_URL = "https://myfirstblog.mydomain.io";
        private const string RSS_BLOG_URL = "https://mysecondblog.mydomain.io";
        private const string RSS_BLOG_RSS_URL = "https://mysecondblog.mydomain.io/feed";

        public DemoDataContext(IBlogContextFactory blogContextFactory)
        {
            this.blogContextFactory = blogContextFactory ?? throw new ArgumentNullException(nameof(blogContextFactory));
        }

        public void Dispose()
        {
            RemoveDemoData();
        }

        public void InsertDemoData()
        {
            EnsureBlogNotExists(BLOG_URL);
            EnsureBlogNotExists(RSS_BLOG_RSS_URL);

            var tags = new Tag[]
            {
                new Tag { Name = "tag1" },
                new Tag { Name = "tag2" },
                new Tag { Name = "tag3" }
            };
            using (var context = blogContextFactory.CreateDbContext())
            {
                InsertTags(context, tags);
                InsertBlog(context, tags);
                InsertRssBlog(context, tags);
            }
        }

        public IEnumerable<Blog> FetchBlogs()
        {
            using (var context = blogContextFactory.CreateDbContext())
            {
                return context.Blogs
                    .Include(b => b.Posts)
                    .ThenInclude(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                    .ToList();
            }
        }

        private void InsertTags(BlogContext context, IEnumerable<Tag> tags)
        {
            context.Tags.AddRange(tags);
            context.SaveChanges();
        }

        private void InsertBlog(BlogContext context, IList<Tag> tags)
        {            
            var post1 = new Post();
            post1.CreationDate = DateTime.UtcNow;
            post1.Title = "Post1 title of first blog";
            post1.Content = "Post1 content";
            post1.PostTags = new List<PostTag>
            {
                new PostTag { Post = post1, Tag = tags[0] },
                new PostTag { Post = post1, Tag = tags[2] }
            };

            var post2 = new Post();
            post2.CreationDate = DateTime.UtcNow;
            post2.Title = "Post2 title of first blog";
            post2.Content = "Post2 content";
            post2.PostTags = new List<PostTag>
            {
                new PostTag { Post = post2, Tag = tags[1] },
                new PostTag { Post = post2, Tag = tags[2] }
            };

            var blog = new Blog();
            blog.Url = BLOG_URL;
            blog.Rating = 4.123456789m;
            blog.Posts = new List<Post> { post1, post2 };

            context.Blogs.Add(blog);
            context.SaveChanges();
        }

        private void InsertRssBlog(BlogContext context, IList<Tag> tags)
        {
            var post1 = new Post();
            post1.CreationDate = DateTime.UtcNow;
            post1.Title = "Post1 title of rss blog";
            post1.Content = "Post1 content if rss blog";
            post1.PostTags = new List<PostTag>
                {
                    new PostTag { Post = post1, Tag = tags[0] }                    
                };

            var rssBlog = new RssBlog();
            rssBlog.Url = RSS_BLOG_URL;
            rssBlog.Rating = 4.123456789m;
            rssBlog.RssUrl = RSS_BLOG_RSS_URL;
            rssBlog.Posts = new List<Post> { post1 };

            context.Blogs.Add(rssBlog);
            context.SaveChanges();
        }

        private void EnsureBlogNotExists(string url)
        {
            using (var context = blogContextFactory.CreateDbContext())
            {
                var blog = context.Blogs.SingleOrDefault(b => b.Url == url);
                if (blog != null)
                {
                    context.Blogs.Remove(blog);
                    context.SaveChanges();
                }
            }
        }

        private void RemoveDemoData()
        {
            RemoveBlogs();
            RemoveTags();
        }

        private void RemoveBlogs()
        {
            var blogUrls = new string[] { BLOG_URL, RSS_BLOG_URL };
            using (var context = blogContextFactory.CreateDbContext())
            {
                var blogs = context.Blogs
                    .Where(b => blogUrls.Contains(b.Url))
                    .ToList();
                context.Blogs.RemoveRange(blogs);
                context.SaveChanges();
            }
        }

        private void RemoveTags()
        {
            using (var context = blogContextFactory.CreateDbContext())
            {
                var tags = context.Tags.ToList();
                context.Tags.RemoveRange(tags);
                context.SaveChanges();
            }
        }
    }
}
