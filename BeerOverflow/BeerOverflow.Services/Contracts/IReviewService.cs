using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Services.Contracts
{
    public interface IReviewService
    {
        ReviewDTO CreateReview(ReviewDTO review);
    }
}
