using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.InterfacesForManagers
{
    public interface IReviewManager
    {
        Task<ReviewDto> GetReviewByIdAsync(int reviewId);
        Task<IReadOnlyCollection<ReviewDto>> GetReviewsAsync();
        Task<IReadOnlyCollection<ReviewDto>> GetReviewsByEventIdAsync(int eventId);
        Task CreateReviewAsync(ReviewDto newReviewDto);
        Task DeleteReviewAsync(int reviewId);
        Task ModifyStarsAsync(int reviewId, int stars);
        Task ModifyContentAsync(int reviewId, string description);

    }
}
