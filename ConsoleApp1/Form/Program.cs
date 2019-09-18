using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Discord;

namespace Weather_Bot.Form
{
	class Program
	{
		public static DiscordSocketClient client;
		public static CommandService commands;
		public static IServiceProvider services;

		static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

		/// <summary>
		/// 起動時処理
		/// </summary>
		/// <returns></returns>
		public async Task MainAsync()
		{
			client = new DiscordSocketClient();
			commands = new CommandService();
			services = new ServiceCollection().BuildServiceProvider();
			client.MessageReceived += CommandRecieved;

			client.Log += Log;
			string token = "NDE1ODA1MDI5OTU0NDg2Mjgz.Dr7JzA.1nP0k9TR-oLaKX2kg79dIr5GBjs";
			await commands.AddModulesAsync(Assembly.GetEntryAssembly(),services);
			await client.LoginAsync(TokenType.Bot, token);
			await client.StartAsync();
			await Task.Delay(-1);
		}

		/// <summary>
		/// メッセージの受信処理
		/// </summary>
		/// <param name="msgParam"></param>
		/// <returns></returns>
		private async Task CommandRecieved(SocketMessage messageParam)
		{
			var message = messageParam as SocketUserMessage;
			Console.WriteLine("{0} {1}→{2}", message.Channel.Name, message.Author.Username, message);

			if (message == null) { return; }
			// コメントがユーザーかBotかの判定
			if (message.Author.IsBot) { return; }

			int argPos = 0;

			// コマンドかどうか判定（今回は、「.」で判定）
			if (!(message.HasCharPrefix('.', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))) { return; }

			var context = new CommandContext(client, message);

			// 実行
			var result = await commands.ExecuteAsync(context, argPos, services);

			//実行できなかった場合
			if (!result.IsSuccess) { await context.Channel.SendMessageAsync("何言ってるかわからないよー(´・ω・`)"); }
		}

		/// <summary>
		/// コンソール表示処理
		/// </summary>
		/// <param name="msg"></param>
		/// <returns></returns>
		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
	}
}

