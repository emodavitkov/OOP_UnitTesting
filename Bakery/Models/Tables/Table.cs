using System;
using System.Collections.Generic;
using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Messages;

namespace Bakery.Models.Tables
{
    public abstract class Table:ITable
    {
        private List<IBakedFood> foodOrders;
        private List<IDrink> drinksOrders;
        private int capacity;
        private int numberOfPeople;

        public Table(int tableNumber, int capacity, decimal pricePerPerson)
        {
            
            this.foodOrders = new List<IBakedFood>();
            this.drinksOrders = new List<IDrink>();
            this.Capacity = capacity;
            PricePerPerson = pricePerPerson;
            TableNumber=tableNumber;
        }

        public int TableNumber { get; private set; }
        

        public int Capacity
        {
            get
            {
                return this.capacity;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTableCapacity);
                }

                this.capacity = value;
            }
        }
      

        public int NumberOfPeople
        {
            get
            {
                return this.numberOfPeople;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidNumberOfPeople);
                }

                this.numberOfPeople = value;
            }
        }
        public decimal PricePerPerson { get; private set; }
        public bool IsReserved { get; private set; }

        public decimal Price
        {
            get
            {
                return PricePerPerson * NumberOfPeople;
            }
        }
        public void Reserve(int numberOfPeople)
        {
            IsReserved = true;
            NumberOfPeople = numberOfPeople;
        }

        public void OrderFood(IBakedFood food)
        {
            this.foodOrders.Add(food);
        }

        public void OrderDrink(IDrink drink)
        {
            this.drinksOrders.Add(drink);
        }

        public decimal GetBill()
        {
            decimal bill = 0;
            foreach (var food in foodOrders)
            {
                bill += food.Price;
            }

            foreach (var drink in drinksOrders)
            {
                bill += drink.Price;
            }

            bill += Price;
            return bill;
        }

        public void Clear()
        {
            foodOrders.Clear();
            drinksOrders.Clear();
            numberOfPeople = 0;
            IsReserved=false;
        }

        public string GetFreeTableInfo()
        {
            return $"Table: {TableNumber}:\r\n"+
            $"Type: {this.GetType().Name}\r\n" +
            $"Capacity: {Capacity}\r\n" + 
            $"Price per Person: {PricePerPerson}";

        }
    }
}