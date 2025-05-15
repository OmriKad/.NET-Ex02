using System;
using System.Collections.Generic;
using System.Linq;
using Ex02;


/// use of internal might not be neccery here, we will need to check it together

namespace Ex02
{
    public class GameLogic
    {
        private static int m_MinNumOfGuesses = 4;
        private static int m_MaxNumOfGuesses = 10;
        private static int m_ItemsEachGuess = 4;
        private static int m_NumnOfOptions = 8;
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


        public Guess SendSpecificGuessToUI(int i) { return m_GuessesList[i]; }
        public int GetCurrentNumberOfGuesses() { return m_GuessesList.Count; }

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
            if (m_GuessesList.Count + 1 < m_MaxNumOfGuesses)
            {
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

        public struct Guess
        {
            public List<GuessConstruct> m_Inputs;
        }

        public struct GuessConstruct
        {
            public int guessCode;
            public eTurnResult guessResult;
        }

        public Guess ConvertIntToGuess(int GuessCode)
        {
            Guess NewGuess = new Guess();
            NewGuess.m_Inputs = new List<GuessConstruct>(); // 1234
            for (int i = 0; i < 4; i++)
            {
                GuessConstruct NewGuessConstruct = new GuessConstruct();
                NewGuessConstruct.guessCode = GuessCode % 10;
                NewGuess.m_Inputs.Insert(0, NewGuessConstruct);
                GuessCode = GuessCode / 10;
            }
            return NewGuess;
        }


        public List<int> GetSecertItem()
        {
            return m_SecretItem;
        }
    }
}