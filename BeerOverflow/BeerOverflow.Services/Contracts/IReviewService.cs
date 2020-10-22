using BeerOverflow.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflow.Services.Contracts
{
    public interface IReviewService
    {
        Task<ReviewDTO> GetReviewAsync(int id);
        Task<ICollection<ReviewDTO>> GetAllReviewsAsync();
        Task<ReviewDTO> CreateReviewAsync(ReviewDTO reviewDTO);
        Task<ReviewDTO> UpdateReviewAsync(int id, ReviewDTO reviewDTO);
        Task<ReviewDTO> DeleteReviewAsync(int id);
    }
}
