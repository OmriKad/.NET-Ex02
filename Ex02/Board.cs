
using System;

namespace Ex02
{
    public class Board
    {
        public void PrintHeadAndSecret(string i_Secert = "# # # #")
        {
            System.Console.WriteLine($@"
| Pins:    |Result:     |
|==========|============|
|{i_Secert}|            |
|==========|============|
");
        }

        public void PrintGessAndResultLine(string i_Guess, string i_Result)
        {
            System.Console.WriteLine(
                $@"
|{i_Guess} |{i_Result}  |
|==========|============|
");
        }
    }
}