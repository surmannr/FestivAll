using DAL.DTOs;
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

        public async Task CreatePost(PostDto newPost)
        {
            if(newPost==null) throw new Exception(ExceptionMessageConstants.NullObject);
            if(PostRepositoryExtension.IsPostParamsNull(newPost)) throw new Exception(ExceptionMessageConstants.RequiredParams);
            Post post = new Post()
            {
                PostContent = newPost.PostContent,
                UserId = newPost.UserId,
                EventId = newPost.EventId,
                CreationDate = newPost.CreationDate
            };
            db.Posts.Add(post);
            await db.SaveChangesAsync();
        }

        public async Task DeletePost(int postId)
        {
            var post = await db.Posts.Where(p => p.Id == postId).FirstOrDefaultAsync();
            if (post != null)
            {
                db.Posts.Remove(post);
                await db.SaveChangesAsync();
            } 
            else new Exception(ExceptionMessageConstants.NullObject);
        }

        public async Task<IReadOnlyCollection<PostDto>> GetAllPosts()
        {
            return await db.Posts.GetPostsList();
        }

        public async Task<PostDto> GetPostById(int postId)
        {
            return await db.Posts.GetPostById(postId);
        }

        public async Task<IReadOnlyCollection<PostDto>> GetPostsByEventId(int eventId)
        {
            return await db.Posts.GetPostsByEventId(eventId);
        }

        public async Task ModifyContent(int postId, string content)
        {
            var post = await db.Posts.Where(p => p.Id == postId).FirstOrDefaultAsync();
            if (post != null)
            {
                post.PostContent = content;
                await db.SaveChangesAsync();
            }
            else new Exception(ExceptionMessageConstants.NullObject);
        }
    }
    internal static class PostRepositoryExtension
    {
        public static bool IsPostParamsNull(PostDto postDto)
        {
            return postDto.EventId == 0 && postDto.UserId == null && postDto.PostContent == null
                && postDto.CreationDate == null;
        }

        public static async Task<IReadOnlyCollection<PostDto>> GetPostsList(this IQueryable<Post> posts)
            => await posts.Select(p => new PostDto(p.PostContent, p.CreationDate, p.EventId, p.UserId)).ToListAsync();

        public static async Task<PostDto> GetPostById(this IQueryable<Post> posts, int postId)
            => await posts.Where(p => p.Id == postId).Select(p => new PostDto(p.PostContent, p.CreationDate, p.EventId, p.UserId)).FirstOrDefaultAsync();

        public static async Task<IReadOnlyCollection<PostDto>> GetPostsByEventId(this IQueryable<Post> posts, int eventId)
            => await posts.Where(p => p.EventId == eventId).Select(p => new PostDto(p.PostContent, p.CreationDate, p.EventId, p.UserId)).ToListAsync();
    }
}
