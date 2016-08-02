using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LewtzTesting.Data_Structure
{
    public class TableDatabase : Dictionary<string,Table>
    {
        public Table GetTableFromString(string tableName)
        {
           return ContainsKey(tableName) ? ((Table)this[tableName].Clone()) : new Table("Table Not Found");
        }

        public Table GetMagicTableFromItemTypes(ItemTypes types)
        {
            return new Table();
        }

        public void AddTable(Table table)
        {
            if (!ContainsKey(table.Name))
            {
                Add(table.Name, table);
            }
        }
    }
}
