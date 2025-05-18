using System;
using System.Linq;

namespace Ex02
{
    public class Secret
    {
        private string m_Secret = "";

        public string SecretValue
        {
            get { return m_Secret; }
        }

        public void GenerateSecret()
        {
            Random random = new Random();
            while (m_Secret.Length < Settings.m_NumOfPins)
            {
                char randomChar = (char)('A' + random.Next(Settings.m_NumOfOptions));
                if (!m_Secret.Contains(randomChar))
                {
                    m_Secret += randomChar;
                }
            }
        }

        internal void ClearSecret()
        {
            m_Secret = "";
        }
    }
}