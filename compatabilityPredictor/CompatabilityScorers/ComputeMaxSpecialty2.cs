using System.Collections.Generic;

namespace compatabilityPredictor
{
    public class ComputeMaxSpecialty2: ComputeMaxSpecialty
    {
        public override void preCompute(List<Person> team) {
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