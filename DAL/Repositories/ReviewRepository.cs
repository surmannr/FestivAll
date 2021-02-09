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
    public class ReviewRepository : IReviewRepository
    {
        private readonly FestivallDb db;
        public ReviewRepository(FestivallDb _db)
        {
            db = _db;
        }

        public async Task CreateReview(Review newReview)
        {
            if(newReview == null) throw new Exception(ExceptionMessageConstants.NullObject);
            if(ReviewRepositoryExtension.IsReviewParamsNull(newReview)) throw new Exception(ExceptionMessageConstants.RequiredParams);
            db.Reviews.Add(newReview);
            await db.SaveChangesAsync();
        }

        public async Task DeleteReview(int reviewId)
        {
            var review = await db.Reviews.Where(r => r.Id == reviewId).FirstOrDefaultAsync();
            if (review != null)
            {
                db.Reviews.Remove(review);
                await db.SaveChangesAsync();
            } 
            else throw new Exception(ExceptionMessageConstants.NullObject);
        }

        public async Task<IReadOnlyCollection<Review>> GetAllReviews()
        {
            return await db.Reviews.GetReviewsList();
        }

        public async Task<Review> GetReviewById(int reviewId)
        {
            return await db.Reviews.GetReviewById(reviewId);
        }

        public async Task<IReadOnlyCollection<Review>> GetReviewsByEventId(int eventId)
        {
            return await db.Reviews.GetReviewsByEventId(eventId);
        }

        public async Task ModifyDescription(int reviewId, string newDescription)
        {
            var review = await db.Reviews.Where(r => r.Id == reviewId).FirstOrDefaultAsync();
            if (review != null)
            {
                review.Description = newDescription;
                await db.SaveChangesAsync();
            }
            else throw new Exception(ExceptionMessageConstants.NullObject);
        }

        public async Task ModifyStars(int reviewId, int newStars)
        {
            var review = await db.Reviews.Where(r => r.Id == reviewId).FirstOrDefaultAsync();
            if (review != null)
            {
                review.Stars = newStars;
                await db.SaveChangesAsync();
            }
            else throw new Exception(ExceptionMessageConstants.NullObject);
        }
    }
    internal static class ReviewRepositoryExtension
    {
        public static bool IsReviewParamsNull(Review review)
        {
            return review.EventId == 0 && review.UserId == null
                && review.Stars == 0 && review.Description == null;
        }

        public static async Task<IReadOnlyCollection<Review>> GetReviewsList(this IQueryable<Review> reviews)
            => await reviews.ToListAsync();

        public static async Task<Review> GetReviewById(this IQueryable<Review> reviews, int reviewId)
            => await reviews.Where(r => r.Id == reviewId).FirstOrDefaultAsync();

        public static async Task<IReadOnlyCollection<Review>> GetReviewsByEventId(this IQueryable<Review> reviews, int eventId)
            => await reviews.Where(r => r.EventId == eventId).ToListAsync();
    }
}
