namespace Theatre.CommandExecuters
{
    using System.Collections.Generic;
    using System.Linq;

    internal static class TheatresCommandExecuter
    {
        public static string ExecutePrintAllTheatresCommand()
        {
            var theatresCount = PerformanceCommandExecuter.Universal.ListTheatres().Count();
            if (theatresCount <= 0)
            {
                return "No theatres";
            }
            var resultTheatres = new LinkedList<string>();
            for (var i = 0; i < theatresCount; i++)
            {
                PerformanceCommandExecuter.Universal.ListTheatres()
                    .Skip(i)
                    .ToList()
                    .ForEach(t => resultTheatres.AddLast(t));
                foreach (var t in PerformanceCommandExecuter.Universal.ListTheatres().Skip(i + 1))
                {
                    resultTheatres.Remove(t);
                }
            }
            return string.Join(", ", resultTheatres);
        }

        internal static string ExecuteAddTheatreCommand(string[] parameters)
        {
            var theatreName = parameters[0];
            PerformanceCommandExecuter.Universal.AddTheatre(theatreName);
            return "Theatre added";
        }
    }
}