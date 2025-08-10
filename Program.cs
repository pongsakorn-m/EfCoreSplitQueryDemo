using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using EfCoreSplitQueryDemo;

var services = new ServiceCollection();

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") 
                       ?? "Server=localhost,1433;Database=SplitQueryDemo;User=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;";

services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Information));

using var serviceProvider = services.BuildServiceProvider();

using var scope = serviceProvider.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

context.Database.EnsureDeleted();
context.Database.EnsureCreated();

if (!context.Blogs.Any())
{
    for (int i = 1; i <= 5; i++)
    {
        var blog = new Blog { Name = $"Blog {i}" };
        for (int j = 1; j <= 100; j++)
        {
            blog.Posts.Add(new Post { Title = $"Post {j} of Blog {i}" });
        }
        context.Blogs.Add(blog);
    }
    context.SaveChanges();
}

Console.WriteLine("\n=== Single Query with Include ===");
var sw = Stopwatch.StartNew();
var blogsInclude = context.Blogs.Include(b => b.Posts).ToList();
sw.Stop();
Console.WriteLine($"Retrieved {blogsInclude.Count} blogs in {sw.ElapsedMilliseconds} ms (Single Query)");

Console.WriteLine("\n=== Split Query with AsSplitQuery ===");
sw.Restart();
var blogsSplit = context.Blogs.Include(b => b.Posts).AsSplitQuery().ToList();
sw.Stop();
Console.WriteLine($"Retrieved {blogsSplit.Count} blogs in {sw.ElapsedMilliseconds} ms (Split Query)");