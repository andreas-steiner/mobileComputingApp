namespace Shared;

public class Species {
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;
	public int Speed { get; set; }
	public string Alignement { get; set; } = string.Empty;
	public int Age { get; set; }
	public string Size { get; set; } = string.Empty;

	public List<Lang> Langs { get; set; } = new();
	public List<Trait> Traits { get; set; } = new();
	public List<SubRace> SubRaces { get; set; } = new();

	public DateTime LastEdited { get; set; } = DateTime.Now;
	public string LastEditFrom { get; set; } = "system";

	public Species CopyFrom(Species other) {
		if (other == null)
			return this;

		Id = other.Id;
		Name = other.Name;
		Speed = other.Speed;
		Alignement = other.Alignement;
		Age = other.Age;
		Size = other.Size;
		Langs.Clear();
		Langs = other.Langs.Select(s => new Lang().CopyFrom(s)).ToList();
		Traits.Clear();
		Traits = other.Traits.Select(s => new Trait().CopyFrom(s)).ToList();
		SubRaces.Clear();
		SubRaces = other.SubRaces.Select(s => new SubRace().CopyFrom(s)).ToList();

		LastEdited = other.LastEdited;
		LastEditFrom = other.LastEditFrom;

		return this;
	}
}

public class Lang {
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;

	public Lang CopyFrom(Lang other) {
		if (other == null)
			return this;

		Id = other.Id;
		Name = other.Name;

		return this;
	}
}

public class Trait {
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;

	public Trait CopyFrom(Trait other) {
		if (other == null)
			return this;

		Id = other.Id;
		Name = other.Name;

		return this;
	}
}

public class SubRace {
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;

	public SubRace CopyFrom(SubRace other) {
		if (other == null)
			return this;

		Id = other.Id;
		Name = other.Name;

		return this;
	}
}
