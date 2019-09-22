namespace Iago.Runner
{
    public class IagoHeader
    {
        public static string GetHeader(string version)
       {
           string header = $@"--------------------------------------------------------
     _________
    /         \ ______  _______  _______   {version}
    \_    ____//      |/  ___  \/   __  \
      |   |   /   /|  |  |   \__|  /  \  \
      |   |  /   /_|  |  |   __|  |    \  \
    __|   |_/_   __   |  |___|  |  \    |  |
   /         /  /  |  |         |\  \__/  /
   \________/\_/  /____\____/|  | \______/
                             |__|
    A cool test and spec runner for DNX

--------------------------------------------------------
";
        return header;
       }
    }
}
