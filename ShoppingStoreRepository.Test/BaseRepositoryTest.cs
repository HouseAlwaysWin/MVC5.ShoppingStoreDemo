// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using ShoppingStoreDomain.Models;

namespace ShoppingStoreRepository.Test
{
    [TestFixture]
    public class BaseRepositoryTest
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\Projects\\ShoppingStoreDemo\\ShoppingStoreRepository.Test\\ShoppingDBTest.mdf;Integrated Security=True";
        [Test]
        public void CanCreateTable()
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
