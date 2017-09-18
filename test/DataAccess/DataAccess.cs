﻿using System;
using System.Linq;
using test.Models;

namespace test.DataAccess
{
    public class DataAccess
    {
        public static void AddBlogToDb()
        {
            using (var db = new BloggingContext())
            {
                db.Blogs.Add(new Blog { Url = "https://github.com/alancapc/test" });
                db.Bloggers.Add(new Blogger { Age = 32, Firstname = "Alan", Surname = "Costa"});
                var removeBlogger = new Blogger{ BloggerId = 1};
                db.Blogs.RemoveRange(db.Blogs.Where(b => b.BlogId > 0 && b.BlogId < 10));
                db.Bloggers.RemoveRange(
                    db.Bloggers.Where(b => b.BloggerId > 0 && b.BloggerId < 10));
                var count = db.SaveChanges();
                Console.WriteLine($"{count} records saved to database");

                Console.WriteLine();
                Console.WriteLine("All blogs in database:");
                foreach (var blog in db.Blogs)
                {
                    Console.WriteLine($" - {blog.Url}");
                }
                foreach (var blogger in db.Bloggers)
                {
                    Console.WriteLine($" - {blogger.Age}, {blogger.Firstname} {blogger.Surname}");
                }
            }
        }
    }
}
