using AutoMapper;
using BL.InterfacesForManagers;
using DAL.InterfacesForRepos;
using DAL.Models;
using SharedLayer.DTOs;
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
        private readonly IMapper mapper;

        public PostManager(IPostRepository _postRepository, IMapper _mapper)
        {
            postRepository = _postRepository;
            mapper = _mapper;
        }

        // Lekérdezések

        public async Task<PostDto> GetPostByIdAsync(int postId)
        {
            var post = await postRepository.GetPostById(postId);
            return mapper.Map<PostDto>(post);
        }

        public async Task<IReadOnlyCollection<PostDto>> GetPostsAsync()
        {
            var posts = await postRepository.GetAllPosts();
            return mapper.Map<List<PostDto>>(posts);
        }

        public async Task<IReadOnlyCollection<PostDto>> GetPostsByEventIdAsync(int eventId)
        {
            var posts = await postRepository.GetPostsByEventId(eventId);
            return mapper.Map<List<PostDto>>(posts);
        }

        // Létrehozás

        public async Task<PostDto> CreatePostAsync(PostDto newPostDto)
        {
            Post post = mapper.Map<Post>(newPostDto);
            var result = await postRepository.CreatePost(post);
            return mapper.Map<PostDto>(result);
        }

        // Törlés

        public async Task DeletePostAsync(int postId)
            => await postRepository.DeletePost(postId);

        // Módosítások

        public async Task<PostDto> ModifyContentAsync(int postId, string postContent)
        {
            var result = await postRepository.ModifyContent(postId, postContent);
            return mapper.Map<PostDto>(result);
        }
    }
}
