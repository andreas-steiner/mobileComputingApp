namespace Shared;

public class ChangedStateModel {
	public DateTime LastEdited { get; set; } = DateTime.Now;
	public string LastEditFrom { get; set; } = "system";
}

public class Species : ChangedStateModel {
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;
	public int Speed { get; set; }
	public string Alignement { get; set; } = string.Empty;
	public int Age { get; set; }
	public string Size { get; set; } = string.Empty;

	public List<Lang> Langs { get; set; } = new() { new Lang() { Name = "Common" } };
	public List<Trait> Traits { get; set; } = new() { new Trait() { Name = "trait 1" } };
	public List<SubRace> SubRaces { get; set; } = new() { new SubRace() { Name = "subrace 1" } };
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
