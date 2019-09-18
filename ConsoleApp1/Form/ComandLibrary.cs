using Discord;
using Discord.Commands;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;


namespace Weather_Bot.Form
{
	public class ComandLibrary : ModuleBase
	{
		string[] mapLibrary = {
			"海岸線",
			"高層タワー",
			"領事館",
			"銀行",
			"クラブハウス",
			"オレゴン",
			"国境",
			"ヴィラ",
			"要塞",
		 };

		/// <summary>
		/// [start]とコメントされたときのレスポンス
		/// </summary>
		/// <returns>チーム分けを行った結果</returns>
		[Command("start")]
		public async Task Start(params string[] person)
		{
			var index = person.Length / 2;

			var groupA = new ArrayList();
			var groupB = new ArrayList();

			for (int i = 0; i < person.Length; i++)
			{
				var teamNum = new Random().Next(2);
				if (teamNum == 1
					&& groupA.Count < index 
					&& groupB.Count >= index)
				{
					groupA.Add(person[i]);
				}
				else
				{
					groupB.Add(person[i]);
				}
			}

			var strGroupA = string.Join(" ", Enumerable.Range(0, groupA.Count).Select(x => groupA[x]));
			var strGroupB = string.Join(" ", Enumerable.Range(0, groupB.Count).Select(x => groupB[x]));

			var embed = new EmbedBuilder();
			embed.WithTitle("♪結果☆");
			embed.WithDescription("チームA : " + strGroupA + "\nチームB : " + strGroupB);


			await ReplyAsync(embed: embed.Build());
		}

		/// <summary>
		/// Mapを選択します。
		/// </summary>
		/// <returns>ランダムで選択されたマップ</returns>
		[Command("map")]
		public async Task Map()
		{
			var rnd = new Random().Next(mapLibrary.Length);
			var result = mapLibrary[rnd];

			var embed = new EmbedBuilder();
			embed.WithTitle("マップ");
			embed.WithDescription(result);


			await ReplyAsync(embed: embed.Build());
		}

		[Command("lp")]
		public async Task Lp()
		{
			var result = "a";
			await ReplyAsync(result);
		}

		/// <summary>
		/// マップ選択
		/// </summary>
		private string SelectMap => mapLibrary[new Random().Next(mapLibrary.Length)];

	}
}

