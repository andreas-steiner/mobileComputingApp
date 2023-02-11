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
            return new List<Species>() {
                new Species() {
                    Id = 1,
                    Name = "Dwarf",
                    Speed = 25,
                    AbilityScores = new List<AbilityScore>() {
                    new AbilityScore() {
                        Id = 1,
                        Name = "AbilityScore1"
                    },
                    new AbilityScore() {
                        Id = 2,
                        Name = "AbilityScore2"
                        }
                    },
                    Alignement = "neutral",
                    Age = 350,
                    Size = "Medium",
                    StartingProficencies = new List<StartingProficency>()
                    {
                        new StartingProficency()
                        {
                            Id = 1,
                            Name = "Starting Proficeny"
                        }
                    },
                    Langs = new List<Lang>{
                        new Lang()
                        {
                            Id = 1,
                            Name = "Dwarvish"
                        }
                    },
                    Traits = new List<Trait>() 
                    {
                        new Trait() 
                        {
                            Id = 1,
                            Name = "Trait1"
                        }
                    },
                    SubRaces = new List<SubRace>() 
                    {
                        new SubRace() 
                        {
                            Id = 1,
                            Name = "Subrace1"
                        } 
                    }
                },
                new Species() {
                    Id = 2,
                    Name = "Elf",
                    Speed = 30,
                    AbilityScores = new List<AbilityScore>() {
                    new AbilityScore() {
                        Id = 3,
                        Name = "AbilityScore1"
                    },
                    new AbilityScore() {
                        Id = 4,
                        Name = "AbilityScore2"
                        }
                    },
                    Alignement = "neutral",
                    Age = 700,
                    Size = "Medium",
                    StartingProficencies = new List<StartingProficency>()
                    {
                        new StartingProficency()
                        {
                            Id = 2,
                            Name = "Starting Proficeny"
                        }
                    },
                    Langs = new List<Lang>{
                        new Lang()
                        {
                            Id = 2,
                            Name = "Dwarvish"
                        }
                    },
                    Traits = new List<Trait>()
                    {
                        new Trait()
                        {
                            Id = 2,
                            Name = "Trait1"
                        }
                    },
                    SubRaces = new List<SubRace>()
                    {
                        new SubRace()
                        {
                            Id = 2,
                            Name = "Subrace1"
                        }
                    }
                },
                new Species() {
                    Id = 1,
                    Name = "Halfling",
                    Speed = 25,
                    AbilityScores = new List<AbilityScore>() {
                    new AbilityScore() {
                        Id = 1,
                        Name = "AbilityScore1"
                    },
                    new AbilityScore() {
                        Id = 2,
                        Name = "AbilityScore2"
                        }
                    },
                    Alignement = "neutral",
                    Age = 150,
                    Size = "Small",
                    StartingProficencies = new List<StartingProficency>()
                    {
                        new StartingProficency()
                        {
                            Id = 1,
                            Name = "Starting Proficeny"
                        }
                    },
                    Langs = new List<Lang>{
                        new Lang()
                        {
                            Id = 1,
                            Name = "Dwarvish"
                        }
                    },
                    Traits = new List<Trait>()
                    {
                        new Trait()
                        {
                            Id = 1,
                            Name = "Trait1"
                        }
                    },
                    SubRaces = new List<SubRace>()
                    {
                        new SubRace()
                        {
                            Id = 1,
                            Name = "Subrace1"
                        }
                    }
                },
                new Species() {
                    Id = 1,
                    Name = "Half Orc",
                    Speed = 30,
                    AbilityScores = new List<AbilityScore>() {
                    new AbilityScore() {
                        Id = 1,
                        Name = "AbilityScore1"
                    },
                    new AbilityScore() {
                        Id = 2,
                        Name = "AbilityScore2"
                        }
                    },
                    Alignement = "neutral",
                    Age = 70,
                    Size = "Medium",
                    StartingProficencies = new List<StartingProficency>()
                    {
                        new StartingProficency()
                        {
                            Id = 1,
                            Name = "Starting Proficeny"
                        }
                    },
                    Langs = new List<Lang>{
                        new Lang()
                        {
                            Id = 1,
                            Name = "Dwarvish"
                        }
                    },
                    Traits = new List<Trait>()
                    {
                        new Trait()
                        {
                            Id = 1,
                            Name = "Trait1"
                        }
                    },
                    SubRaces = new List<SubRace>()
                    {
                        new SubRace()
                        {
                            Id = 1,
                            Name = "Subrace1"
                        }
                    }
                }
            };
        }

        public void AddSpecies(Species Species)
        {
            throw new NotImplementedException();
        }
    }
}
