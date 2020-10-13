using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.Contracts
{
    public interface IReviewService
    {
        ReviewDTO GetReview(int id);
        IEnumerable<ReviewDTO> GetAllReviews();
        public void CreateReview(ReviewDTO reviewDTO);
        public void UpdateReview(int id, ReviewDTO reviewDTO);
        public void DeleteReview(ReviewDTO reviewDTO);
    }
}
