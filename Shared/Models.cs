namespace Shared;

public class Species {
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public int Speed { get; set; }
	public List<AbilityScore> AbilityScores { get; set; } = new();
	public string Alignement { get; set; } = string.Empty;
	public int Age { get; set; }
	public string Size { get; set; } = string.Empty;
	public List<StartingProficency> StartingProficencies { get; set; } = new();
	public List<Lang> Langs { get; set; } = new();
	public List<Trait> Traits { get; set; } = new();
	public List<SubRace> SubRaces { get; set; } = new();
}

public class AbilityScore {
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
}

public class StartingProficency {
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
}

public class Lang {
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
}

public class Trait {
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
}

public class SubRace {
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
}
