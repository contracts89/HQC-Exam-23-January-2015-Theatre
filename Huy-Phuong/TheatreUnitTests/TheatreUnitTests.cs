namespace TheatreUnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Theatre.CommandExecuters;
    using Theatre.Exceptions;
    using Theatre.Models;

    [TestClass]
    public class TheatreUnitTests
    {
        private SortedDictionary<string, SortedSet<Performance>> sortedDictionaryStringSortedSetPerformance;

        [TestMethod]
        public void AddPerformanceMethod_AddPerformance_ShouldIncreasePerformanceCount()
        {
            var performanceCount = PerformanceCommandExecuter.Universal.ListAllPerformances().Count();
            var playDateTime = "10.10.2015 10:15";
            var playTimeSpan = "1:00";

            PerformanceCommandExecuter.Universal.AddTheatre("Bubeshki teatar");
            PerformanceCommandExecuter.Universal.AddPerformance(
                "Bubeshki teatar", 
                "Bubenica", 
                DateTime.ParseExact(playDateTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture), 
                TimeSpan.Parse(playTimeSpan), 
                2.00M);

            var newPerformanceCount = PerformanceCommandExecuter.Universal.ListAllPerformances().Count();

            Assert.AreNotEqual(performanceCount, newPerformanceCount);
        }

        [TestMethod]
        public void ListPerformancesTest_NoPerformances_ShouldHaveZeroCount()
        {
            const int Count = 0;

            PerformanceCommandExecuter.Universal.AddTheatre("Bukov theatre");
            var performancesCount = PerformanceCommandExecuter.Universal.ListPerformances("Bukov theatre").Count();

            Assert.AreEqual(Count, performancesCount);
        }

        [TestMethod]
        [ExpectedException(typeof(TheatreNotFoundException))]
        public void ListPerformancesTest_NoThearte_ShoudThrowNoTheatre()
        {
            PerformanceCommandExecuter.Universal.ListPerformances("Vazov");
        }

        [TestMethod]
        public void ListPerformancesTest_OnePerformance_ShouldReturnValidCounter()
        {
            const int Count = 1;

            var performancesCount = PerformanceCommandExecuter.Universal.ListPerformances("Bubeshki teatar").Count();

            Assert.AreEqual(Count, performancesCount);
        }

        [TestMethod]
        public void ListTheartes_AddTheatres_ShouldIncreseCount()
        {
            var timeSpan = "1:00";
            var dateTime = "01.01.2016 12:30";

            PerformanceCommandExecuter.Universal.AddTheatre("Salza i smqh");
            PerformanceCommandExecuter.Universal.AddPerformance(
                "Salza i smqh", 
                "Figaro", 
                DateTime.ParseExact(dateTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture), 
                TimeSpan.Parse(timeSpan), 
                1.40M);

            Assert.IsNotNull(this.sortedDictionaryStringSortedSetPerformance.Count);
        }

        [TestMethod]
        public void ListTheatres_NoTheatres_ShoudReturnNoTheatre()
        {
            const int Count = 3;
            var theartesCount = PerformanceCommandExecuter.Universal.ListTheatres().Count();

            Assert.AreEqual(Count, theartesCount, "Should return the proper count");
        }

        [TestInitialize]
        public void StratUp()
        {
            this.sortedDictionaryStringSortedSetPerformance = new SortedDictionary<string, SortedSet<Performance>>();
        }

        [TestMethod]
        [ExpectedException(typeof(TimeDurationOverlapException))]
        public void TestAddPerformanceMethod_DurartionOverlap_ShoudThrowException()
        {
            var timeSpan = "1:00";
            var dateTime = "01.01.2016 12:30";

            PerformanceCommandExecuter.Universal.AddPerformance(
                "Salza i smqh", 
                "The Beast", 
                DateTime.ParseExact(dateTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture), 
                TimeSpan.Parse(timeSpan), 
                1.40M);
        }

        [TestMethod]
        [ExpectedException(typeof(TheatreNotFoundException))]
        public void TestAddPerformanceMethod_NoTheatre_ShoudThrowException()
        {
            var timeSpan = "0:30";
            var dateTime = "02.01.2016 10:30";

            PerformanceCommandExecuter.Universal.AddPerformance(
                "Mladeshki", 
                "The Beast", 
                DateTime.ParseExact(dateTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture), 
                TimeSpan.Parse(timeSpan), 
                1.40M);
        }
    }
}