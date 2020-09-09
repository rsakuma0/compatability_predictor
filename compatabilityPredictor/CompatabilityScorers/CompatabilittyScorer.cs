using System.Collections.Generic;

namespace compatabilityPredictor
{
    public interface CompatabilityScorer
    {
        double compute(Person applicant);

        void preCompute(List<Person> team);
    }
}