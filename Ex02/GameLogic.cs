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
        private const int k_MinNumOfGuesses = 4;
        private const  int k_MaxNumOfGuesses = 10;
        private const int k_ItemsEachGuess = 4;
        private const int k_NumnOfOptions = 8;
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
            for (int i = 0; i < k_ItemsEachGuess; i++)
            {
                bool generateFlag = false;
                do
                {
                    int randomNum = random.Next(1, k_NumnOfOptions);
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
            m_GuessesList.Clear();
            m_SecretItem.Clear();
            generateSecretItem();
        }

        public eGameResult CheckGuess(Guess i_Guess)
        {
            eGameResult result;
            if (m_GuessesList.Count + 1 < k_MaxNumOfGuesses)
            {
                m_GuessesList.Add(i_Guess);
                result = compareAndResolve();
            }
            else
            {
                result = eGameResult.Lose;
            }
            return result;
        }

        private eGameResult compareAndResolve()
        {
            Guess latestGuess = m_GuessesList.Last();
            bool allGreen = true;

            for(int i = 0; i < latestGuess.m_Inputs.Count; i++)
            {
                int locationInSecret = m_SecretItem.IndexOf(latestGuess.m_Inputs[i].m_GuessCode);
                if (locationInSecret == -1) // Not found in secret
                {
                    latestGuess.m_Inputs[i].m_GuessResult = eTurnResult.Red;
                    allGreen = false;
                }
                else if (locationInSecret == i)
                {
                    latestGuess.m_Inputs[i].m_GuessResult = eTurnResult.Green;
                }
                else
                {
                    latestGuess.m_Inputs[i].m_GuessResult = eTurnResult.Yellow;
                    allGreen = false;
                }
            }

            eGameResult result = allGreen ? eGameResult.Win : eGameResult.Pending;
            return result;
        }

        internal class Guess
        {
            internal List<GuessConstruct> m_Inputs;
        }

        internal class GuessConstruct
        {
            internal int m_GuessCode;
            internal eTurnResult m_GuessResult;
        }
    }
}