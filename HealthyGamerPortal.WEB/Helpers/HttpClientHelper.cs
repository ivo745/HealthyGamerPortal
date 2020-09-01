using Discord.Rest;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthyGamerPortal.Common.Helpers
{
    /// <summary>
    /// Helper class that initializes base <see cref="HttpClient"/>s for use with Refit or other elements requiring a HTTP connection.
    /// </summary>
    public class HttpClientHelper
    {
        private static readonly DiscordRestClient discordClientUser = new DiscordRestClient();
        private static readonly DiscordRestClient discordClientBot = new DiscordRestClient();

        public static RestUser CurrentUser()
        {
            return discordClientUser.CurrentUser;
        }

        public static DiscordRestClient ClientBot()
        {
            return discordClientBot;
        }

        public static DiscordRestClient ClientUser()
        {
            return discordClientUser;
        }

        public static async Task DiscordClientBotLoginAsync()
        {
            if (discordClientBot.LoginState != Discord.LoginState.LoggedIn)
            {
                await discordClientBot.LoginAsync(Discord.TokenType.Bot, "NjYyNTU2OTE4MTc4NTEyOTA2.XiEvpw.NCZmaAJCxFwJalHMy3XZjAGsg9c", true);
            }
        }

        public static async Task DiscordClientUserLoginAsync(string accessToken)
        {
            if (discordClientUser.LoginState != Discord.LoginState.LoggedIn)
            {
                await discordClientUser.LoginAsync(Discord.TokenType.Bearer, accessToken, true);
            }
        }
    }
}