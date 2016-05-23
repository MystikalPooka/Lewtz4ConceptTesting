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
            var j = new JSONLoader();
            var baseTable = new Table("Treasure Table");
            j.LoadTableFromFile(@"Tables\treasure table.json", baseTable);
            var baseMagicTable = new Table("Magic Base");
            j.LoadTableFromFile(@"Tables\magic base.json", baseMagicTable);

            var printTree = new PrintEntireTreeVisitor();

            baseTable.Accept(printTree);
            baseMagicTable.Accept(printTree);

            baseTable.RollCount = 12;
            baseMagicTable.RollCount = 2;

            Console.WriteLine("\r\n===================\r\n");
            var lootBag = new GetLootVisitor();
            var lootBag2 = new GetLootVisitor();
            baseTable.Accept(lootBag);
            //baseTable.Accept(lootBag2);

            foreach (Component comp in lootBag.GetLootBag())
            {
                Console.WriteLine(comp);
            }

            Console.ReadLine();
        }
    }
}