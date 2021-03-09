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
    public class ReviewManager : IReviewManager
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;

        public ReviewManager(IReviewRepository _reviewRepository, IMapper _mapper)
        {
            reviewRepository = _reviewRepository;
            mapper = _mapper;
        }

        // Lekérdezések

        public async Task<ReviewDto> GetReviewByIdAsync(int reviewId)
        {
            var review = await reviewRepository.GetReviewById(reviewId);
            return mapper.Map<ReviewDto>(review);
        }
        public async Task<IReadOnlyCollection<ReviewDto>> GetReviewsAsync()
        {
            var reviews = await reviewRepository.GetAllReviews();
            return mapper.Map<List<ReviewDto>>(reviews);
        }

        public async Task<IReadOnlyCollection<ReviewDto>> GetReviewsByEventIdAsync(int eventId)
        {
            var reviews = await reviewRepository.GetReviewsByEventId(eventId);
            return mapper.Map<List<ReviewDto>>(reviews);
        }

        // Létrehozás

        public async Task<ReviewDto> CreateReviewAsync(ReviewDto newReviewDto)
        {
            Review review = mapper.Map<Review>(newReviewDto);
            var result = await reviewRepository.CreateReview(review);
            return mapper.Map<ReviewDto>(result);
        }

        // Törlés

        public async Task DeleteReviewAsync(int reviewId)
            => await reviewRepository.DeleteReview(reviewId);

        // Módosítások

        public async Task<ReviewDto> ModifyStarsAsync(int reviewId, int stars)
        {
            var result = await reviewRepository.ModifyStars(reviewId, stars);
            return mapper.Map<ReviewDto>(result);
        }

        public async Task<ReviewDto> ModifyContentAsync(int reviewId, string description)
        {
            var result = await reviewRepository.ModifyDescription(reviewId, description);
            return mapper.Map<ReviewDto>(result);
        }
    }
}
