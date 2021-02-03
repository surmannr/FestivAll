using DAL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.InterfacesForRepos
{
    public interface IPostRepository
    {
        // Lekérdezések
        Task<PostDto> GetPostById(int postId);
        Task<IReadOnlyCollection<PostDto>> GetPostsByEventId(int eventId);
        Task<IReadOnlyCollection<PostDto>> GetAllPosts();
        // Létrehozás
        Task CreatePost(PostDto newPost);
        // Módosítások
        Task ModifyContent(int postId, string content);
        // Törlés
        Task DeletePost(int postId);
    }
}
