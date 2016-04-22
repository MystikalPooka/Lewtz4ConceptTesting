using LewtzTesting.Data_Structure;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LewtzTesting.Loaders.JSON
{
    public class JSONLoader : ILoader
    {
        private IDictionary<string, Component> _referenceDictionary;

        //public JSONLoader(IDictionary<string, Component> refDict)
        //{
        //    _referenceDictionary = refDict;
        //}

        public void LoadTableFromFile(string filename, Table tableToAddTo = null)
        {
            var json = File.ReadAllText(filename);
            JToken token = JToken.Parse(json);

            var currentTableName = getNameFromFilename(filename);
            if (tableToAddTo == null)
            {
                tableToAddTo = new Table(currentTableName);
            }

            if (token != null)
            {
                var itemsToAdd =
                        from item in token.Children<JObject>()
                        where item.Value<string>("type") == "item" || item.Value<string>("type") == "MagicItem"
                        select item;

                foreach(var item in itemsToAdd)
                {
                    tableToAddTo.Add(item.ToObject<Item>());
                }

                foreach (var item in tableToAddTo.GetChildren())
                {
                   //Console.WriteLine(item);
                }

                var tablesToLoad =
                        from table in token.Children<JObject>()
                        where table.Value<string>("type") == "table"
                        select table;
                
                foreach (var table in tablesToLoad)
                {
                    var newTable = table.ToObject<Table>(); //includes base case "probability"
                    //Console.WriteLine(newTable);

                    string newFilename = filename.Replace(currentTableName, newTable.Name);
                    //Console.WriteLine(newFilename);

                    var probabilityList = getProbabilityListFromNode(table);

                    //make a new table for each probability
                    for(int i = 0; i < probabilityList.Count; ++i)
                    {
                        var p = probabilityList[i];
                        var diffProbability = (int)p;

                        var diffName = newTable.Name + p.Name.Replace("probability","");
                        
                        //Console.WriteLine("diff" + diffName);
                        
                        if (diffProbability > 0)
                        {
                            var testPrint = new Visitors.PrintEntireTreeVisitor();
                            var diffProbTable = new Table(diffName, diffProbability, newTable.Book);
                            LoadTableFromFile(newFilename, diffProbTable);
                            tableToAddTo.Add(diffProbTable);
                        }
                    }
                }
            }
        }

        private string getNameFromFilename(string filename)
        {
            int indexOfLastSlash = filename.LastIndexOf(@"\");
            int indexOfPeriod = filename.LastIndexOf(".");
            int length = indexOfPeriod - indexOfLastSlash - 1;
            var name = filename.Substring(indexOfLastSlash + 1, length);

            return name;
        }

        private List<JProperty> getProbabilityListFromNode(JToken node)
        {
            var probabilities =
                   from p in node.Children<JProperty>()
                   where p.Name.StartsWith("probability")
                   select p;
            //Console.WriteLine("Count: " + probabilities.Count());
            return probabilities.ToList();
        }
    }
}
