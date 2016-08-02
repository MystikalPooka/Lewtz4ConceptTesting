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

            var baseTable2 = new Table("Treasure Table");
            baseTable.LoadFromFile(@"..\..\Tables\treasure table 2.json", new JSONLoader());

            var baseMagicTable = new Table("magic base");
            baseMagicTable.LoadFromFile(@"..\..\Tables\magic base.json", new JSONLoader());

            var printTree = new PrintEntireTreeVisitor();

            baseTable.Accept(printTree);
            Console.WriteLine("\r\n===================\r\n");
            baseTable2.Accept(printTree);
            //baseMagicTable.Accept(printTree);

            baseTable.RollCount = 100;

            Console.WriteLine("\r\n===================\r\n");
            var lootBag = new GetLootVisitor();
            var lootBag2 = new GetLootVisitor();
            baseTable.Accept(lootBag);

            foreach (Component comp in lootBag.GetLootBag())
            {
                Console.WriteLine(comp);
            }
        
            Console.ReadLine();
        }
    }
}