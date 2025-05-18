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
            for (int i = 0; i < i_GuessLimit; i++)
            {
                string inputFromUser = "";
                bool validGuess = false;
                while (!validGuess && !wantsToQuit)
                {
                    m_GameUi.DisplayBoard(m_GuessesList, m_Secret, message, false);
                    inputFromUser = m_GameUi.GetGuessFromUser(out TurnStatus.eGameStatus guessStatus);
                    switch (guessStatus)
                    {
                        case TurnStatus.eGameStatus.InvalidLength:
                            message = Messages.InvalidGuessLength();
                            break;
                        case TurnStatus.eGameStatus.InvalidChar:
                            message = Messages.InvalidGuessLetters();
                            break;
                        case TurnStatus.eGameStatus.RepeatingChars:
                            message = Messages.RepeatingChars();
                            break;
                        case TurnStatus.eGameStatus.Quit:
                            wantsToQuit = true;
                            break;
                        case TurnStatus.eGameStatus.Valid:
                        default:
                            message = Messages.GuessRequest();
                            validGuess = true;
                            break;
                    }
                }

                if (wantsToQuit)
                {
                    break;
                }

                Guess inputGuess = new Guess(inputFromUser);
                m_GuessesList.Add(inputGuess);
                m_GameWon = inputGuess.CompareWithSecret(m_Secret);
                if (m_GameWon)
                {
                    break;
                }
            }

            switch (m_GameWon)
            {
                case true:
                    m_GameUi.DisplayBoard(m_GuessesList, m_Secret, Messages.PrintWinMessage(), true);
                    break;
                case false:
                    m_GameUi.DisplayBoard(m_GuessesList, m_Secret, Messages.PrintLoseMessage(), true);
                    break;
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