using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Bet
    {        
        public int Id { get; protected set; }
        public User User { get; protected set; }
        public Match Match { get; protected set; }
        public Team Team { get; protected set; } // TODO: potrzebne?        
        public int Price { get; protected set; }                 
        public Score Score { get; protected set; }
        public DateTime CreatedAt { get; protected set; }        
        public DateTime LastUpdate { get; protected set; }

        protected Bet()
        {

        }

        public Bet(int price, User user, Match match, Score score)
        {            
            SetPrice(price);
            SetUser(user);
            SetMatch(match);
            SetScore(score);

            SetCreationDate();
        }

        public void SetId(int id)
        {
            if (id < 0)
            {
                throw new Exception($"Bet: Id cannot be set to '{ id }' (less than zero).");
            }

            Id = id;
        }

        public void SetUser(User user)
        {
            if (user == null)
            {
                throw new Exception("Bet: provided user is not valid.");
            }
            if (User == user)
            {
                return;
            }

            User = user;
            Update();
        }

        public void SetMatch(Match match)
        {
            if (match == null)
            {
                throw new Exception("Bet: provided match is not valid.");
            }
            if (Match == match)
            {
                return;
            }

            Match = match;
            Update();
        }

        public void SetTeamI(Team team)
        {
            if (team == null)
                throw new Exception("Bet: provided team is not valid.");

            if (Team == team)
                return;

            Team = team;
            Update();
        }

        public void SetPrice(int price)
        {
            if (price <= 0)
                throw new Exception("Bet: price must be greater than zero.");

            if (price > 1000000)
                throw new Exception("Bet: price cannot be greater than 1000000.");

            if (Price == price)
                return;

            Price = price;
            Update();
        }

        public void SetScore(Score score)
        {
            if (score == null)
                throw new Exception("Bet: provided score is not valid.");

            if (score.Goals < 0)
                throw new Exception("Bet: goals must be greater than or equal to zero.");

            if (score.Goals > 100)
                throw new Exception("Bet: cannot bet for more goals than 100.");

            if (score.Shots < 0)
                throw new Exception("Bet: shots must be greater than or equal to zero.");

            if (score.Shots > 10000)
                throw new Exception("Bet: cannot bet for more shots than 10000.");

            if (Score == score)
                return;

            Score = score;
            Update();
        }

        private void Update()
        {
            LastUpdate = DateTime.UtcNow;
        }

        private void SetCreationDate()
        {
            CreatedAt = DateTime.UtcNow;
            Update();
        }
    }
}
