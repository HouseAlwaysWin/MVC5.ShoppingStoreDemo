using NUnit.Framework;
using ShoppingStoreDomain.Models;
using ShoppingStoreRepository;
using System;
using System.Configuration;

namespace Tests
{
    public class RepositoryTest
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\Projects\\ShoppingStoreDemo\\ShoppingStoreRepository.Test\\ShoppingDBTest.mdf;Integrated Security=True";
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateTable()
        {
            UnitOfWork uow = new UnitOfWork(connectionString);
            uow.ProductRepository.Create(new Product
            {
                Name = "test",
                Price = 100M,
                CreatedDate = DateTime.UtcNow,
                EditedDate = DateTime.UtcNow
            }); ;
            uow.Commit();
            Assert.Pass();
        }
    }
}