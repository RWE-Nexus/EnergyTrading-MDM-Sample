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
    public class EntityOverlapsFixture
    {
        private readonly DateTime finish;
        private readonly DateRange validity;
        private readonly DateTime start;

        public EntityOverlapsFixture()
        {
            this.start = new DateTime(1999, 12, 31);
            this.finish = new DateTime(2010, 12, 31);
            this.validity = new DateRange(this.start, this.finish);
        }

        [Test]
        public void RangeAfterValidity()
        {
            // Arrange
            var repository = new Mock<IRepository>();
            var pm = new PersonMapping { Validity = this.validity };

            var list = new List<PersonMapping> { pm };
            repository.Setup(x => x.Queryable<PersonMapping>()).Returns(list.AsQueryable());
            var range = new DateRange(this.validity.Finish.AddHours(1), this.validity.Finish.AddHours(2));

            // Act
            var candidate = repository.Object.Queryable<PersonMapping>().Overlaps(range).ToList();

            // Assert
            Assert.AreEqual(0, candidate.Count, "Count differs");
        }

        [Test]
        public void RangeBeforeValidity()
        {
            // Arrange
            var repository = new Mock<IRepository>();
            var pm = new PersonMapping { Validity = this.validity };

            var list = new List<PersonMapping> { pm };
            repository.Setup(x => x.Queryable<PersonMapping>()).Returns(list.AsQueryable());
            var range = new DateRange(this.validity.Start.AddHours(-2), this.validity.Start.AddHours(-1));

            // Act
            var candidate = repository.Object.Queryable<PersonMapping>().Overlaps(range).ToList();

            // Assert
            Assert.AreEqual(0, candidate.Count, "Count differs");
        }

        [Test]
        public void RangeWithinValidity()
        {
            // Arrange
            var repository = new Mock<IRepository>();
            var pm = new PersonMapping { Validity = this.validity };

            var list = new List<PersonMapping> { pm };
            repository.Setup(x => x.Queryable<PersonMapping>()).Returns(list.AsQueryable());
            var range = new DateRange(this.validity.Start.AddHours(1), this.validity.Finish.AddHours(-1));

            // Act
            var candidate = repository.Object.Queryable<PersonMapping>().Overlaps(range).ToList();

            // Assert
            Assert.AreEqual(1, candidate.Count, "Count differs");
        }

        [Test]
        public void RangeOverlapsStart()
        {
            // Arrange
            var repository = new Mock<IRepository>();
            var pm = new PersonMapping { Validity = this.validity };

            var list = new List<PersonMapping> { pm };
            repository.Setup(x => x.Queryable<PersonMapping>()).Returns(list.AsQueryable());
            var range = new DateRange(this.validity.Start.AddHours(-1), this.validity.Start.AddHours(1));

            // Act
            var candidate = repository.Object.Queryable<PersonMapping>().Overlaps(range).ToList();

            // Assert
            Assert.AreEqual(1, candidate.Count, "Count differs");
        }

        [Test]
        public void RangeOverlapsFinish()
        {
            // Arrange
            var repository = new Mock<IRepository>();
            var pm = new PersonMapping { Validity = this.validity };

            var list = new List<PersonMapping> { pm };
            repository.Setup(x => x.Queryable<PersonMapping>()).Returns(list.AsQueryable());
            var range = new DateRange(this.validity.Finish.AddHours(-1), this.validity.Finish.AddHours(1));

            // Act
            var candidate = repository.Object.Queryable<PersonMapping>().Overlaps(range).ToList();

            // Assert
            Assert.AreEqual(1, candidate.Count, "Count differs");
        }

        [Test]
        public void RangeSubsumesValidity()
        {
            // Arrange
            var repository = new Mock<IRepository>();
            var pm = new PersonMapping { Validity = this.validity };

            var list = new List<PersonMapping> { pm };
            repository.Setup(x => x.Queryable<PersonMapping>()).Returns(list.AsQueryable());
            var range = new DateRange(this.validity.Start.AddHours(-1), this.validity.Finish.AddHours(1));

            // Act
            var candidate = repository.Object.Queryable<PersonMapping>().Overlaps(range).ToList();

            // Assert
            Assert.AreEqual(1, candidate.Count, "Count differs");
        }
    }
}