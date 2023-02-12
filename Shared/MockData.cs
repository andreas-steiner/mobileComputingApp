namespace Shared;
public static class MockData {
	public static List<Species> GetData() {
		return new List<Species>() {
			new Species() {
				Id = 1,
				Name = "Dwarf",
				Speed = 25,
				Alignement = "neutral",
				Age = 350,
				Size = "Medium",
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
				Alignement = "neutral",
				Age = 700,
				Size = "Medium",
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
				Id = 3,
				Name = "Halfling",
				Speed = 25,
				Alignement = "neutral",
				Age = 150,
				Size = "Small",
				Langs = new List<Lang>{
					new Lang()
					{
						Id = 3,
						Name = "Dwarvish"
					}
				},
				Traits = new List<Trait>()
				{
					new Trait()
					{
						Id = 3,
						Name = "Trait1"
					}
				},
				SubRaces = new List<SubRace>()
				{
					new SubRace()
					{
						Id = 3,
						Name = "Subrace1"
					}
				}
			},
			new Species() {
				Id = 4,
				Name = "Half Orc",
				Speed = 30,
				Alignement = "neutral",
				Age = 70,
				Size = "Medium",
				Langs = new List<Lang>{
					new Lang()
					{
						Id = 4,
						Name = "Dwarvish"
					}
				},
				Traits = new List<Trait>()
				{
					new Trait()
					{
						Id = 4,
						Name = "Trait1"
					}
				},
				SubRaces = new List<SubRace>()
				{
					new SubRace()
					{
						Id = 4,
						Name = "Subrace1"
					}
				}
			}
		}.ToList();
	}
}
