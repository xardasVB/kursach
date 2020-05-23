using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IReviewRepository
    {
        Review AddReview(Review Review);
        Review UpdateReview(Review Review);
        IQueryable<Review> GetReviews();
        Review GetReviewById(int id);
    }
}
