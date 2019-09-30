using System;
using System.Linq;
using Nabu;

namespace Iago.Runner
{
    public class IagoHeader
    {
        public static string GetHeader(string version, int centerWidth = 0)
        {
            string header = $@"
.      _________                                        .
.     /         \ ______  _______  _______              .
.     \_    ____//      |/  ___  \/   __  \             .
.       |   |   /   /|  |  |   \__|  /  \  \            .
.       |   |  /   /_|  |  |   __|  |    \  \           .
.     __|   |_/_   __   |  |___|  |  \    |  |          .
.    /         /  /  |  |         |\  \__/  /           .
.    \________/\_/  /____\____/|  | \______/            .
.                              |__|                     .
.                 (v. {version} )                       .
.     A cool test and spec runner for dotnet core
";
            if(centerWidth < 60)
                return header;

            var lines = header.Split(Environment.NewLine).Select(x => x.Center(centerWidth));
            return string.Join(Environment.NewLine, lines);
        }
    }
}