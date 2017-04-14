using Bookmaker.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Team
    {        
        public int Id { get; protected set; }
        public Stadium Stadium { get; protected set; }
        public string Name { get; protected set; }
        public IEnumerable<Match> Matches { get; protected set; }

        protected Team()
        {

        }

        public Team(Stadium stadium, string name)
        {
            SetStadium(stadium);
            SetName(name);
        }

        public void SetId(int id)
        {
            if (id < 0)
            {
                throw new InvalidDataException($"Team: Id cannot be set to '{ id }' (less than zero).");
            }

            Id = id;
        }

        public void SetStadium(Stadium stadium)
        {
            if (stadium == null)
                throw new InvalidDataException("Team: provided stadium is not valid.");

            if (Stadium == stadium)
                return;

            Stadium = stadium;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidDataException("Team: provided name cannot be empty.");

            if (Name == name.ToLowerInvariant())
                return;

            Name = name;
        }
    }
}
