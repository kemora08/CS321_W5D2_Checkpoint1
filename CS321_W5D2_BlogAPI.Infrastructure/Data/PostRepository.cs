using System;
using System.Collections.Generic;
using System.Linq;
using CS321_W5D2_BlogAPI.Core.Models;
using CS321_W5D2_BlogAPI.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CS321_W5D2_BlogAPI.Infrastructure.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _dbContext;
        private object updatedPost;

        public PostRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
            // TODO: inject AppDbContext
        }

        public Post Get(int id)
        {
            // TODO: Implement Get(id). Include related Post and Blog.User
            return _dbContext.Posts
               .Include(a => a.Blog.User)
               .SingleOrDefault(b => b.Id == id);
        }

        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
            // TODO: Implement GetBlogPosts, return all posts for given blog id
            // TODO: Include related Blog and AppUser
            return _dbContext.Posts
                   .Include(a => a.Blog.User)
                    .ToList();
        }

        public Post Add(Post item)
        {
            // TODO: add Post
            _dbContext.Posts.Add(item);
            _dbContext.SaveChanges();
            return item;
        }

        public Post Update(Post Post)
        {
            // TODO: update Post
            // get the ToDo object in the current list with this id 
            var currentPost = _dbContext.Posts.Find(Post.Id);

            // return null if todo to update isn't found
            if (currentPost == null) return null;

            // NOTE: This method is already completed for you, but note
            // how the property values are copied below.

            // copy the property values from the changed todo into the
            // one in the db. NOTE that this is much simpler than individually
            // copying each property.
            _dbContext.Entry(currentPost)
                .CurrentValues
                .SetValues(updatedPost);

            // update the todo and save
            _dbContext.Posts.Update(currentPost);
            _dbContext.SaveChanges();
            return currentPost;
        }

        public IEnumerable<Post> GetAll()
        {
            // TODO: get all posts
            return _dbContext.Posts
                 .Include(a => a.Blog)
                 .ThenInclude(a => a.User)
                  .ToList();
        }

        public void Remove(int id)
        {
            // TODO: remove Post
            var currentPost = _dbContext.Posts.FirstOrDefault(b => b.Id == id);
            if (currentPost != null)
            {
                _dbContext.Posts.Remove(currentPost);
                _dbContext.SaveChanges();
            }
        }

    }
}
