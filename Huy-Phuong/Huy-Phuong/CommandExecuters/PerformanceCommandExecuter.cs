namespace Theatre.CommandExecuters
{
    using System.Linq;
    using System.Text;
    using Theatre.Contracts;
    using Theatre.Models;

    public static class PerformanceCommandExecuter
    {
        public static IPerformanceDatabase Universal = new PerformanceDatabase();

        internal static string ExecutePrintAllPerformancesCommand()
        {
            var performances = Universal.ListAllPerformances().ToList();
            var result = string.Empty;
            if (performances.Any())
            {
                for (var i = 0; i < performances.Count; i++)
                {
                    var sb = new StringBuilder();
                    sb.Append(result);
                    if (i > 0)
                    {
                        sb.Append(", ");
                    }
                    var result1 = performances[i].PerformanceDateTime.ToString("dd.MM.yyyy HH:mm");
                    sb.AppendFormat(
                        "({0}, {1}, {2})",
                        performances[i].PerformanceName,
                        performances[i].TheatreName,
                        result1);
                    result = sb + "";
                }
                return result;
            }

            return "No performances";
        }
    }
}