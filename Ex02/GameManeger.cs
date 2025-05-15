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
        Board m_Board;
        GameLogic m_GameLogic;


        private enum eGameState { Menu,Exit,Playing,GuessHandling,GameEnd}


        public GameManager()
        {
            m_Board = new Board();
            m_GameLogic = new GameLogic();
        }



        public void GameLoop()
        {
            while (m_GameState != eGameState.Exit)
            {
                if (m_GameState == eGameState.Menu)
                {
                    Ex02.ConsoleUtils.Screen.Clear(); // ani omo!!!
                    HandleUserInputInMenus();
                    if (m_GameState == eGameState.Playing)
                    {
                        m_GameLogic.StartGame();
                        GameUI.PrintSecretItem(m_GameLogic.GetSecertItem());
                    }
                }
                else if (m_GameState == eGameState.Playing)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    m_Board.PrintBoard();
                    m_GameState = eGameState.GuessHandling;

                }
                else if (m_GameState == eGameState.GuessHandling)
                {
                    GetUserInputForGuesses();
                    Ex02.ConsoleUtils.Screen.Clear();
                    m_Board.PrintBoard();
                    for (int i = 0; i < m_GameLogic.GetCurrentNumberOfGuesses(); i++)
                    {
                        GameUI.PrintGuessAndResult(m_GameLogic.SendSpecificGuessToUI(i), i);

                    }
                    Console.SetCursorPosition(0, Console.WindowHeight - 1);


                }
  

            }

        }


        private void HandleUserInputInMenus()
        {
            System.Console.WriteLine("Please enter the number of guesses to start the game.");
            System.Console.WriteLine("The number of guesses should not be lower than 4 and should not exceed 10.");
            System.Console.WriteLine("Type 'q' to quit.");

            string inputFromUser = System.Console.ReadLine();

            if (inputFromUser.ToLower() == "q")
            {
                Ex02.ConsoleUtils.Screen.Clear(); // ani omo!!!
                System.Console.WriteLine("Terminating Program...");
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
                    System.Console.WriteLine("Your choice is out of bounds. Please pick a number from 4 to 10.");
                }
            }
            else
            {
                System.Console.WriteLine("Your input is not valid. Please enter a number between 4 to 10, or 'q' to quit.");
            }
        }




        private void GetUserInputForGuesses()
        {
            System.Console.WriteLine("please enter your guess : ");
            string PotentialGuess;
            PotentialGuess = System.Console.ReadLine();
            if(PotentialGuess.ToLower() == "q")
            {
                Ex02.ConsoleUtils.Screen.Clear(); // ani omo!!!
                m_GameState = eGameState.Exit;
                System.Console.WriteLine("Bye");
            }
            else if (PotentialGuess.Length != 4)
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
                    else
                    {
                        GameLogic.Guess CurrGuess = m_GameLogic.ConvertIntToGuess(GameUI.convertStringToInt(PotentialGuess));
                        m_GameLogic.CheckGuess(CurrGuess);
                    }

                }
                else
                {
                    System.Console.WriteLine("Your guess does not contain only letters,Please try again"); 
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
