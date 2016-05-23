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
            Table magicTable = new Table();
            var abilitiesVisitor = new GetLootVisitor();
            if (ReferenceDictionary.TryGetValue("Magic Base", out magicTable))
            {
                magicTable.Accept(abilitiesVisitor);
                _appliedAbilities.AddRange(abilitiesVisitor.GetLootBag());
            }
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
