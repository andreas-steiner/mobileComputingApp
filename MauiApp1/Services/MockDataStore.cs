using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class MockDataStore : DataStore
    {
        public List<Species> AllSpecies()
        {
            return MockData.GetData();
        }
        
        public void AddSpecies(Species Species)
        {
            throw new NotImplementedException();
        }
    }
}
