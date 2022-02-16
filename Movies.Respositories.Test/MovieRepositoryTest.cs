using System;
using Movies.Entities.Entities;
using NUnit.Framework;

namespace Movies.Repositories.Test
{
    public class MovieRepositoryTest : BaseRepositoryTest
    {
        //[Test]
        //public void GetAllTest()
        //{
        //    // Setup
        //    GenerateDbRecords(10);

        //    // Act
        //    var result = CountryRepository.GetAll().Result;

        //    // Assert
        //    Assert.AreEqual(result.Count(), 10);
        //}

        //private void GenerateDbRecord(Guid id)
        //{
        //    Context.Movies.Add(new Movie
        //    {
        //        Id = id,
        //        Name = $"Name for {id}",
        //        CallingCode = 77,
        //        Capital = $"Capital for {id}",
        //        Region = $"Region for {id}",
        //        SubRegion = $"SubRegion for {id}",
        //        Flag = $"Url Flag for {id}",
        //        CreationDate = DateTimeOffset.UtcNow.AddDays(1).AddHours(2).AddMinutes(3),
        //        ModificationDate = DateTimeOffset.MinValue
        //    });

        //    Context.SaveChanges();
        //}
    }
}