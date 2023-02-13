using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared;

namespace Server;

public static class ApiFetchData {
	public static async Task<IApplicationBuilder> AddDataContextFromApi(this IApplicationBuilder app) {
		var scope = app.ApplicationServices.CreateScope();

		var db = scope.ServiceProvider.GetRequiredService<DataContext>();

		var http = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>().CreateClient();
		http.BaseAddress = new("http://www.dnd5eapi.co/");

		dynamic races = JObject.Parse(await http.GetStringAsync("/api/races"));

		if(races == null)
			return app;

		foreach(dynamic race in races.results) {
			dynamic raceInfo = JObject.Parse(await http.GetStringAsync((string)race.url));

			if(raceInfo == null)
				continue;

			var species = new Species() {
				Name = raceInfo.name,
				Size = raceInfo.size,
				Speed = (int)raceInfo.speed,
				Age = 10,
				Alignement = raceInfo.alignment,
				Langs = ((IEnumerable<dynamic>)raceInfo.languages).Select(s => new Lang() { Name = s.name }).ToList(),
				SubRaces = ((IEnumerable<dynamic>)raceInfo.subraces).Select(s => new SubRace() { Name = s.name }).ToList(),
				LastEditFrom = "system"
			};

			db.Specien.Add(species);
		}

		db.SaveChanges();

		return app;
	}
}
