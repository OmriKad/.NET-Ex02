using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public class GameUI
    {

        public static StringBuilder convertGuessToString(int currGuess)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                char c = (char)(currGuess + 64);
            }
            return sb;
        }




        public static int convertStringToGuess(string userInput)
        {
            int currGuess = 0;
            for (int i = 0; i < 4; i++)
            {
                currGuess += (int)(userInput[i] - 64);
                currGuess = currGuess * 10;
            }
            return currGuess;
        }
    }
}
