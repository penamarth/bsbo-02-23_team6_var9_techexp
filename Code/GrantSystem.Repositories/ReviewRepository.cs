using System;
using System.Collections.Generic;
using GrantSystem.Interfaces;
using GrantSysytem.Domain;

namespace GrantSystem.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly List<Review> _reviews = new List<Review>();

        public Review findById(int id)
        {
            Console.WriteLine("=== Вызов ReviewRepository.findById() ===");

            return _reviews.Find(r => r.Id == id);
        }

        public List<Review> findByApplicant(int id)
        {
            Console.WriteLine("=== Вызов ReviewRepository.findByApplicant() ===");

            return _reviews.FindAll(r => r.ExpertId == id);
        }

        public List<Review> findByApplication(int applicationId)
        {
            Console.WriteLine("=== Вызов ReviewRepository.findByApplication() ===");

            return _reviews.FindAll(r => r.ApplicationId == applicationId);
        }

        public Review save(Review review)
        {
            Console.WriteLine("=== Вызов ReviewRepository.save() ===");

            review.Id = _reviews.Count + 1;
            _reviews.Add(review);
            return review;
        }
    }
}

