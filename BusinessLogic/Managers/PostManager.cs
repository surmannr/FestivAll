using BL.InterfacesForManagers;
using DAL.InterfacesForRepos;
using DAL.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class PostManager : IPostManager
    {
        private readonly IPostRepository postRepository;

        public PostManager(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }

        // Lekérdezések

        public async Task<PostDto> GetPostByIdAsync(int postId)
        {
            var post = await postRepository.GetPostById(postId);
            return new PostDto(post.PostContent, post.CreationDate, post.EventId, post.UserId);
        }

        public async Task<IReadOnlyCollection<PostDto>> GetPostsAsync()
        {
            var posts = await postRepository.GetAllPosts();
            return posts.Select(p => new PostDto(p.PostContent, p.CreationDate, p.EventId, p.UserId)).ToList();
        }

        public async Task<IReadOnlyCollection<PostDto>> GetPostsByEventIdAsync(int eventId)
        {
            var posts = await postRepository.GetPostsByEventId(eventId);
            return posts.Select(p => new PostDto(p.PostContent, p.CreationDate, p.EventId, p.UserId)).ToList();
        }

        // Létrehozás

        public async Task CreatePostAsync(PostDto newPostDto)
        {
            Post post = new Post()
            {
                PostContent = newPostDto.PostContent,
                CreationDate = newPostDto.CreationDate,
                EventId = newPostDto.EventId,
                UserId = newPostDto.UserId
            };
            await postRepository.CreatePost(post);
        }

        // Törlés

        public async Task DeletePostAsync(int postId)
            => await postRepository.DeletePost(postId);

        // Módosítások

        public async Task ModifyContentAsync(int postId, string postContent)
            => await postRepository.ModifyContent(postId, postContent);
    }
}
