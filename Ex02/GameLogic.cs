using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal class GameLogic
    {
        private static int m_MinNumOfGuesses = 4;
        private static int m_MaxNumOfGuesses = 10;
        private static int m_ItemsEachGuess = 4;
        private static int m_NumnOfOptions = 8;
        private static int m_NumOfGuesses = 0;
        public enum eTurnResult
        { 
            Red,
            Yellow,
            Green
        }

        public enum eGameResult
        {
            Win,
            Lose,
            Pending
        }
        private List<Guess> m_GuessesList = new List<Guess>();
        private List<int> m_SecretItem = new List<int>();


        private void generateSecretItem()
        {
            Random random = new Random();
            for (int i = 0; i < m_ItemsEachGuess; i++)
            {
                bool generateFlag = false;
                do
                {
                    int randomNum = random.Next(1, m_NumnOfOptions);
                    if (!m_SecretItem.Contains(randomNum))
                    {
                        m_SecretItem.Add(randomNum);
                        generateFlag = true;
                    }
                }

                while(!generateFlag);
            }
        }

        public void StartGame()
        {
            m_NumOfGuesses = 0;
            m_GuessesList.Clear();
            m_SecretItem.Clear();
            generateSecretItem();
        }

        public eGameResult CheckGuess(Guess i_Guess)
        {
            eGameResult result;
            if (m_NumOfGuesses + 1 < m_MaxNumOfGuesses)
            {
                m_NumOfGuesses++;
                m_GuessesList.Add(i_Guess);
                result = compareAndResolve(ref i_Guess);
            }
            else
            {
                result = eGameResult.Lose;
            }
            return result;
        }

        private eGameResult compareAndResolve(ref Guess i_Guess)
        {
            for (int i = 0; i < i_Guess.m_Inputs.Count; i++)
            {
                int letter = i_Guess.m_Inputs[i].guessCode;
                int letterLocation = m_SecretItem.IndexOf(letter);
                GuessConstruct temp = i_Guess.m_Inputs[i];
                if (letterLocation != -1)
                {
                    temp.guessResult = i == letterLocation ? eTurnResult.Green : eTurnResult.Yellow;
                }
                else
                {
                    temp.guessResult = eTurnResult.Red;
                }
                i_Guess.m_Inputs[i] = temp;
            }
            // check if all letters are green, if so, return win else return pending
            bool allGreen = true;
            for (int i = 0; i < i_Guess.m_Inputs.Count; i++)
            {
                if (i_Guess.m_Inputs[i].guessResult != eTurnResult.Green)
                {
                    allGreen = false;
                    break;
                }
            }
            eGameResult result = allGreen ? eGameResult.Win : eGameResult.Pending;
            return result;
        }

        internal struct Guess
        {
            internal List<GuessConstruct> m_Inputs;
        }

        internal struct GuessConstruct
        {
            internal int guessCode;
            internal eTurnResult guessResult;
        }
    }
}