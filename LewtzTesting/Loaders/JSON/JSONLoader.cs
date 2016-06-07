
using LewtzTesting.Data_Structure;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LewtzTesting.Loaders.JSON
{
    public class JSONLoader : ILoader
    {
        private static Dictionary<string, Table> _referenceDictionary = new Dictionary<string, Table>();

        public void LoadTableFromFile(string filename, Table tableToAddTo = null)
        {
            try
            {
                var json = File.ReadAllText(filename);
                JToken token = JToken.Parse(json);

                var currentTableName = getNameFromFilename(filename);
                if (tableToAddTo == null)
                {
                    tableToAddTo = new Table(currentTableName);
                }
                addToDictionary(tableToAddTo);

                if (token != null)
                {
                    loadAllItemsAndAbilities(tableToAddTo, token);
                    loadAllTablesFromTokenAndFile(tableToAddTo, token, filename);
                }
                tableToAddTo.Sort();
            }
            catch(FileNotFoundException e)
            {
                
            }
        }

        private static void loadAllItemsAndAbilities(Table tableToAddTo, JToken token)
        {
            var itemsToAdd =
                from item in token.Children<JObject>()
                where item.Value<string>("type").ToLower().Contains("item")
                select item;

            foreach (var item in itemsToAdd)
            {
                var type = item.Value<string>("type");
                Item newItem;

                if (type.ToLower().Contains("magic"))
                {
                    newItem = item.ToObject<MagicItem>();
                    ((MagicItem)newItem).ReferenceDictionary = _referenceDictionary;
                }
                else
                {
                    newItem = item.ToObject<MundaneItem>();
                }
                tableToAddTo.Add(newItem);
            }

            var abilitiesToAdd =
                from item in token.Children<JObject>()
                where item.Value<string>("type").ToLower().Contains("ability")
                select item;

            foreach (var ability in abilitiesToAdd)
            {
                var itemType = ability.Value<string>("type");
                Ability newAbility = ability.ToObject<Ability>();
                newAbility.Types |= ItemTypes.Ability;
                tableToAddTo.Add(newAbility);
            }
        }
        private void loadAllTablesFromTokenAndFile(Table tableToAddTo, JToken token, string filename)
        {
            var tablesToLoad =
                    from table in token.Children<JObject>()
                    where table.Value<string>("type") == "table"
                    select table;

            foreach (var table in tablesToLoad)
            {
                var probabilityList = getProbabilityListFromNode(table);
                //make a new table for each probability
                for (int i = 0; i < probabilityList.Count; ++i)
                {
                    var newTable = table.ToObject<Table>(); //includes base case "probability"
                    string newFilename = filename.Replace(tableToAddTo.Name.ToLower(), newTable.Name.Replace(", roll again", ""));

                    var p = probabilityList[i];

                    var diffProbability = (int)p;
                    if (diffProbability > 0)
                    {
                        tableToAddTo.Add(newTable);
                        LoadTableFromFile(newFilename, newTable);
                        newTable.Sort();
                    }
                }
            }
        }

        private void addToDictionary(Table table)
        {
            if (table != null)
            {
                if (!_referenceDictionary.ContainsKey(table.Name))
                {
                    _referenceDictionary.Add(table.Name, table);
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