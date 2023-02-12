namespace Shared;

public class ChangedStateModel {
	public DateTime LastEdited { get; set; } = DateTime.Now;
	public string LastEditFrom { get; set; } = "system";
}

public class Species : ChangedStateModel {
	public int Id { get; set; }

	public string Name { get; set; } = "new Species Name";
	public int Speed { get; set; } = 30;
	public string Alignement { get; set; } = "Neutral";
	public int Age { get; set; } = 75;
	public string Size { get; set; } = "Medium";

	public List<Lang> Langs { get; set; } = new() { new Lang() { Name = "Common" } };
	public List<Trait> Traits { get; set; } = new() { new Trait() { Name = "new Trait" } };
	public List<SubRace> SubRaces { get; set; } = new() { new SubRace() { Name = "Sub Race" } };
}

public class Lang : ChangedStateModel {
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
}

public class Trait : ChangedStateModel {
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
}

public class SubRace : ChangedStateModel {
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
}
