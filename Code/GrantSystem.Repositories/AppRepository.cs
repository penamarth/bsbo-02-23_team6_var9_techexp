using GrantSystem.Interfaces;
using GrantSysytem.Domain;
using System;
using System.Collections.Generic;

namespace GrantSystem.Repositories
{
    public class AppRepository : IAppRepository
    {
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

            return new GrantApplication();
        }

        public List<GrantApplication> findByStatus(string status)
        {
            throw new NotImplementedException();
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
    }
}
