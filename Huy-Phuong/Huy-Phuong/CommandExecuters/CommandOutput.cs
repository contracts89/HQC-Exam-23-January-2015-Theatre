namespace Theatre.CommandExecuters
{
    using System.Collections.Generic;
    using System.Linq;

    public class CommandOutput
    {
        public static string CommandResult(string theatre, out List<string> performances)
        {
            string commandResult;
            performances = PerformanceCommandExecuter.Universal.ListPerformances(theatre).Select(
                p =>
                    {
                        var result1 = p.PerformanceDateTime.ToString("dd.MM.yyyy HH:mm");
                        return string.Format("({0}, {1})", p.PerformanceName, result1);
                    }).ToList();
            if (performances.Any())
            {
                commandResult = string.Join(", ", performances);
            }
            else
            {
                commandResult = "No performances";
            }
            return commandResult;
        }
    }
}