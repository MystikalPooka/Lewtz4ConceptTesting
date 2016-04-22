using LewtzTesting.Data_Structure;

namespace LewtzTesting.Loaders
{
    public interface ILoader
    {
        void LoadTableFromFile(string filename, Table table);
    }
}
