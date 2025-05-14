using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal class Program
    {
        public static void Main()
        {
            GameLogic game = new GameLogic();
            game.StartGame();
            GameLogic.Guess guess = new GameLogic.Guess
            {
                m_Inputs = new List<GameLogic.GuessConstruct>
                   {
                       new GameLogic.GuessConstruct { guessCode = 1},
                       new GameLogic.GuessConstruct { guessCode = 2},
                       new GameLogic.GuessConstruct { guessCode = 3},
                       new GameLogic.GuessConstruct { guessCode = 4}
                   }
            };
            GameLogic.eGameResult result = game.CheckGuess(guess);
        }
    }
}
