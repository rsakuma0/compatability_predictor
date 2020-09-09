using System;
using System.Collections.Generic;

namespace compatabilityPredictor
{
    public class ComputeMostAverage: CompatabilityScorer
    {
        private Person teamMetric;

        public ComputeMostAverage() {
            this.teamMetric = new Person();
            this.teamMetric.name = "Most Average";
        }

        public double compute(Person applicant) {
            // looking for a normalized value representing how close the attributes are between team and applicant
            // perfectly landing on the average is 1.0.
            // the further we are from that val in either direction reduces the score.
            // we can choose an arbitrary max dist. 5 or 10 makes sense, as its a 10 point scale, or 5 from either side of the middle.
            double totalNormalValues = 0.0;
            foreach(KeyValuePair<string, double> item in applicant.attributes)
            {
                double diff = Math.Abs(teamMetric.attributes[item.Key] - applicant.attributes[item.Key]);
                double normalizedAttribute = diff >= 5.0 ? 0.0
                                                         : (5.0 - diff) / 5.0;
                totalNormalValues += normalizedAttribute;
            }

            totalNormalValues /= applicant.attributes.Count;
            return totalNormalValues;
        }

        public void preCompute(List<Person> team) {
            // initialize the team metric to the min value
            if (team.Count > 0) {
                Person person = team[0];
                foreach(KeyValuePair<string, double> item in person.attributes)
                {
                    teamMetric.attributes[item.Key] = 0.0;
                }
            }

            // iterate through each team member and compute the average for each stat
            foreach(var person in team) {
                foreach(KeyValuePair<string, double> item in person.attributes)
                {
                    teamMetric.attributes[item.Key] += item.Value;
                }
            }
            if (team.Count > 0) {
                Person person = team[0];
                foreach(KeyValuePair<string, double> item in person.attributes)
                {
                    teamMetric.attributes[item.Key] /= team.Count;
                }
            }
        }
    }
}