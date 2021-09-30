using BotInterface.Bot;
using BotInterface.Game;
using System;
using System.Collections.Generic;

namespace ExampleBot
{
    public class ExampleBot : IBot
    {
        public Move MakeMove(Gamestate gamestate)
        {
            int myDynamite = 100;
            int opponentDynamite = 100;
            var rounds = gamestate.GetRounds();

            for (int i = 0; i < rounds.Length; i++)
            {
                if (rounds[i].GetP1() == Move.D) myDynamite--;
                if (rounds[i].GetP2() == Move.D) opponentDynamite--;
            }

            int roundsToAverage = Math.Min(rounds.Length, 20);
            var options = new List<Move>();

            for (int i = rounds.Length - roundsToAverage; i < rounds.Length; i++)
            {
                switch (rounds[i].GetP2())
                {
                    case Move.P:
                        options.Add(Move.S);
                        break;
                    case Move.R:
                        options.Add(Move.P);
                        break;
                    case Move.S:
                        options.Add(Move.R);
                        break;
                    case Move.D:
                        if (opponentDynamite > 0) options.Add(Move.W);
                        break;
                }
            }

            if (rounds.Length > 400)
            {
                if (myDynamite > 1) options.Add(Move.D);
                if (myDynamite > 2) options.Add(Move.D);
                if (myDynamite > 3) options.Add(Move.D);
            }
            if (rounds.Length > 600)
            {
                if (myDynamite > 1) options.Add(Move.D);
                if (myDynamite > 2) options.Add(Move.D);
                if (myDynamite > 3) options.Add(Move.D);
            }

            if (options.Count == 0) options.Add(Move.D);

            var random = new Random();
            int index = random.Next(options.Count);
            return options[index];
        }
    }
}
