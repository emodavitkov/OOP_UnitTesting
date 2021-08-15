
namespace Aquariums.Tests
{
    using System;
    using NUnit.Framework;

    public class AquariumsTests
    {
        private Aquarium aquarium;
        private Fish fish;

        [SetUp]
        public void Setup()
        {
            aquarium = new Aquarium("AquariumName", 10);
            fish = new Fish("FishName");
        }


        [Test]
        public void WhenAquariumIsCreated_PropertiesShouldBeSet()
        {
            string aquariumName = "AquariumName";
            int capacity = 10;
            aquarium.Add(new Fish("FishName"));
            aquarium.Add(new Fish("Riba"));
            aquarium.Add(new Fish("Riba2"));

            Assert.That(aquarium.Name, Is.EqualTo(aquariumName));
            Assert.That(aquarium.Capacity, Is.EqualTo(capacity));
            Assert.AreEqual(aquarium.Count,3);
        }

        [Test]
        public void WhenAquariumNameIsCreated_NullOrEmptyThrowsError()
        {
            Assert.Throws<ArgumentNullException>
                (() => new Aquarium(null, 1));

            Assert.Throws<ArgumentNullException>
                (() => new Aquarium(String.Empty, 1));
        }

        [Test]
        public void WhenAquariumIsCreated_NegativeCapacityThrowsError()
        {
            Exception ex = Assert.Throws<ArgumentException>((() =>
                    {
                        aquarium = new Aquarium("Test",-10);

                    }
                ));

            Assert.AreEqual(ex.Message, "Invalid aquarium capacity!");
        }

        [Test]
        public void WhenAquariumIsCreated_CountShouldBeZero()
        {
            Assert.AreEqual(0, aquarium.Count);
        }

        [Test]
        public void WhenAddingAFish_ThrowsErrorWhenTheCapacityIsReached()
        {
            aquarium = new Aquarium("Test", 1);

            Assert.Throws<InvalidOperationException>((() =>
                    {
                        aquarium.Add(new Fish("Ribka"));
                        aquarium.Add(new Fish("Ribka1"));
                        
                    }
                ));
        }


        [Test]
        public void WhenAddingFishWithCorrectData_SuccessfullyCompleted()
        {

            aquarium.Add(new Fish("Ribka1"));

            Assert.AreEqual(1, aquarium.Count);

            aquarium.Add(new Fish("Ribka2"));
            Assert.AreEqual(2, aquarium.Count);
        }

        [Test]
        public void WhenRemoveAFish_ThrowsErrorWhenThereIsNoFishWithSuchName()
        {
            Assert.Throws<InvalidOperationException>((() =>
                    {
                        aquarium.RemoveFish("emo");
                    }
                ));
        }

        [Test]
        public void RemoveAFish_WhenDataIsCorrect()
        {
            aquarium.Add(new Fish("emo"));
            aquarium.RemoveFish("emo");
            Assert.AreEqual(0, aquarium.Count);
        }

        [Test]
        public void WhenSellAFish_ThrowsErrorWhenThereIsNoFishWithSuchName()
        {
            Assert.Throws<InvalidOperationException>((() =>
                    {
                        aquarium.SellFish("emo");
                    }
                ));
        }

        [Test]
        public void SellAFish_WhenDataIsCorrect()
        {
            aquarium.Add(new Fish("emo"));
            fish = aquarium.SellFish("emo");

            Assert.AreEqual(fish.Name, "emo");
            Assert.AreEqual(fish.Available,false);
        }


        [Test]
        public void Report()
        {
            aquarium.Add(new Fish("emo"));

            string expectedMassage = $"Fish available at AquariumName: emo";
            Assert.AreEqual(expectedMassage, aquarium.Report());
            
        }
    }
}
