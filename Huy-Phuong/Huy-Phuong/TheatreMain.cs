namespace Theatre
{
    using System.Globalization;
    using System.Threading;
    using Theatre.Core;

    internal class TheatreMain
    {
        protected static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            var theatreEngine = new Engine();

            theatreEngine.Run();
        }
    }
}