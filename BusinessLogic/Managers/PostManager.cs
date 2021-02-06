using DAL.DTOs;
using DAL.InterfacesForRepos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class PostManager
    {
        private readonly IPostRepository postRepository;

        public PostManager(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }

        // Lekérdezések

        public async Task<PostDto> GetPostByIdAsync(int postId)
            => await postRepository.GetPostById(postId);

        public async Task<IReadOnlyCollection<PostDto>> GetPostsAsync()
            => await postRepository.GetAllPosts();

        public async Task<IReadOnlyCollection<PostDto>> GetPostsByEventIdAsync(int eventId)
            => await postRepository.GetPostsByEventId(eventId);

        // Létrehozás

        public async Task CreatePostAsync(PostDto newPostDto)
            => await postRepository.CreatePost(newPostDto);

        // Törlés

        public async Task DeletePostAsync(int postId)
            => await postRepository.DeletePost(postId);

        // Módosítások

        public async Task ModifyContentAsync(int postId, string postContent)
            => await postRepository.ModifyContent(postId, postContent);
    }
}
