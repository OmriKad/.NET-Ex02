using System.Collections.Generic;

namespace Ex02
{
    public class GameManager
    {
        private int m_ChosenNumberOfGuesses = 0;
        private bool m_GameWon = false;
        private List<Guess> m_GuessesList = new List<Guess>();
        private Secret m_Secret = new Secret();
        private GameUi m_GameUi = new GameUi();

        public void GameLoop()
        {
            m_GameUi.DisplayWelcomeMessage();
            while (true)
            {
                m_ChosenNumberOfGuesses = m_GameUi.ShowMenuAndGetNumOfGuesses();
                initGame();
                bool quit = runGuessLoop(m_ChosenNumberOfGuesses);
                if (quit)
                {
                    break;
                }

                if (!m_GameUi.AskForNewGame())
                {
                    break;
                }
            }

            m_GameUi.DisplayExitMessage();
        }

        private bool runGuessLoop(int i_GuessLimit)
        {
            bool wantsToQuit = false;
            string message = Messages.GuessRequest();
            TurnStatus.eGameStatus guessStatus;
            for (int i = 0; i < i_GuessLimit; i++)
            {
                string guess = "";
                bool validGuess = false;
                while(!validGuess && !wantsToQuit)
                {
                    m_GameUi.DisplayBoard(m_GuessesList, m_Secret, message, false);
                    guess = m_GameUi.GetGuessFromUser(out guessStatus);
                    if (guessStatus == TurnStatus.eGameStatus.InvalidLength)
                    {
                        message = Messages.InvalidGuessLength();
                    }
                    else if (guessStatus == TurnStatus.eGameStatus.InvalidChar)
                    {
                        message = Messages.InvalidGuessLetters();
                    }
                    else if (guessStatus == TurnStatus.eGameStatus.RepeatingChars)
                    {
                        message = Messages.RepeatingChars();
                    }
                    else if (guessStatus == TurnStatus.eGameStatus.Quit)
                    {
                        wantsToQuit = true;
                    }
                    else
                    {
                        message = Messages.GuessRequest();
                        validGuess = true;
                    }
                }

                if (wantsToQuit) 
                { 
                    break;
                }

                Guess inputGuess = new Guess(guess);
                m_GuessesList.Add(inputGuess);
                m_GameWon = inputGuess.CompareWithSecret(m_Secret);
                if(m_GameWon)
                {
                    break;
                }
            }

            if (m_GameWon)
            {
                m_GameUi.DisplayBoard(m_GuessesList, m_Secret, Messages.PrintWinMessage(), true);
            }
            else if (!m_GameWon)
            {
                m_GameUi.DisplayBoard(m_GuessesList, m_Secret, Messages.PrintLoseMessage(), true);
            }

            return wantsToQuit;
        }
        private void initGame()
        {
            m_GuessesList.Clear();
            m_Secret.ClearSecret();
            m_Secret.GenerateSecret();
        }
    }
}