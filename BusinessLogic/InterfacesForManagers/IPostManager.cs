using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.InterfacesForManagers
{
    public interface IPostManager
    {
        Task<PostDto> GetPostByIdAsync(int postId);
        Task<IReadOnlyCollection<PostDto>> GetPostsAsync();
        Task<IReadOnlyCollection<PostDto>> GetPostsByEventIdAsync(int eventId);
        Task CreatePostAsync(PostDto newPostDto);
        Task DeletePostAsync(int postId);
        Task ModifyContentAsync(int postId, string postContent);
    }
}
