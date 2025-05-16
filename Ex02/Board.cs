
using System;
using System.Text;

namespace Ex02
{
    class Board
    {

        private const string k_BoardSkeleton = @"
    Current board status:

    Pins:        Result:    
   |============|============|
   |  # # # #   |            |
   |------------|------------|
   |            |            |
   |------------|------------|
   |            |            |
   |------------|------------|
   |            |            |
   |------------|------------|
   |            |            |
   |------------|------------|
   |            |            |
   |------------|------------|
   |            |            |
   |------------|------------|
   |            |            |
   |------------|------------|
   |            |            |
   |------------|------------|
   |            |            |
   |------------|------------|
   |            |            |
   |------------|------------|
   |============|============|

    ";

        public void PrintBoard()
        {
            System.Console.WriteLine(k_BoardSkeleton);
        }


        public void BuildCorrespondingGuessLine()//string Guess,string Result ,uint GuessIndex
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("a b c d");  /// Guess takes 7 blank spaces
            sb.Append("  |  ");
            sb.Append("a b c d");
            Console.SetCursorPosition(7,6); // need to multiple by int
            System.Console.WriteLine(sb.ToString());
            Console.SetCursorPosition(0, 0);
        }

    }

}

