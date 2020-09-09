using System.Collections.Generic;

namespace compatabilityPredictor
{
    public class ComputeMaxSpecialty: CompatabilityScorer
    {
        protected Person teamMetric;

        public ComputeMaxSpecialty() {
            this.teamMetric = new Person();
            this.teamMetric.name = "Max Specialty";
        }

        public virtual double compute(Person applicant) {
            double bestSpecialty = 0.0;

            // for each stat, compare it to the team max.
            foreach(KeyValuePair<string, double> item in applicant.attributes)
            {
                // look for any value the applicant has that exceeds the team max
                if (item.Value > teamMetric.attributes[item.Key]) {
                    // calc the normal as the value between prev team max and perfect 10
                    double min = teamMetric.attributes[item.Key];
                    double max = 10;
                    double normalizedValue = (item.Value - min) / (max - min);

                    // we only care about the biggest normalized value among all the stats.
                    if(normalizedValue > bestSpecialty) {
                        bestSpecialty = normalizedValue;
                    }
                }
            }

            return bestSpecialty;
        }

        public virtual void preCompute(List<Person> team) {
            // initialize the team metric to the min value
            if (team.Count > 0) {
                Person person = team[0];
                foreach(KeyValuePair<string, double> item in person.attributes)
                {
                    teamMetric.attributes[item.Key] = 0.0;
                }
            }

            // iterate through each team member and their stats, to end up with the max val per stat for the whole team
            foreach(var person in team) {
                foreach(KeyValuePair<string, double> item in person.attributes)
                {
                    if (item.Value > teamMetric.attributes[item.Key]) {
                        teamMetric.attributes[item.Key] = item.Value;
                    }
                }
            }
        }
    }
}