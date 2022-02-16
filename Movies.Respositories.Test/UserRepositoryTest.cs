using System;
using System.Linq;
using Movies.Entities.Entities;
using NUnit.Framework;

namespace Movies.Repositories.Test
{
    public class UserRepositoryTest:BaseRepositoryTest
    {
        [Test]
        public void GetAllTest()
        {
            // Setup
            GenerateDbRecords(10);

            // Act
            var result = UserRepository.GetAll().Result;

            // Assert
            Assert.AreEqual(result.Count(), 10);
        }

        [Test]
        public void GetTest()
        {
            // Setup
            var id = Guid.NewGuid();
            GenerateDbRecord(id);

            // Act
            var result = UserRepository.GetById(id).Result;

            // Assert
            Assert.AreEqual(result.Id, id);
        }

        [Test]
        public void GetNotFoundTest()
        {
            // Setup
            var id = Guid.NewGuid();

            // Act
            var result = UserRepository.GetById(id).Result;

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void CreateTest()
        {
            // Setup
            var id = Guid.NewGuid();
            var model = new User
            {
                Id = id,
                FullName = $"FullName for{ id}",
                Email = $"Email for{ id}",
                PasswordHash = new byte[10],
                PasswordSalt = new byte[5],
                CreationDate = DateTimeOffset.UtcNow,
                ModificationDate = DateTimeOffset.MinValue
            };

            // Act
            var result = UserRepository.Create(model).Result;
            var dbRecord = UserRepository.GetById(model.Id).Result;

            // Assert
            Assert.AreEqual(result, 1);
            Assert.AreEqual(dbRecord.FullName, model.FullName);
            Assert.AreEqual(dbRecord.Email, model.Email);
            Assert.AreEqual(dbRecord.CreationDate, model.CreationDate);
            Assert.AreEqual(dbRecord.ModificationDate, model.ModificationDate);
        }

        [Test]
        public void UpdateTest()
        {
            // Setup
            var id = Guid.NewGuid();
            const string newName = "Updated Name";
            GenerateDbRecord(id);

            var model = UserRepository.GetById(id).Result;
            model.FullName = newName;

            var oldModificationDate = model.ModificationDate;

            // Act
            var result = UserRepository.Update(model).Result;
            var dbRecord = UserRepository.GetById(model.Id).Result;

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(dbRecord.FullName, newName);
            Assert.AreEqual(dbRecord.CreationDate, model.CreationDate);
            Assert.AreNotEqual(dbRecord.ModificationDate, oldModificationDate);
        }

        [Test]
        public void DeleteTest()
        {
            // Setup
            var id = Guid.NewGuid();
            GenerateDbRecord(id);
            var model = UserRepository.GetById(id).Result;

            // Act
            var result = UserRepository.Delete(model).Result;
            var dbRecord = UserRepository.GetById(model.Id).Result;

            // Assert
            Assert.AreEqual(result, 1);
            Assert.IsNull(dbRecord);
        }

        #region Private methods

        private void GenerateDbRecords(int numberRecords)
        {
            for (int i = 0; i < numberRecords; i++)
            {
                var id = Guid.NewGuid();
                GenerateDbRecord(id, i);
            }

        }

        private void GenerateDbRecord(Guid id, int iterator = 0)
        {
            Context.Users.Add(new User
            {
                Id = id,
                FullName = $"FullName for{ id}",
                Email = $"Email for{ id}",
                PasswordHash = new byte[10],
                PasswordSalt = new byte[5],
                CreationDate = DateTimeOffset.UtcNow.AddDays(iterator).AddHours(iterator).AddMinutes(iterator),
                ModificationDate = DateTimeOffset.MinValue
            });

            Context.SaveChanges();
        }

        #endregion

    }
}