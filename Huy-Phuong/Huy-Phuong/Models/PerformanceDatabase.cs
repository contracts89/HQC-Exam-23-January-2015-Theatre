namespace Theatre.Models
{
    using System;
    using System.Collections.Generic;
    using Theatre.Contracts;
    using Theatre.Exceptions;

    internal class PerformanceDatabase : IPerformanceDatabase
    {
        private readonly SortedDictionary<string, SortedSet<Performance>> sortedDictionaryStringSortedSetPerformance =
            new SortedDictionary<string, SortedSet<Performance>>();

        public void AddTheatre(string tt)
        {
            if (this.sortedDictionaryStringSortedSetPerformance.ContainsKey(tt))
            {
                throw new DuplicateTheatreException("Duplicate theatre");
            }

            this.sortedDictionaryStringSortedSetPerformance[tt] = new SortedSet<Performance>();
        }

        public IEnumerable<Performance> ListAllPerformances()
        {
            var theatres = this.sortedDictionaryStringSortedSetPerformance.Keys;

            var result2 = new List<Performance>();
            foreach (var t in theatres)
            {
                var performances = this.sortedDictionaryStringSortedSetPerformance[t];
                result2.AddRange(performances);
            }

            return result2;
        }

        public IEnumerable<string> ListTheatres()
        {
            var theatres = this.sortedDictionaryStringSortedSetPerformance.Keys;
            return theatres;
        }

        void IPerformanceDatabase.AddPerformance(
            string theatreName,
            string performanceName,
            DateTime performanceDateTime,
            TimeSpan performanceDuration,
            decimal ticketPrice)
        {
            if (!this.sortedDictionaryStringSortedSetPerformance.ContainsKey(theatreName))
            {
                throw new TheatreNotFoundException("Theatre does not exist");
            }

            var ps = this.sortedDictionaryStringSortedSetPerformance[theatreName];

            var e2 = performanceDateTime + performanceDuration;
            if (PerformanceOverlapCheck(ps, performanceDateTime, e2))
            {
                throw new TimeDurationOverlapException("Time/duration overlap");
            }

            var p = new Performance(theatreName, performanceName, performanceDateTime, performanceDuration, ticketPrice);
            ps.Add(p);
        }

        IEnumerable<Performance> IPerformanceDatabase.ListPerformances(string theatreName)
        {
            if (!this.sortedDictionaryStringSortedSetPerformance.ContainsKey(theatreName))
            {
                throw new TheatreNotFoundException("Theatre does not exist");
            }
            var asfd = this.sortedDictionaryStringSortedSetPerformance[theatreName];
            return asfd;
        }

        protected internal static bool PerformanceOverlapCheck(
            IEnumerable<Performance> performances,
            DateTime performanceDateTime,
            DateTime peformanceDuration)
        {
            foreach (var performance in performances)
            {
                var dateOfPerformance = performance.PerformanceDateTime;
                var performanceTimeSpan = performance.PerformanceDateTime + performance.PerformanceDuration;
                var result = (dateOfPerformance <= performanceDateTime && performanceDateTime <= performanceTimeSpan)
                             || (dateOfPerformance <= peformanceDuration && peformanceDuration <= performanceTimeSpan)
                             || (performanceDateTime <= dateOfPerformance && dateOfPerformance <= peformanceDuration)
                             || (performanceDateTime <= performanceTimeSpan && performanceTimeSpan <= peformanceDuration);
                if (result)
                {
                    return true;
                }
            }

            return false;
        }
    }
}