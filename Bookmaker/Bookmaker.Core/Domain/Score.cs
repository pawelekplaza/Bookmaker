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

        protected Score()
        {

        }

        public Score(int goals, int shots)
        {

        }

        public void SetId(int id)
        {
            if (id < 0)
            {
                throw new Exception($"Score: Id cannot be set to '{ id }' (less than zero).");
            }

            Id = id;
        }
    }
}
