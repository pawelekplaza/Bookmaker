using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Result
    {        
        public int Id { get; protected set; }        
        public Score HostScore { get; protected set; }        
        public Score GuestScore { get; protected set; }

        protected Result()
        {

        }

        public Result(Score hostScore, Score guestScore)
        {

        }

        public void SetId(int id)
        {
            if (id < 0)
            {
                throw new Exception($"Result: Id cannot be set to '{ id }' (less than zero).");
            }

            Id = id;
        }
    }
}
