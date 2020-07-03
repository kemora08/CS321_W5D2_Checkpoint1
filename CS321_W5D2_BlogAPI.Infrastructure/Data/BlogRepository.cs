using System;
using System.Collections.Generic;
using System.Linq;
using CS321_W5D2_BlogAPI.Core.Models;
using CS321_W5D2_BlogAPI.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CS321_W5D2_BlogAPI.Infrastructure.Data
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _dbContext;
       
        public BlogRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            // TODO: inject AppDbContext
        }

        public IEnumerable<Blog> GetAll()
        {
            // TODO: Retrieve all blgs. Include Blog.User.
            return _dbContext.Blogs
                  .Include(a => a.User)
                   .ToList();
        }

        public Blog Get(int id)
        {
            // TODO: Retrieve the blog by id. Include Blog.User.
            return _dbContext.Blogs
               .Include(a => a.User)
               .SingleOrDefault(b => b.Id == id);
        }

        public Blog Add(Blog item)
        {
            // TODO: Add new blog
            _dbContext.Blogs.Add(item);
            _dbContext.SaveChanges();
            return item;
        }

        public Blog Update(Blog blog)
        {
            // TODO: update blog
            // get the ToDo object in the current list with this id 
            var currentBlog = _dbContext.Blogs.Find(blog.Id);

            // return null if todo to update isn't found
            if (currentBlog == null) return null;

            // NOTE: This method is already completed for you, but note
            // how the property values are copied below.

            // copy the property values from the changed todo into the
            // one in the db. NOTE that this is much simpler than individually
            // copying each property.
            _dbContext.Entry(currentBlog)
                .CurrentValues
                .SetValues(blog);

            // update the todo and save
            _dbContext.Blogs.Update(currentBlog);
            _dbContext.SaveChanges();
            return currentBlog;
        }

        public void Remove(int id)
        {
            // TODO: remove blog
            var currentBlog = _dbContext.Blogs.FirstOrDefault(b => b.Id == id);
            if (currentBlog != null)
            {
                _dbContext.Blogs.Remove(currentBlog);
                _dbContext.SaveChanges();
            }

        }
    }
}
