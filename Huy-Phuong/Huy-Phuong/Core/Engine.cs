namespace Theatre.Core
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Theatre.CommandExecuters;

    public class Engine
    {
        public static string TheatreName(
            string[] commandParams,
            out string performanceTitle,
            out DateTime startDateTime,
            out TimeSpan duration,
            out decimal price)
        {
            var theatreName = commandParams[0];
            performanceTitle = commandParams[1];
            startDateTime = DateTime.ParseExact(commandParams[2], "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            duration = TimeSpan.Parse(commandParams[3]);
            price = decimal.Parse(commandParams[4], NumberStyles.Float);
            return theatreName;
        }

        public void Run()
        {
            while (true)
            {
                var reader = Console.ReadLine();
                if (reader == null)
                {
                    return;
                }

                if (reader == string.Empty)
                {
                    continue;
                }
                var commandParts = reader.Split('(');
                var command = commandParts[0];
                var commandParts1 = reader.Split(new[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
                var commandParams = commandParts1.Skip(1).Select(p => p.Trim()).ToArray();
                string commandResult;
                try
                {
                    commandResult = CommandReader.CommandResult(command, commandParams);
                }
                catch (Exception ex)
                {
                    commandResult = "Error: " + ex.Message;
                }

                Console.WriteLine(commandResult);
            }
        }
    }
}