using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex02.GameLogic;

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




        public static int convertStringToInt(string userInput)
        {
            int currGuess = 0;
            for (int i = 0; i < 4; i++)
            {
                currGuess = currGuess * 10;
                currGuess += (int)(userInput[i] - 64);
            }
            return currGuess;  /// 1814
        }


        public static void PrintGuessAndResult(GameLogic.Guess CurrentGuess,int GuessIndex)
        {
            StringBuilder CurrentLine = ConvertGuessToStringBuilder(CurrentGuess);
            CurrentLine = ProccessResult(CurrentGuess, CurrentLine);
            Console.SetCursorPosition(7 + GuessIndex, 6);
            System.Console.WriteLine(CurrentLine.ToString());
            Console.SetCursorPosition(0,0);
        }


        private static StringBuilder ConvertGuessToStringBuilder(GameLogic.Guess CurrentGuess)
        {
            char CurrentLetter;
            StringBuilder Guess = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                CurrentLetter = (char)(CurrentGuess.m_Inputs[i].guessCode + 64);
                Guess.Append(CurrentLetter);
                Guess.Append(" ");
            }

            return Guess;

        }



        private static StringBuilder ProccessResult(GameLogic.Guess CurrentGuess, StringBuilder CurrentLine)
        {
            GameLogic.eTurnResult CurrentResult;
            int numberOfV = 0;
            int NumberOfX = 0;
            int NumberOfSpaces = 0;
            for (int i = 0; i < 4; i++)
            {
                CurrentResult = CurrentGuess.m_Inputs[i].guessResult;
                switch (CurrentResult)
                {
                    case GameLogic.eTurnResult.Red:
                        NumberOfSpaces++;
                        break;
                    case GameLogic.eTurnResult.Green:
                        numberOfV++;
                        break;
                    case GameLogic.eTurnResult.Yellow:
                        NumberOfX++;
                        break;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (numberOfV > 0)
                {
                    CurrentLine.Append("V");
                    numberOfV--;
                }
                else if (NumberOfX > 0)
                {
                    CurrentLine.Append("X");
                    NumberOfX--;
                }
                else if (NumberOfSpaces > 0)
                {
                    CurrentLine.Append(" ");
                    NumberOfSpaces--;
                }
            }
            return CurrentLine;

        }

        public static void PrintSecretItem(List<int> SecertItem)
        {
            StringBuilder SecretItemString = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                char CurrentChar = (char)(SecertItem[i] + 64);
                SecretItemString.Append(CurrentChar);
                SecretItemString.Append(" ");
            }
            System.Console.WriteLine(SecretItemString.ToString());
        }
    }
}
