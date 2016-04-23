using LewtzTesting.Data_Structure;
using LewtzTesting.Loaders.JSON;
using LewtzTesting.Visitors;
using System;

namespace LewtzTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            //Table treasureTable = new Table("Treasure");
            //Table weaponsTable = new Table("Weapons", 50);
            //Table armorTable = new Table("Armor", 100);

            //treasureTable.Add(weaponsTable);
            //treasureTable.Add(armorTable);
            //    weaponsTable.Add(new Item("Twig", 10));
            //var subWeapons = new Table("SubWeapons", 20);
            //    weaponsTable.Add(subWeapons);
            //        subWeapons.Add(new Item("Subby", 30));
            //        subWeapons.Add(new Item("Subba", 70));
            //        subWeapons.Add(new Item("Subba Subba", 100));
            //    weaponsTable.Add(new Item("Stick", 45));
            //    weaponsTable.Add(new Item("Big Stick", 95));
            //    weaponsTable.Add(new Item("Big Fuckin' Stick", 100));
            //    armorTable.Add(new Item("Panties", 15));
            //    armorTable.Add(new Item("Pantaloons", 65));
            //    armorTable.Add(new Item("Pants", 90));
            //    armorTable.Add(new Item("Panthers", 100));

            var j = new JSONLoader();
            var baseTable = new Table("Weapons");
            j.LoadTableFromFile(@"E:\Coding\Visual Studio Projects\LewtzTesting\LewtzTesting\Tables\weapons.json", baseTable);
           var printTree = new PrintEntireTreeVisitor();

            //baseTable.Accept(printTree);

            var lootList = baseTable.RollLoot(7);

            foreach(Item item in lootList)
            {
                Console.WriteLine("ROLLED ITEM " + item);
            }

            Console.ReadLine();
        }
    }
}