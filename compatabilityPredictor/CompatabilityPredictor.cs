using System.Collections.Generic;

namespace compatabilityPredictor
{
    public class CompatabilityPredictor
    {
        private List<Person> team;
        private List<Person> applicants;
        private CompatabilityScorer scoringMethod;

        public CompatabilityPredictor(UserInput userInput) {
            this.team = userInput.team;
            this.applicants = userInput.applicants;
            this.setScoringMethod(new ComputeDefault());
        }

        public void setScoringMethod(CompatabilityScorer scoringMethod) {
            this.scoringMethod = scoringMethod;
        }

        public List<ApplicantScore> ScoreApplicants() {
            List<ApplicantScore> applicantScores = new List<ApplicantScore>();
            this.scoringMethod.preCompute(this.team);

            foreach(var person in this.applicants) {
                ApplicantScore score = new ApplicantScore();
                score.name = person.name;
                score.score = scoringMethod.compute(person);
                applicantScores.Add(score);
            }
            return applicantScores;
        }
    }
}