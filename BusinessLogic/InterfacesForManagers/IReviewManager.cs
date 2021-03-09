using SharedLayer.DTOs;
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
        Task<ReviewDto> CreateReviewAsync(ReviewDto newReviewDto);
        Task DeleteReviewAsync(int reviewId);
        Task<ReviewDto> ModifyStarsAsync(int reviewId, int stars);
        Task<ReviewDto> ModifyContentAsync(int reviewId, string description);

    }
}
