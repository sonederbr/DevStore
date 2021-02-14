using System.Collections.Generic;

using DevStore.Core.DomainObjects;

namespace DevStore.Catalog.Domain
{
    public class Specification : ValueObject
    {
        public int TotalTime { get; private set; }
        public int NumberOfClasses { get; private set; }

        public Specification(int totalTime, int numberOfClasses)
        {
            AssertionConcern.ValidarSeMenorQue(totalTime, 1, "O campo Largura não pode ser menor ou igual a 0");
            AssertionConcern.ValidarSeMenorQue(numberOfClasses, 1, "O campo Profundidade não pode ser menor ou igual a 0");

            TotalTime = totalTime;
            NumberOfClasses = numberOfClasses;
        }

        public string FormatedDescription()
        {
            return $"Duração: {TotalTime} Número de aulas: {NumberOfClasses}";
        }

        public override string ToString()
        {
            return FormatedDescription();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return TotalTime;
            yield return NumberOfClasses;
        }
    }
}