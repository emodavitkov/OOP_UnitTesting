using System;
using System.Collections.Generic;
using System.Linq;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Utilities;
using EasterRaces.Utilities.Messages;

namespace EasterRaces.Models.Races.Entities
{
    public class Race : IRace
    {
        private const int NameMinLen = 5;
        private const int MinLaps = 1;

        private string name;
        private int laps;

        private readonly IDictionary<string, IDriver> driverByName;

        public Race(string name, int laps)
        {
            this.name = name;
            this.laps = laps;
            this.driverByName = new Dictionary<string, IDriver>();

        }
        public string Name
        {
            get =>this.name;
            private set 
            {
                Validator.ThrowIfStringIsNullEmptyOrLessThan(
                    value,
                    NameMinLen,
                    string.Format(ExceptionMessages.InvalidName,value,NameMinLen));

                this.name = value;
            }
        }

        public int Laps
        {
            get => this.laps;

            private set
            {
                if (value < MinLaps)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidNumberOfLaps, MinLaps));

                }
                this.laps = value;
            }

        }

        public IReadOnlyCollection<IDriver> Drivers => this.driverByName.Values.ToList();

        public void AddDriver(IDriver driver)
        {
            if (driver==null)
            {
                throw new ArgumentNullException(ExceptionMessages.DriverInvalid);
            }

            if (Equals(!driver.CanParticipate))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.DriverNotParticipate, driver.Name));
            }

            if (this.driverByName.ContainsKey(driver.Name))
            {
                throw new ArgumentNullException(
                    string.Format(ExceptionMessages.DriverAlreadyAdded,driver.Name,this.Name));
            }

            this.driverByName.Add(driver.Name,driver);
        }
    }
}