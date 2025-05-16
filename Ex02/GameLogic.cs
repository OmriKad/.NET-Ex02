using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex02
{
    public class GameLogic
    {

        private static int s_MinNumOfGuesses = 4;
        private static int s_MaxNumOfGuesses = 10;
        private static int s_ItemsEachGuess = 4;
        private static int s_NumnOfOptions = 8;

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



        public Guess SendSpecificGuessToUi(int i_I) { return m_GuessesList[i_I]; }
        public int GetCurrentNumberOfGuesses() { return m_GuessesList.Count; }

        private void generateSecretItem()
        {
            Random random = new Random();
            for (int i = 0; i < s_ItemsEachGuess; i++)
            {
                bool generateFlag = false;
                do
                {
                    int randomNum = random.Next(1, s_NumnOfOptions);
                    if (!m_SecretItem.Contains(randomNum))
                    {
                        m_SecretItem.Add(randomNum);
                        generateFlag = true;
                    }
                }

                while (!generateFlag);
            }
        }

        public void StartGame()
        {
            m_GuessesList.Clear();
            m_SecretItem.Clear();
            generateSecretItem();
        }

        public eGameResult CheckGuess(GameLogic.Guess i_Guess)
        {
            eGameResult result;

            if (m_GuessesList.Count + 1 < s_MaxNumOfGuesses)
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

        public class Guess
        {
            public List<GuessConstruct> m_Inputs;

            public Guess ConvertIntToGuess(int i_GuessCode)
            {
                Guess newGuess = new Guess
                {
                    m_Inputs = new List<GuessConstruct>()
                };
                for (int i = 0; i < 4; i++)
                {
                    GuessConstruct newGuessConstruct = new GuessConstruct
                    {
                        m_GuessCode = i_GuessCode % 10
                    };
                    newGuess.m_Inputs.Insert(0, newGuessConstruct);
                    i_GuessCode /= 10;
                }
                return newGuess;
            }
        }

        public class GuessConstruct
        {
            public int m_GuessCode;
            public eTurnResult m_GuessResult;
        }




        public List<int> GetSecertItem()
        {
            return m_SecretItem;
        }
    }
}