using System;
using System.Collections.Generic;

namespace EfCoreBlogContext.Dal
{
    class Post
    {
        public int Id { get; set; }
                
        public string Title { get; set; }
                
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public int BlogId { get; set; }

        public Blog Blog { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
