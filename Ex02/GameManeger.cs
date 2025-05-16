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
        eGameState m_GameState = eGameState.Menu;
        Board m_Board = new Board();
        GameLogic m_GameLogic = new GameLogic();
        GameUi m_GameUi = new GameUi();

        private enum eGameState { Menu,Exit,Playing,GuessHandling,GameEnd}

        public void GameLoop()
        {
            while (m_GameState != eGameState.Exit)
            {
                if (m_GameState == eGameState.Menu)
                {
                    HandleUserInputInMenus();
                    if (m_GameState == eGameState.Playing)
                    {
                        m_GameLogic.StartGame();
                    }
                }
                else if (m_GameState == eGameState.Playing)
                {
                    m_Board.PrintBoard();
                    m_GameState = eGameState.GuessHandling;
                }
                else if (m_GameState == eGameState.GuessHandling)
                {
                    GetUserInputForGuesses();
                    m_Board.PrintBoard();
                    for (int i = 0; i < m_GameLogic.GetCurrentNumberOfGuesses(); i++)
                    {
                        GameUi.PrintGuessAndResult(m_GameLogic.SendSpecificGuessToUi(i), i);

                    }
                    Console.SetCursorPosition(0, Console.WindowHeight - 1);

                }
            }
        }

        private void HandleUserInputInMenus()
        {
            Console.WriteLine("Please enter the number of guesses to start the game.");
            Console.WriteLine("The number of guesses should not be lower than 4 and should not exceed 10.");
            Console.WriteLine("Type 'q' to quit.");

            string inputFromUser = Console.ReadLine();

            if (inputFromUser.ToLower() == "q")
            {
                ConsoleUtils.Screen.Clear(); // ani omo!!!
                Console.WriteLine("Terminating Program...");
                m_GameState = eGameState.Exit;
            }
            else if (int.TryParse(inputFromUser, out int inputConvertedToNumber))
            {
                if (inputConvertedToNumber >= 4 && inputConvertedToNumber <= 10)
                {
                    m_MaxNumberOfGuesses = inputConvertedToNumber;
                    m_GameState = eGameState.Playing;
                }
                else
                {
                    Console.WriteLine("Your choice is out of bounds. Please pick a number from 4 to 10.");
                }
            }
            else
            {
                Console.WriteLine("Your input is not valid. Please enter a number between 4 to 10, or 'q' to quit.");
            }
        }




        private void GetUserInputForGuesses()
        {
            Console.WriteLine("please enter your guess : ");
            string PotentialGuess;
            PotentialGuess = Console.ReadLine();
            if(PotentialGuess.ToLower() == "q")
            {
                ConsoleUtils.Screen.Clear(); // ani omo!!!
                m_GameState = eGameState.Exit;
                Console.WriteLine("Bye");
            }
            else if (PotentialGuess.Length != 4)
            {
                Console.WriteLine("Your guess is not in the correct length, please enter a 4 letter string");
            }
            else
            {
                if (ContainsOnlyLetters(PotentialGuess))
                {
                    if (!ValidateGuess(PotentialGuess))
                    {
                        Console.WriteLine("Your guess contains characters that are not allowed, make sure to enter characters between A-H only");
                    }
                    else
                    {
                        GameLogic.Guess CurrGuess = m_GameLogic.ConvertIntToGuess(GameUi.convertStringToInt(PotentialGuess));
                        m_GameLogic.CheckGuess(CurrGuess);
                    }

                }
                else
                {
                    Console.WriteLine("Your guess does not contain only letters,Please try again"); 
                }

            }
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
