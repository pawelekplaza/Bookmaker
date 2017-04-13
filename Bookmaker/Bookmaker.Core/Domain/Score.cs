using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Score
    {   
        public int Id { get; protected set; }
        public int Goals { get; protected set; }
        public int Shots { get; protected set; }
        public IEnumerable<Bet> Bets { get; protected set; }

        protected Score()
        {

        }

        public Score(int goals, int shots)
        {
            SetGoals(goals);
            SetShots(shots);
        }

        public void SetId(int id)
        {
            if (id < 0)
            {
                throw new Exception($"Score: Id cannot be set to '{ id }' (less than zero).");
            }

            Id = id;
        }

        public void SetGoals(int goals)
        {
            if (goals < 0)
                throw new Exception("Score: number of goals cannot be less than zero.");

            if (goals > 100)
                throw new Exception("Score: number of goals cannot be greater than 100.");

            if (Goals == goals)
                return;

            Goals = goals;
        }

        public void SetShots(int shots)
        {
            if (shots < 0)
                throw new Exception("Score: number of shots cannot be less than zero.");

            if (shots > 10000)
                throw new Exception("Score: number of shots cannot be greater than 10000.");

            if (Shots == shots)
                return;

            Shots = shots;
        }
    }
}
