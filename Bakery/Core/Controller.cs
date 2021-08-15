﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Bakery.Core.Contracts;
using Bakery.Models.BakedFoods;
using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables;
using Bakery.Models.Tables.Contracts;

namespace Bakery.Core
{
    public class Controller:IController
    {

        private List<IBakedFood> bakedFoods;
        private List<IDrink> drinks;
        private List<ITable> tables;
        private decimal totalIncome = 0;

        public Controller()
        {
            this.bakedFoods = new List<IBakedFood>();
            this.drinks = new List<IDrink>();
            this.tables = new List<ITable>();
        }

        //Bread White 2.50
        public string AddFood(string type, string name, decimal price)
        {
            
            if (type=="Bread")
            {
             this.bakedFoods.Add(new Bread(name,price));    
            }

            if (type=="Cake")
            {
                this.bakedFoods.Add(new Cake(name, price));
            }

            return $"Added {name} ({type}) to the menu";
        }

        public string AddDrink(string type, string name, int portion, string brand)
        {
            if (type=="Tea")
            {
                this.drinks.Add(new Tea(name,portion,brand));
            }

            if (type=="Water")
            {
                this.drinks.Add(new Water(name,portion,brand));
            }
            return $"Added {name} ({brand}) to the drink menu";
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            if (type=="InsideTable")
            {
                this.tables.Add(new InsideTable(tableNumber,capacity));
            }

            if (type=="OutsideTable")
            {
                this.tables.Add(new OutsideTable(tableNumber,capacity));
            }
            return $"Added table number {tableNumber} in the bakery";
        }

        public string ReserveTable(int numberOfPeople)
        {
            ITable table = tables.FirstOrDefault(table => !table.IsReserved && table.Capacity >= numberOfPeople);

            if (table==null)
            {
                return $"No available table for {numberOfPeople} people";
            }
            else
            {
                table.Reserve(numberOfPeople);
                return $"Table {table.NumberOfPeople} has been reserved for {numberOfPeople} people";
            }
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            //ITable table = tables.FirstOrDefault(table => table.TableNumber == tableNumber);
            var table = tables.FirstOrDefault(table => table.TableNumber == tableNumber);

            if (table==null)
            {
                return $"Could not find table {tableNumber}";
            }
            else
            {
                IBakedFood food = bakedFoods.FirstOrDefault(f => f.Name == foodName);
                if (food==null)
                {
                    return $"No {foodName} in the menu";
                }

                else
                {
                    table.OrderFood(food);
                    return $"Table {tableNumber} ordered {foodName}";
                }
            }
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            //ITable table = tables.FirstOrDefault(table => table.TableNumber == tableNumber);
            var table = tables.FirstOrDefault(table => table.TableNumber == tableNumber);

            if (table == null)
            {
                return $"Could not find table {tableNumber}";
            }
            else
            {
                IDrink drink = drinks.FirstOrDefault(d => d.Name == drinkName && d.Brand==drinkBrand);
                if (drink == null)
                {
                    return $"Could not find table {tableNumber}";
                }

                else
                {
                    table.OrderDrink(drink);
                    return $"Table {tableNumber} ordered {drinkName} {drinkBrand}";
                }
            }
        }

        public string LeaveTable(int tableNumber)
        {
            ITable table = tables.FirstOrDefault(table => table.TableNumber == tableNumber);
            var bill = table.GetBill();
            totalIncome += bill;
            table.Clear();
            return $"Table: {tableNumber}\r\n" +
            $"Bill: {bill:f2}";

        }

        public string GetFreeTablesInfo()
        {
            string result = "";
            List<ITable> freeTables = tables.Where(table => !table.IsReserved).ToList();

            foreach (var freeTable in freeTables)
            {
                result += freeTable.GetFreeTableInfo() + Environment.NewLine;
            }
            
            return result.TrimEnd();
        }

        public string GetTotalIncome()
        {

            return $"Total income: {totalIncome:f2}lv";
        }
    }
}