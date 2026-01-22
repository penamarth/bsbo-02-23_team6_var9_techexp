using GrantSystem.Interfaces;
using GrantSysytem.Domain;
using System;
using System.Collections.Generic;

namespace GrantSystem.Repositories
{
    public class AppRepository : IAppRepository
    {
        private List<Grant> _grants = new List<Grant>();
        private int _nextGrantId = 1;

        public void delete(BaseUser user)
        {
            throw new NotImplementedException();
        }

        public List<GrantApplication> findByApplicant(int id)
        {
            throw new NotImplementedException();
        }

        public GrantApplication findById(int id)
        {
            Console.WriteLine("=== Вызов AppRepository.findById() ===");

            return new GrantApplication
            {
                Id = id,
                Title = "Исследование",
                Description = "Проект по юмл",
                Status = "UNDER_REVIEW",
                ApplicantId = 501,
                SubmissionDate = DateTime.Now.AddDays(-3),
                reviews = new List<Review>
                {
                    new Review
                    {
                        Id = 1,
                        ApplicationId = id,
                        ExpertId = 10,
                        Score = 8.5f,
                        Comment = "Хороший проект",
                        SubmissionDate = DateTime.Now.AddDays(-2)
                    }
                }
            };
        }

        public List<GrantApplication> findByStatus(string status)
        {
            Console.WriteLine("=== Вызов AppRepository.findByStatus() ===");

            return new List<GrantApplication>
            {
                new GrantApplication
                {
                    Id = 101,
                    Title = "Исследование нейросетевых моделей",
                    Description = "Проект по разработке эффективных архитектур ИИ.",
                    Status = "UNDER_REVIEW",
                    SubmissionDate = DateTime.Now.AddDays(-3),
                    ApplicantId = 501
                },
                new GrantApplication
                {
                    Id = 102,
                    Title = "Экологические технологии переработки",
                    Description = "Разработка нового метода утилизации пластика.",
                    Status = "UNDER_REVIEW",
                    SubmissionDate = DateTime.Now.AddDays(-1),
                    ApplicantId = 502
                }
            };
        }

        public GrantApplication save(GrantApplication app)
        {
            Console.WriteLine("=== Вызов AppRepository.save() ===");

            return app;
        }

        public GrantApplication update(GrantApplication app)
        {
            Console.WriteLine("=== Вызов AppRepository.update() ===");

            return app;
        }

        public long countAll()
        {
            Console.WriteLine("=== Вызов AppRepository.countAll() ===");

            return 156;
        }

        public long countByStatus(string status)
        {
            Console.WriteLine("=== Вызов AppRepository.countByStatus() ===");

            return 42;
        }

        public double sumAmount()
        {
            Console.WriteLine("=== Вызов AppRepository.sumAmount() ===");

            return 1250000;
        }

        public double calculateAverageScore()
        {
            Console.WriteLine("=== Вызов AppRepository.calculateAverageScore() ===");

            return 8.2;
        }

        public int getInvestorGrants(string id)
        {
            Console.WriteLine("=== Вызов AppRepository.getInvestorGrants() ===");

            return 15;
        }

        public double getInvestorTotal(string id)
        {
            Console.WriteLine("=== Вызов AppRepository.getInvestorTotal() ===");

            return 1250000;
        }

        public double avgAmountByInvestor(string id)
        {
            Console.WriteLine("=== Вызов AppRepository.avgAmountByInvestor() ===");

            return 83333;
        }

        public int getUniqueApplicants(string id)
        {
            Console.WriteLine("=== Вызов AppRepository.getUniqueApplicants() ===");

            return 12;
        }

        public void SaveGrant(Grant grant)
        {
            Console.WriteLine("=== Вызов AppRepository.SaveGrant() ===");
            
            if (grant.Id == 0)
            {
                grant.Id = _nextGrantId++;
            }
            _grants.Add(grant);
        }
    }
}
