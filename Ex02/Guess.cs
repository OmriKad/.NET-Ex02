using System.Collections.Generic;

namespace Ex02
{
    public class Guess
    {
        private const int m_NumOfItemsInGuess = 4;

        private List<Item> m_GuessItems;

        public static Guess ConvertSringToGuess(string i_Guess)
        {
            Guess guess = new Guess();
            for (int i = 0; i < m_NumOfItemsInGuess; i++)
            {
                Item guessItem = new Item();
                foreach (char letter in i_Guess)
                {
                    guessItem = Item.ConvertCharToItem(letter);
                }
                guess.m_GuessItems.Add(guessItem);
            }
            return guess;
        }

        private string guessToString()
        {
            string newString = "";
            for (int i = 0; i < m_NumOfItemsInGuess; i++)
            {
                newString += (m_GuessItems[i].ItemLetterToChar());
            }
            return newString;
        }
    }
}
