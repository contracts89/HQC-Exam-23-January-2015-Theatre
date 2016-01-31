namespace Theatre.Models
{
    using System;

    public class Performance : IComparable<Performance>
    {
        private string performanceName;

        private string theatreName;

        private decimal ticketPrice;

        public Performance(
            string theatreName,
            string performanceName,
            DateTime performanceDateTime,
            TimeSpan performanceDuration,
            decimal ticketPrice)
        {
            this.TheatreName = theatreName;
            this.PerformanceName = performanceName;
            this.PerformanceDateTime = performanceDateTime;
            this.PerformanceDuration = performanceDuration;
            this.TicketPrice = ticketPrice;
        }

        public DateTime PerformanceDateTime { get; set; }

        public TimeSpan PerformanceDuration { get; private set; }

        public string PerformanceName
        {
            get
            {
                return this.performanceName;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The name of the performance is invalid!", "performanceName");
                }
                this.performanceName = value;
            }
        }

        public string TheatreName
        {
            get
            {
                return this.theatreName;
            }

            protected internal set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("TheatreName", "The name of the theatre is invalid!");
                }
                this.theatreName = value;
            }
        }

        protected internal decimal TicketPrice
        {
            get
            {
                return this.ticketPrice;
            }
            protected set
            {
                if (value <= 0)
                {
                    throw new AggregateException("Invalid ticket price!");
                }
                this.ticketPrice = value;
            }
        }

        public override string ToString()
        {
            var result =
                string.Format(
                    "Performance(TeatreName: {0}; PerformanceName: {1};"
                    + " performanceDateTime: {2}, performanceDuration: {3}, TicketPrice: {4})",
                    this.TheatreName,
                    this.PerformanceName,
                    this.PerformanceDateTime.ToString("dd.MM.yyyy HH:mm"),
                    this.PerformanceDuration.ToString("hh':'mm"),
                    this.TicketPrice.ToString("f2"));
            return result;
        }

        int IComparable<Performance>.CompareTo(Performance otherPerformance)
        {
            var tmp = this.PerformanceDateTime.CompareTo(otherPerformance.PerformanceDateTime);
            return tmp;
        }
    }
}