﻿using Bookmaker.Core.Utils;
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
        public Team Team { get; protected set; } // todo
        public int Price { get; protected set; }                 
        public Score Score { get; protected set; }
        public DateTime CreatedAt { get; protected set; }        
        public DateTime LastUpdate { get; protected set; }

        protected Bet()
        {

        }

        public Bet(int price, User user, Match match, Team team, Score score)
        {            
            SetPrice(price);
            SetUser(user);
            SetMatch(match);
            SetTeam(team);
            SetScore(score);

            SetCreationDate();
        }

        public void SetId(int id)
        {
            if (id < 0)
            {
                throw new InvalidDataException($"Bet: Id cannot be set to '{ id }' (less than zero).");
            }

            Id = id;
        }

        public void SetUser(User user)
        {
            if (user == null)
            {
                throw new InvalidDataException("Bet: provided user is not valid.");
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
                throw new InvalidDataException("Bet: provided match is not valid.");
            }
            if (Match == match)
            {
                return;
            }

            Match = match;
            Update();
        }

        public void SetTeam(Team team)
        {
            if (team == null)
                throw new InvalidDataException("Bet: provided team is not valid.");

            if (Team == team)
                return;

            Team = team;
            Update();
        }

        public void SetPrice(int price)
        {
            if (price <= 0)
                throw new InvalidDataException("Bet: price must be greater than zero.");

            if (price > 1000000)
                throw new InvalidDataException("Bet: price cannot be greater than 1000000.");

            if (Price == price)
                return;

            Price = price;
            Update();
        }

        public void SetScore(Score score)
        {
            if (score == null)
                throw new InvalidDataException("Bet: provided score is not valid.");

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
