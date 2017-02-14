using System;
using System.Collections.Generic;
using System.Text;
using Ex02_Othelo;

namespace Ex05.OtheloUI
{
    public class GameController
    {
        static int m_GameType;
        static string m_FirstPlayerName;
        static string m_SecondPlayerName;
        static int m_BoardSize;

        public static int GameType
        {
            get
            {
                return m_GameType;
            }
            set
            {
                m_GameType = value;
            }
        }

        public static string FirstPlayerName
        {
            get
            {
                return m_FirstPlayerName;
            }
            set
            {
                m_FirstPlayerName = value;
            }
        }

        public static string SecondPlayerName
        {
            get
            {
                return m_SecondPlayerName;
            }
            set
            {
                m_SecondPlayerName = value;
            }
        }

        public static int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
            set
            {
                m_BoardSize = value;
            }
        }
    }
}
