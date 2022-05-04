using System;
using BlogsAndPosts.Models;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography;
using Castle.Core.Internal;

namespace BlogsAndPosts

{
    class Program
    {
        static void Main(string[] args)
        {
            var choice = "";
            do
            {
                Console.WriteLine("(1) Create Blog \n(2) List Blogs \n(3) Add Post \n(4) Display Posts \n(X) Quit ");
                choice = Console.ReadLine().ToLower();

                if (choice == "1")
                {
                    Console.WriteLine("Enter Blog name: ");
                    var blogName = Console.ReadLine();
                    if (string.IsNullOrEmpty(blogName))
                    {
                        Console.WriteLine("Blog name cannot be empty! Enter Blog name: ");
                        blogName = Console.ReadLine();
                    }

                    var blog = new Blog();
                    blog.Name = blogName;

                    using (var context = new DataContext())
                    {
                        context.Add(blog);
                        context.SaveChanges();
                    }
                }

                else if (choice == "2")
                {
                    using (var context = new DataContext())
                    {
                        Console.WriteLine("Here is the list of blogs");
                        foreach (var b in context.Blogs)
                        {
                            Console.WriteLine($"Blog: {b.BlogId}) {b.Name}");
                        }
                    }
                }

                else if (choice == "3")
                {
                    var post = new Post();
                    Console.WriteLine("Enter your Post title");
                    var postTitle = Console.ReadLine();
                    if (string.IsNullOrEmpty(postTitle))
                    {
                        Console.WriteLine("Post Title cannot be empty! Enter Post Title: ");
                        postTitle = Console.ReadLine();
                    }
                    
                    post.Title = postTitle;
                    
                    Console.WriteLine("Enter Post Content: ");
                    var postContent = Console.ReadLine();
                        if (string.IsNullOrEmpty(postContent))
                        {
                            Console.WriteLine("Content cannot be empty! Enter Post Content: ");
                            postContent = Console.ReadLine();
                        }
                    post.Content = postContent;

                    using (var context = new DataContext())
                    {
                        Console.WriteLine("Here is the list of blogs");
                        foreach (var b in context.Blogs)
                        {
                            Console.WriteLine($"Blog: {b.BlogId}) {b.Name}");
                        }

                        Console.WriteLine("Enter BlogId of Post: ");
                    }
                    var blId = Console.ReadLine();
                        if (string.IsNullOrEmpty(blId))
                        {
                            Console.WriteLine("BlogId cannot be empty! Enter BlogId: ");
                            blId = Console.ReadLine();
                        } 
                        int blogId;
                        while (!int.TryParse(blId, out blogId))
                        {
                            Console.WriteLine("BlogId must be a number! Enter valid BlogId: ");

                        }
                    post.BlogId = blogId;
                    using (var context = new DataContext())
                    {
                        var blog = context.Blogs.FirstOrDefault(x => x.BlogId == blogId);
                        if (blog == null)
                        {
                            Console.WriteLine("Blog does not exist!");
                        }
                        else
                        {
                            context.Posts.Add(post);
                            context.SaveChanges();
                        }
                    }
                }

                else if (choice == "4")
                {
                    using (var context = new DataContext())
                    {
                        Console.WriteLine("Here is the list of blogs");
                        foreach (var b in context.Blogs)
                        {
                            Console.WriteLine($"Blog: {b.BlogId}) {b.Name}");
                        }
                    }

                    Console.WriteLine("Enter BlogId to view posts: ");
                    var blId = Console.ReadLine();
                    if (string.IsNullOrEmpty(blId))
                    {
                        Console.WriteLine("BlogId cannot be empty! Enter BlogId: ");
                        blId = Console.ReadLine();
                    }

                    int blogId;
                    while (!int.TryParse(blId, out blogId))
                    {
                        Console.WriteLine("BlogId must be a number! Enter valid BlogId: ");
                    }
                    using (var context = new DataContext())
                    {
                        var blog = context.Blogs.FirstOrDefault(x => x.BlogId == blogId);
                        if (blog == null)
                        {
                            Console.WriteLine("Blog does not exist!");
                            
                        }
                        else if (blog.Posts.IsNullOrEmpty())
                        {
                            Console.WriteLine($"There are no posts in {blog.Name}");
                        }
                        else
                        {
                            Console.WriteLine($"Posts for Blog: {blog.Name}");
                            foreach (var post in blog.Posts)
                            {
                                Console.WriteLine($"\tPost: {post.PostId}) {post.Title}: {post.Content}");
                            }
                        }

                    }
                }
            } while (choice != "x");
        }
    }
}