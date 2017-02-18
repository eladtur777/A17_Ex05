namespace Ex02_Othelo
{
    public class Player
    {
        private static int m_PlayersAmount = 0;
        private readonly string r_PlayerName;
        private int m_PlayerScore;
        private char m_PlayerWasher;
        private eUserType m_UserType;

        public Player(string i_PlayerName)
        {
            r_PlayerName = i_PlayerName;
            m_PlayerScore = 2;
            setWasher();
            m_PlayersAmount++;
            setType();
        }

        public char PlayerWasher
        {
            get { return m_PlayerWasher; }
        }

        public int PlayerScore
        {
            get { return m_PlayerScore; }
            set { m_PlayerScore = value; }
        }

        public string PlayerName
        {
            get { return r_PlayerName; }
        }

        public eUserType PlayerType
        {
            get { return m_UserType; }
        }

        private void setWasher()
        {
            if (m_PlayersAmount % 2 == 0)
            {
                m_PlayerWasher = (char)eGameSigns.X;
            }
            else if (m_PlayersAmount % 2 == 1)
            {
                m_PlayerWasher = (char)eGameSigns.O;
            }
        }

        private void setType()
        {
            if (r_PlayerName.Equals("Computer"))
            {
                m_UserType = eUserType.Computer;
            }
            else
            {
                m_UserType = eUserType.Uman;
            }
        }
    }
}