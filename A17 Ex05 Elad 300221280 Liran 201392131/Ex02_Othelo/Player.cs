namespace Ex02_Othelo
{
    public class Player
    {
        private static int m_PlayersAmount = 0;
        private readonly string r_PlayerName;
        private int m_PlayerScore;
        private char m_PlayerWasher;
        private EnumUserType.e_UserType m_UserType;

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

        public EnumUserType.e_UserType PlayerType
        {
            get { return m_UserType; }
        }

        private void setWasher()
        {
            if (m_PlayersAmount % 2 == 0)
            {
                m_PlayerWasher = (char)EnumGameSigns.e_Signs.X;
            }
            else if (m_PlayersAmount % 2 == 1)
            {
                m_PlayerWasher = (char)EnumGameSigns.e_Signs.O;
            }
        }

        private void setType()
        {
            if (r_PlayerName.Equals("Computer"))
            {
                m_UserType = EnumUserType.e_UserType.Computer;
            }
            else
            {
                m_UserType = EnumUserType.e_UserType.Uman;
            }
        }
    }
}
