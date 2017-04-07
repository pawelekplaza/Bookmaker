using System;
using System.Collections.Generic;
using System.Text;

namespace Bookmaker.Core.Domain
{
    public class Bet
    {
        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public Guid MatchId { get; protected set; }
        public Guid TeamId { get; protected set; } // TODO: potrzebne?
        public int Price { get; protected set; }         
        public Score Score { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime LastUpdate { get; protected set; }

        protected Bet()
        {

        }

        public Bet(int price, Guid userId, Guid matchId, Score score)
        {
            Id = Guid.NewGuid();
            SetPrice(price);
            SetUserId(userId);
            SetMatchId(matchId);
            SetScore(score);

            SetCreationDate();
        }

        public void SetUserId(Guid userId)
        {
            if (userId == null)
            {
                throw new Exception("Bet: provided user id is not valid.");
            }
            if (UserId == userId)
            {
                return;
            }

            UserId = userId;
            Update();
        }

        public void SetMatchId(Guid matchId)
        {
            if (matchId == null)
            {
                throw new Exception("Bet: provided match id is not valid.");
            }
            if (MatchId == matchId)
            {
                return;
            }

            MatchId = matchId;
            Update();
        }

        public void SetTeamId(Guid teamId)
        {
            if (teamId == null)
                throw new Exception("Bet: provided team id is not valid.");

            if (TeamId == teamId)
                return;

            TeamId = teamId;
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
