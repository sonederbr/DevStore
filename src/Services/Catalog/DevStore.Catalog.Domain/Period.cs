using System;
using System.Collections.Generic;

using DevStore.Core.DomainObjects;

namespace DevStore.Catalog.Domain
{
    public class Period : ValueObject
    {
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public Period(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public string FormatedDescription()
        {
            return $"Start: {StartDate} end: {EndDate}";
        }

        public override string ToString()
        {
            return FormatedDescription();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return StartDate;
            yield return EndDate;
        }
    }
}