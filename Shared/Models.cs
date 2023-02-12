namespace Shared;

public class ChangedStateModel {
	public DateTime LastEdited { get; set; } = DateTime.Now;
	public string LastEditFrom { get; set; } = "system";

	public void CopyFrom(ChangedStateModel other) {
		LastEdited = other.LastEdited;
		LastEditFrom = other.LastEditFrom;
	}
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

	public Species CopyFrom(Species other) {
		if (other == null)
			return this;

		base.CopyFrom(other);

		Id = other.Id;
		Name = other.Name;
		Speed = other.Speed;
		Alignement = other.Alignement;
		Age = other.Age;
		Size = other.Size;
		Langs = other.Langs;
		Traits = other.Traits;
		SubRaces = other.SubRaces;

		return this;
	}
}

public class Lang : ChangedStateModel {
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;

	public Lang CopyFrom(Lang other) {
		if (other == null)
			return this;

		base.CopyFrom(other);

		Id = other.Id;
		Name = other.Name;

		return this;
	}
}

public class Trait : ChangedStateModel {
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;

	public Trait CopyFrom(Lang other) {
		if (other == null)
			return this;

		base.CopyFrom(other);

		Id = other.Id;
		Name = other.Name;

		return this;
	}
}

public class SubRace : ChangedStateModel {
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;

	public SubRace CopyFrom(Lang other) {
		if (other == null)
			return this;

		base.CopyFrom(other);

		Id = other.Id;
		Name = other.Name;

		return this;
	}
}
