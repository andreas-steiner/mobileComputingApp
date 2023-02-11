using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public interface DataStore
    {
        List<Species> AllSpecies();
        void AddSpecies(Species Species);
    }
}
