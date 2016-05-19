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
            var baseTable = new Table("Weapons");
            j.LoadTableFromFile(@"Tables\weapons.json", baseTable);
            var baseMagicTable = new Table("Magic Base");
            //j.LoadTableFromFile(@"Tables\magic base.json", baseMagicTable);

            var printTree = new PrintEntireTreeVisitor();

            baseTable.Accept(printTree);
            baseMagicTable.Accept(printTree);

            Console.ReadLine();
        }
    }
}