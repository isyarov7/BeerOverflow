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
        ReviewDTO CreateReview(ReviewDTO reviewDTO);
        ReviewDTO UpdateReview(int id, ReviewDTO reviewDTO);
        public bool DeleteReview(int id);
    }
}
