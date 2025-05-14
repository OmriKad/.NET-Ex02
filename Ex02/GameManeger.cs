using System;
using Ex02;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public class GameManager
    {
        int m_CurrentNumberOfGuesses = 0;
        bool m_IsUserTryingToGameQuit;
        int m_MaxNumberOfGuesses;
        Board m_Board;
        GameLogic m_GameLogic;
        GameUI m_GameUI;


        public GameManager()
        {
            m_Board = new Board();
            m_GameLogic = new GameLogic();
            m_GameUI = new GameUI();
        }



        public void GameLoop()
        {
            while (m_IsUserTryingToGameQuit == false)
            {
                HandleUserInputInMenus();
                m_Board.PrintBoard();
                GetUserInputForGuesses();






            }

        }


        private void HandleUserInputInMenus()
        {
            System.Console.WriteLine("Please Enter The Number Of Guesses To Start The Game");
            System.Console.WriteLine("The number of guesses should not be lower then 4, and shall not excceed 10");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            string InputFromUser = $"{keyInfo.KeyChar}";
            if (int.TryParse(InputFromUser, out int InputConvertedToNumber))
            {
                m_IsUserTryingToGameQuit = false;
                if (InputConvertedToNumber >= 4 && InputConvertedToNumber <= 10) // magic numbers... fix this later
                {
                    m_MaxNumberOfGuesses = InputConvertedToNumber;
                }
                else
                {
                    System.Console.WriteLine("Your choice is Out of Bounds,Please pick a number from 4-10");
                }
            }
            else
            {
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    System.Console.WriteLine("Terminating Program...");
                    m_IsUserTryingToGameQuit = true;
                }
                else
                {
                    System.Console.WriteLine("Your input is not valid, please enter a number between 4 to 10");
                }

            }

        }


        private void GetUserInputForGuesses()
        {
            System.Console.WriteLine("please enter your guess : ");
            string PotentialGuess;
            PotentialGuess = System.Console.ReadLine();
            if (PotentialGuess.Length != 4)
            {
                System.Console.WriteLine("Your guess is not in the correct length, please enter a 4 letter string");
            }
            else
            {
                if (ContainsOnlyLetters(PotentialGuess))
                {
                    if (!ValidateGuess(PotentialGuess))
                    {
                        System.Console.WriteLine("Your guess contains characters that are not allowed, make sure to enter characters between A-H only");
                    }

                }
                else
                {
                    System.Console.WriteLine("Your guess does not contain only letters,Please try again"); 
                }

            }
            System.Console.WriteLine(PotentialGuess);
        }

        private bool ValidateGuess(string PotentialGuess)
        {
            bool isGuessValid = true;
            foreach (char c in PotentialGuess)
            {
                if (c > 'H' || c < 'A')
                {
                    isGuessValid = false;
                    break;
                }
            }
            return isGuessValid;
        }



        private bool ContainsOnlyLetters(string PotentialGuess)
        {
            bool isComprisedOfLetters = true;
            foreach (char c in PotentialGuess)
            {
                if (!char.IsLetter(c))
                {
                    isComprisedOfLetters = false;
                }
            }
            return isComprisedOfLetters; 
        }

    }
}
