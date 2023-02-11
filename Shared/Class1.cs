namespace Shared
{
    public class Spezies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Speed { get; set; }
        public List<AbilityScore> abilityScores { get; set; }
        public string Alignement { get; set; }
        public  int Age { get; set; }
        public string Size { get; set; }
        public List<StartingProficency> startingProficencies { get; set; }
        public List<Lang> langs { get; set; }
        public List<Trait> traits { get; set; }
        public List<SubRace> subRaces { get; set; }
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