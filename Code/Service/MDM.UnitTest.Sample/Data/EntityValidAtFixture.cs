namespace EnergyTrading.MDM.Test.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using Moq;

    using EnergyTrading.MDM.Data;
    using EnergyTrading;
    using EnergyTrading.Data;
    using EnergyTrading.MDM;

    [TestFixture]
    public class EntityValidAtFixture
    {
        private readonly DateTime finish;
        private readonly DateRange range;
        private readonly DateTime start;

        public EntityValidAtFixture()
        {
            this.start = new DateTime(1999, 12, 31);
            this.finish = new DateTime(2010, 12, 31);
            this.range = new DateRange(this.start, this.finish);
        }

        [Test]
        public void DateAfterValidity()
        {
            // Arrange
            var repository = new Mock<IRepository>();
            var pm = new PersonMapping { Validity = this.range };

            var list = new List<PersonMapping> { pm };
            repository.Setup(x => x.Queryable<PersonMapping>()).Returns(list.AsQueryable());
            var date = this.range.Finish.AddHours(1);

            // Act
            var candidate = repository.Object.Queryable<PersonMapping>().ValidAt(date).ToList();

            // Assert
            Assert.AreEqual(0, candidate.Count, "Count differs");
        }

        [Test]
        public void DateBeforeValidity()
        {
            // Arrange
            var repository = new Mock<IRepository>();
            var pm = new PersonMapping { Validity = this.range };

            var list = new List<PersonMapping> { pm };
            repository.Setup(x => x.Queryable<PersonMapping>()).Returns(list.AsQueryable());
            var date = this.range.Start.AddHours(-1);

            // Act
            var candidate = repository.Object.Queryable<PersonMapping>().ValidAt(date).ToList();

            // Assert
            Assert.AreEqual(0, candidate.Count, "Count differs");
        }

        [Test]
        public void DateDuringValidity()
        {
            // Arrange
            var repository = new Mock<IRepository>();
            var pm = new PersonMapping { Validity = this.range };

            var list = new List<PersonMapping> { pm };
            repository.Setup(x => x.Queryable<PersonMapping>()).Returns(list.AsQueryable());
            var date = this.range.Start.AddHours(12);

            // Act
            var candidate = repository.Object.Queryable<PersonMapping>().ValidAt(date).ToList();

            // Assert
            Assert.AreEqual(1, candidate.Count, "Count differs");
        }
    }
}