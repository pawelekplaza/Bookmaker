using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Wallet
    {
        public int Id { get; protected set; }
        public User User { get; protected set; }       
        public int Points { get; protected set; }

        protected Wallet()
        {

        }

        public Wallet(User user, int points)
        {

        }

        public void SetId(int id)
        {
            if (id < 0)
            {
                throw new Exception($"Wallet: Id cannot be set to '{ id }' (less than zero).");
            }

            Id = id;
        }

        public void SetUser(User user)
        {
            if (user == null)
                throw new Exception("Wallet: provided user is not valid.");

            if (User == user)
                return;

            User = user;
        }

        public void SetPoints(int points)
        {
            if (points < 0)
                throw new Exception("Wallet: number of points cannot be less than zero.");

            if (Points == points)
                return;

            Points = points;
        }
    }
}
