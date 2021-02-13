using DAL.Exceptions;
using DAL.InterfacesForRepos;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly FestivallDb db;
        public PostRepository(FestivallDb _db)
        {
            db = _db;
        }

        public async Task<Post> CreatePost(Post newPost)
        {
            if(newPost==null) throw new NullReferenceException(ExceptionMessageConstants.NullObject);
            if(PostRepositoryExtension.IsPostParamsNull(newPost)) throw new ArgumentNullException(ExceptionMessageConstants.RequiredParams);
            db.Posts.Add(newPost);
            await db.SaveChangesAsync();
            return newPost;
        }

        public async Task DeletePost(int postId)
        {
            var post = await db.Posts.Where(p => p.Id == postId).FirstOrDefaultAsync();
            if (post != null)
            {
                db.Posts.Remove(post);
                await db.SaveChangesAsync();
            } 
            else throw new NullReferenceException(ExceptionMessageConstants.NullObject);
        }
        
        public async Task<IReadOnlyCollection<Post>> GetAllPosts()
        {
            return await db.Posts.GetPostsList();
        }

        public async Task<Post> GetPostById(int postId)
        {
            return await db.Posts.GetPostById(postId);
        }

        public async Task<IReadOnlyCollection<Post>> GetPostsByEventId(int eventId)
        {
            return await db.Posts.GetPostsByEventId(eventId);
        }
        
        public async Task<Post> ModifyContent(int postId, string content)
        {
            var post = await db.Posts.Where(p => p.Id == postId).FirstOrDefaultAsync();
            if (post != null)
            {
                post.PostContent = content ?? throw new ArgumentNullException(ExceptionMessageConstants.RequiredParams);
                await db.SaveChangesAsync();
                return post;
            }
            else throw new NullReferenceException(ExceptionMessageConstants.NullObject);
        }
    }
    internal static class PostRepositoryExtension
    {
        public static bool IsPostParamsNull(Post post)
        {
            return post.EventId <= 0 || String.IsNullOrEmpty(post.UserId) || post.PostContent == null;
        }

        public static async Task<IReadOnlyCollection<Post>> GetPostsList(this IQueryable<Post> posts)
            => await posts.ToListAsync();

        public static async Task<Post> GetPostById(this IQueryable<Post> posts, int postId)
            => await posts.Where(p => p.Id == postId).FirstOrDefaultAsync();

        public static async Task<IReadOnlyCollection<Post>> GetPostsByEventId(this IQueryable<Post> posts, int eventId)
            => await posts.Where(p => p.EventId == eventId).ToListAsync();
    }
}
