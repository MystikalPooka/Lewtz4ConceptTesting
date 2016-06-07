using System.Collections.Generic;
using LewtzTesting.Visitors;

namespace LewtzTesting.Data_Structure
{
    public class MagicItem : Item
    {
        private List<Component> _appliedAbilities;
        public Dictionary<string, Table> ReferenceDictionary { get; set; }

       public MagicItem(Dictionary<string, Table> referenceDict)
        {
            _appliedAbilities = new List<Component>();
            ReferenceDictionary = referenceDict;
            Types |= ItemTypes.Magic;
        }

        public MagicItem()
        {
            _appliedAbilities = new List<Component>();
            Types |= ItemTypes.Magic;
        }

        public void Build()
        {
            Table magicTable = getTableFromDictionaryString("magic base");
            
            if (magicTable.Name != "Table Not Found")
            {
                var abilitiesVisitor = new GetLootVisitor();
                magicTable.Accept(abilitiesVisitor);
                var abilityBag = abilitiesVisitor.GetLootBag();
                if(abilityBag.Count > 0)
                {
                    ItemTypes types = abilityBag[0].Types;
                     if(types != ItemTypes.Ability && types != ItemTypes.None)
                    {
                        //System.Console.WriteLine("HOLY CHEESE, IT'S NOT JUST AN ABILITY " + types);
                    }
                    //Name = ROLL ON TABLE TO DETERMINE ITEM (based on originally rolled item type)
                    _appliedAbilities.AddRange(abilityBag);
                }
            }
        }

        private Table getTableFromDictionaryString(string tableName)
        {
            return ReferenceDictionary.ContainsKey(tableName) ? ReferenceDictionary[tableName] : new Table("Table Not Found");
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return base.ToString() + Types.ToString() + " Abilities: \r\n \t"+ GetAbilityNames();
        }

        private string GetAbilityNames()
        {
            string abilities = "";
            foreach(Component comp in _appliedAbilities)
            {
                abilities += comp.Name + "\r\n";
            }
            return abilities;
        }
    }
}
