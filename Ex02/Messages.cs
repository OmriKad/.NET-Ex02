using System;
using System.Threading;

namespace Ex02
{
    public class Messages
    {
        public static void PrintWelcomeMessage()
        {
            ConsoleUtils.Screen.Clear();
            Console.WriteLine("Welcome to the game!");
            Console.WriteLine($"You have {Settings.m_MinNumOfGuesses}-{Settings.m_MaxNumOfGuesses} guesses to guess the secret item.");
            Console.WriteLine("Good luck!");
            Thread.Sleep(4000);
        }

        public static void PrintInputRequest()
        {
            ConsoleUtils.Screen.Clear();
            Console.WriteLine("Please enter the number of guesses you want to play with:");
        }

        public static void PrintNumOfGuessInvalidInput()
        {
            ConsoleUtils.Screen.Clear();
            Console.WriteLine("Invalid input. Please enter a number.");
            Thread.Sleep(3000);
        }

        public static void PrintInvalidNumOfGuesses()
        {
            ConsoleUtils.Screen.Clear();
            Console.WriteLine($"Invalid number of guesses. Please enter a number between {Settings.m_MinNumOfGuesses} and {Settings.m_MaxNumOfGuesses}.");
            Thread.Sleep(3000);
        }

        public static string GuessRequest()
        {
            return "Enter a guess of A-H or q to quit:";
        }

        public static string InvalidGuessLetters()
        {
            return "Invalid guess. Please enter a guess with only A-H letters.";
        }

        public static string InvalidGuessLength()
        {
            return $"Invalid guess. Please enter a guess with {Settings.m_NumOfPins} letters.";
        }

        internal static void PrintQuitMessage()
        {
            ConsoleUtils.Screen.Clear();
            Console.WriteLine("Thanks for playing!");
            Thread.Sleep(3000);
        }

        public static string PrintWinMessage()
        {
            return "Congratulations! You guessed the secret item!";
        }

        public static string PrintLoseMessage()
        {
            return "Sorry, you didn't guess the secret item. Better luck next time!";
        }

        public static void PrintNewGameRequest()
        {
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
            ConsoleUtils.Screen.Clear();
            Console.WriteLine("Do you want to play again? (y/n)");
        }

        public static void PrintInvalidNewGameInput()
        {
            ConsoleUtils.Screen.Clear();
            Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            Thread.Sleep(3000);
        }

        public static string RepeatingChars()
        {
            return "Invalid guess. Please enter a guess with no repeating letters.";
        }
    }
}