using NUnit.Framework;

namespace Robots.Tests
{
    using System;

    public class RobotsTests
    {
        private Robot robot;
        private RobotManager robotManager;

        [SetUp]
        public void Setup()
        {
            robot = new Robot("pesho",100);
            robotManager = new RobotManager(10);
        }

        [Test]
        public void WhenRobotIsCreated_PropertiesShouldBeSet()
        {
            string robotName = "pesho";
            int battery = 100;
            int batteryMax = 100;

           Assert.That(robot.Name,Is.EqualTo(robotName));
           Assert.That(robot.Battery,Is.EqualTo(battery));
           Assert.That(robot.MaximumBattery,Is.EqualTo(batteryMax));
        }

        [Test]
        public void WhenRobotIsCreated_CapacityAShouldBeSet()
        {
            Assert.That(this.robotManager.Capacity, Is.EqualTo(10));
        }

        [Test]
        public void WhenRobotManagerIsCreated_NegativeCapacityThrowsError()
        {
            Exception ex = Assert.Throws<ArgumentException>((() =>
                    {
                        robotManager = new RobotManager(-10);

                    }
                ));

            Assert.AreEqual(ex.Message, "Invalid capacity!");
        }

        //public void WhenRobotManagerIsCreated_NegativeCapacityThrowsError()
        //{
        //    Assert.Throws<ArgumentException>((() =>
        //            {
        //                robotManager = new RobotManager(-10);

        //            }
        //        ));
        //}

        [Test]
        public void WhenRobotManagerIsCreated_CountShouldBeZero()
        {

            Assert.AreEqual(0,robotManager.Count);
        }

        [Test]
        public void WhenAddingARobot_ThrowsErrorWhenThereIsAlreadyRobotWithTheSameName()
        {
            Assert.Throws<InvalidOperationException>((() =>
                    {
                        robotManager.Add(robot);
                        robotManager.Add(robot);
                    }
                ));
        }

        [Test]
        public void WhenAddingARobot_ThrowsErrorWhenThereIsNoCapacityLeft()
        {
            Assert.Throws<InvalidOperationException>((() =>
                    {
                       RobotManager robotManager = new RobotManager(1);

                        robotManager.Add(robot);
                        robotManager.Add(new Robot("Test 2",10));
                    }
                ));

            Assert.Throws<InvalidOperationException>((() =>
                    {
                        RobotManager robotManager = new RobotManager(0);

                        robotManager.Add(robot);
                    }
                ));

        }

        [Test]
        public void WhenAddingARobotWithCorrectData_SuccessfullyCompleted()
        {

            robotManager.Add(robot);

            Assert.AreEqual(1,robotManager.Count);

            robotManager.Add(new Robot("Test2",2));
            Assert.AreEqual(2,robotManager.Count);
        }

        [Test]
        public void WhenRemoveARobot_ThrowsErrorWhenThereIsNoRobotWithSuchName()
        {
            Assert.Throws<InvalidOperationException>((() =>
                    {
                        robotManager.Remove("emo");
                    }
                ));
        }

        [Test]
        public void RemoveARobot_WhenDataIsCorrect()
        {
            robotManager.Add(new Robot("testRemove",100));
            robotManager.Remove("testRemove");
            Assert.AreEqual(0,robotManager.Count);
        }

        [Test]
        public void WhenRobotWork_ThrowsErrorWhenThereIsNoRobotWithSuchName()
        {
            Assert.Throws<InvalidOperationException>((() =>
                    {
                        robotManager.Work("emo","test",10);
                    }
                ));
        }

        
        [Test]
        public void WhenRobotWork_ThrowsExpectionWhenDoNotHaveEnoughBattery()
        {
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>((() =>
                    {
                        robotManager.Work(robot.Name, "job", robot.Battery+10);
                    }
                ));
        }

        [Test]
        public void WhenRobotWork_ShouldDecreaseBattery()
        {
            robotManager.Add(robot);
            robotManager.Work(robot.Name,"job",5);
            Assert.AreEqual(robot.Battery,95);
        }

        [Test]
        public void WhenChargeNotExisting_ExceptionShouldBeThrown()
        {
            Assert.Throws<InvalidOperationException>((() =>
                    {
                        robotManager.Charge("emo");
                    }
                ));
        }

        [Test]
        public void WhenChargeRobot_ShouldGetBatteryToMax()
        {
            robotManager.Add(robot);
            robotManager.Work(robot.Name, "job", 5);
            robotManager.Charge(robot.Name);
            Assert.AreEqual(robot.Battery, 100);
        }

    }
}
