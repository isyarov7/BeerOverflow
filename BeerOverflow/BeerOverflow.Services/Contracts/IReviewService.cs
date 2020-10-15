using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface IReviewService
    {
        ReviewDTO GetReview(int id);
        IEnumerable<ReviewDTO> GetAllReviews();
        public void CreateReview(ReviewDTO reviewDTO);
        public void UpdateReview(int id, ReviewDTO reviewDTO);
        public void DeleteReview(ReviewDTO reviewDTO);
        Task<ReviewDTO> GetReviewAsync(int id);
        Task<ICollection<ReviewDTO>> GetAllReviewsAsync();
        Task<ReviewDTO> CreateReviewAsync(ReviewDTO reviewDTO);
        Task<ReviewDTO> UpdateReviewAsync(ReviewDTO reviewDTO, string name);
        Task<ReviewDTO> DeleteReviewAsync(ReviewDTO reviewDTO);
    }
}
