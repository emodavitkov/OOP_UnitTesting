using System;
using NUnit.Framework;


namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        private RaceEntry raceEntry;
        
        [SetUp]
        public void Setup()
        {
            this.raceEntry = new RaceEntry();
        }

        [Test]
        public void Counter_IsZeroByDefault()
        {
            Assert.That(this.raceEntry.Counter,Is.Zero);
        }

        [Test]
        public void Counter_Increases_WhenAddingDriver()
        {
            this.raceEntry.AddDriver(new UnitDriver("pesho",new UnitCar("tesla",400,100)));
            Assert.That(this.raceEntry.Counter, Is.EqualTo(1));
        }

        [Test]
        public void AddDriver_ThrowsException_WhenDriverIsNull()
        {
            Assert.Throws<InvalidOperationException>((() => raceEntry.AddDriver(null)));
        }

        [Test]
        public void AddDriver_ThrowsException_WhenDriverNameExists()
        {
            var driverName = "Nasko";

            this.raceEntry.AddDriver(new UnitDriver(driverName, new UnitCar("tesla s", 400, 500)));

            Assert.Throws<InvalidOperationException>((() => raceEntry.AddDriver(new UnitDriver(driverName,new UnitCar("Tesla C",300,700)))));
        }

        [Test]
        public void AddDriver_ReturnsExpectedResultMessage()
        {
            var driverName = "pesho";
            var actual = this.raceEntry.AddDriver(new UnitDriver(driverName, new UnitCar("tesla s", 400, 500)));
            var expected = $"Driver {driverName} added in race.";
            Assert.That(actual,Is.EqualTo(expected));

        }

        [Test]

        public void CalculateAverageHorsePower_ThrowsException_WhenParticipantAreLessThanRequired()
        {
            Assert.Throws<InvalidOperationException>((() => this.raceEntry.CalculateAverageHorsePower()));

            this.raceEntry.AddDriver(new UnitDriver("pesho", new UnitCar("Tesla", 500, 600)));

            Assert.Throws<InvalidOperationException>((() => this.raceEntry.CalculateAverageHorsePower()));


        }

        [Test]

        public void CalculateAverageHorsePower_returnExpectedCalculatedResult()
        {
            int n = 10;

            double expected = 0;

            for (int i = 0; i < n; i++)
            {
                int hp = 450 + i;
                expected += hp;
                this.raceEntry.AddDriver(new UnitDriver($"Name-{i}", new UnitCar("model", hp, 550)));

            }

            expected /= n;

            double actual = this.raceEntry.CalculateAverageHorsePower();

            Assert.That(expected,Is.EqualTo(actual));
        }
    }
}