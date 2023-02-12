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

	public List<Lang> Langs { get; set; } = new();
	public List<Trait> Traits { get; set; } = new();
	public List<SubRace> SubRaces { get; set; } = new();
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
