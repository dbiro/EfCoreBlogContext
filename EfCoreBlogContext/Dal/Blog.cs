using System.Collections.Generic;

namespace EfCoreBlogContext.Dal
{
    class Blog
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public decimal Rating { get; set; }

        //public BlogType Type { get; set; }

        public List<Post> Posts { get; set; }
    }
}
