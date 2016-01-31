namespace Theatre.CommandExecuters
{
    using System;
    using System.Collections.Generic;
    using Theatre.Core;

    public class CommandReader
    {
        public static string CommandResult(string command, string[] commandParams)
        {
            string commandResult;
            switch (command)
            {
                case "AddTheatre":
                    commandResult = TheatresCommandExecuter.ExecuteAddTheatreCommand(commandParams);
                    break;
                case "PrintAllTheatres":
                    commandResult = TheatresCommandExecuter.ExecutePrintAllTheatresCommand();
                    break;
                case "AddPerformance":
                    string performanceTitle;
                    DateTime startDateTime;
                    TimeSpan duration;
                    decimal price;
                    var theatreName = Engine.TheatreName(
                        commandParams,
                        out performanceTitle,
                        out startDateTime,
                        out duration,
                        out price);
                    PerformanceCommandExecuter.Universal.AddPerformance(
                        theatreName,
                        performanceTitle,
                        startDateTime,
                        duration,
                        price);
                    commandResult = "Performance added";
                    break;
                case "PrintAllPerformances":
                    commandResult = PerformanceCommandExecuter.ExecutePrintAllPerformancesCommand();
                    break;
                case "PrintPerformances":
                    var theatre = commandParams[0];
                    List<string> performances;
                    commandResult = CommandOutput.CommandResult(theatre, out performances);
                    break;
                default:
                    commandResult = "Invalid command!";
                    break;
            }
            return commandResult;
        }
    }
}