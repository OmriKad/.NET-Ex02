using System;
using System.Collections.Generic;

namespace Ex02
{
    public class GameUi
    {
        private readonly Board m_Board = new Board();

        public void DisplayWelcomeMessage()
        {
            Messages.PrintWelcomeMessage();
        }

        public int ShowMenuAndGetNumOfGuesses()
        {
            bool isValidInput = false;
            int numOfGuesses = 0;
            while (!isValidInput)
            {
                Messages.PrintInputRequest();
                string input = Console.ReadLine();
                isValidInput = int.TryParse(input, out numOfGuesses);
                if (!isValidInput)
                {
                    Messages.PrintNumOfGuessInvalidInput();
                }
                else if (numOfGuesses > Settings.m_MaxNumOfGuesses || numOfGuesses < Settings.m_MinNumOfGuesses)
                {
                    isValidInput = false;
                    Messages.PrintInvalidNumOfGuesses();
                }
            }
            return numOfGuesses;
        }

        internal bool AskForNewGame()
        {
            bool isValidInput = false;
            string input = "";
            while (!isValidInput)
            {
                Messages.PrintNewGameRequest();
                input = Console.ReadLine();
                if (input.ToLower() == "y" || input.ToLower() == "n")
                {
                    isValidInput = true;
                }
                else
                {
                    Messages.PrintInvalidNewGameInput();
                }
            }
            return input.ToLower() == "y";
        }

        internal bool CheckIfQuit(string i_Guess)
        {
            i_Guess = i_Guess.ToLower();
            return i_Guess == "q";
        }

        internal void DisplayBoard(List<Guess> i_GuessesList, Secret i_SecretItem, string i_Feedback, bool i_GameEnded) // Skeleton for upcoming display method change
        {
            m_Board.RenderBoard(i_GuessesList, i_SecretItem, i_Feedback, i_GameEnded);
        }

        internal void DisplayExitMessage()
        {
            Messages.PrintQuitMessage();
        }

        internal string GetGuessFromUser(out TurnStatus.eGameStatus o_GuessStatus)
        {
            string inputFromUser = "";
            inputFromUser = Console.ReadLine(); 
            bool containsOnlyValidLetters = Guess.ContainsOnlyValidLetters(inputFromUser); 
            bool correctLength = Guess.IsInCorrectLength(inputFromUser);
            bool containsReapeatingLetters = Guess.CheckIfHasRepeatingChars(inputFromUser);
            if (CheckIfQuit(inputFromUser))
            { 
                o_GuessStatus = TurnStatus.eGameStatus.Quit;
            }
            else if (!correctLength) 
            { 
                o_GuessStatus = TurnStatus.eGameStatus.InvalidLength;

            }
            else if (!containsOnlyValidLetters)
            {
                o_GuessStatus = TurnStatus.eGameStatus.InvalidChar;
            }
            else if (containsReapeatingLetters)
            {
                o_GuessStatus = TurnStatus.eGameStatus.RepeatingChars;
            }
            else
            {
                o_GuessStatus = TurnStatus.eGameStatus.Valid;
            }
            
            return inputFromUser;
        }
    }
}