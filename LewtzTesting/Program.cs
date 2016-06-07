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
            var baseTable = new Table("Treasure Table");
            baseTable.LoadFromFile(@"..\..\Tables\treasure table.json", new JSONLoader());
            var baseMagicTable = new Table("magic base");
            baseMagicTable.LoadFromFile(@"..\..\Tables\magic base.json", new JSONLoader());

            var printTree = new PrintEntireTreeVisitor();

            baseTable.Accept(printTree);
            baseMagicTable.Accept(printTree);

            baseTable.RollCount = 12;

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