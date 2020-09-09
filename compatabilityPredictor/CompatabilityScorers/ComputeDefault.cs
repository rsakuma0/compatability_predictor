using System.Collections.Generic;

namespace compatabilityPredictor
{
    public class ComputeDefault: CompatabilityScorer
    {
        public ComputeDefault() {
        }

        public double compute(Person applicant) {
            // everybody passes!!!!!!!
            return 1.0;
        }

        public void preCompute(List<Person> team) {
        }
    }
}