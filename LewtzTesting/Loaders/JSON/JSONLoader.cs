using LewtzTesting.Data_Structure;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LewtzTesting.Loaders.JSON
{
    public class JSONLoader : ILoader
    {
        private IDictionary<string, Component> _referenceDictionary;

        public JSONLoader()
        {
            _referenceDictionary = new Dictionary<string, Component>();
        }

        public void LoadTableFromFile(string filename, Table tableToAddTo = null)
        {
            //
            //TODO: LOAD MAGIC TABLES FIRST, THEN PASS THAT TO EVERY MAGIC ITEM
            //
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
                        where item.Value<string>("type").Contains("item")
                        select item;

                foreach(var item in itemsToAdd)
                {
                    var itemType = item.Value<string>("type");
                    ItemNode newItem;
                    newItem = item.ToObject<Item>();
                    tableToAddTo.Add(newItem);
                }

                var tablesToLoad =
                        from table in token.Children<JObject>()
                        where table.Value<string>("type") == "table"
                        select table;
                
                foreach (var table in tablesToLoad)
                {
                    var newTable = table.ToObject<Table>(); //includes base case "probability"

                    if (newTable.Name.Contains(", roll again"))
                    {
                        newTable.Name.Replace(", roll again", "");
                    }

                    string newFilename = filename.Replace(currentTableName, newTable.Name);

                    var probabilityList = getProbabilityListFromNode(table);

                    //make a new table for each probability
                    for(int i = 0; i < probabilityList.Count; ++i)
                    {
                        var p = probabilityList[i];
                        
                        var diffName = newTable.Name + p.Name.Replace("probability","");

                        var diffProbability = (int)p;
                        if (diffProbability > 0)
                        {
                            var diffProbTable = new Table(diffName, diffProbability, newTable.Book);
                            LoadTableFromFile(newFilename, diffProbTable);
                            tableToAddTo.Add(diffProbTable);
                            diffProbTable.Sort();
                        }
                        newTable.Sort();
                    }
                    tableToAddTo.Sort();
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
            return probabilities.ToList();
        }
    }
}
