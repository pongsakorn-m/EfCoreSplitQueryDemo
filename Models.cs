
namespace EfCoreSplitQueryDemo
{
    public class Blog
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int BlogId { get; set; }
        public Blog Blog { get; set; } = null!;
    }
}
    