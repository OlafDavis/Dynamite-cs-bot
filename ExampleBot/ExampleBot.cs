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

            var options = new List<Move> { Move.R, Move.S, Move.P };

            if (myDynamite > 0) options.Add(Move.D);
            if (opponentDynamite > 0 && opponentDynamite < 100) options.Add(Move.W);

            var random = new Random();
            int index = random.Next(options.Count);
            return options[index];
        }
    }
}
