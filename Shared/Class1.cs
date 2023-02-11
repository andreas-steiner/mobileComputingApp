namespace Shared
{
    public class Species
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Speed { get; set; }
        public List<AbilityScore> AbilityScores { get; set; }
        public string Alignement { get; set; }
        public  int Age { get; set; }
        public string Size { get; set; }
        public List<StartingProficency> StartingProficencies { get; set; }
        public List<Lang> Langs { get; set; }
        public List<Trait> Traits { get; set; }
        public List<SubRace> SubRaces { get; set; }
    }

    public class AbilityScore
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StartingProficency
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Lang
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Trait
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SubRace
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
