using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class Board
    {
        private Cell[,] m_ArrayCell = null;
        private int m_Size = 0;

        public Board(int i_Size)
        {
            m_Size = i_Size + 2;
            m_ArrayCell = new Cell[m_Size, m_Size];
            initBoard();
        }

        public void UpdateSignCellOnBoardByPoint(char i_Sign, Point i_point)
        {
            m_ArrayCell[i_point.AxisXValue, i_point.AxisYValue].SignValue = i_Sign;
        }

        public Cell GetCellOnBoard(Point i_point)
        {
            return m_ArrayCell[i_point.AxisXValue, i_point.AxisYValue];
        }

        public Cell CellOnBoardByLocation(int i_x, int i_y)
        {
            return m_ArrayCell[i_x, i_y];
        }

        public Cell[,] CellBoard
        {
            get { return m_ArrayCell; }
        }

        public int Boardsize
        {
            get { return m_Size; }
        }

        private void initBoard()
        {
            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    m_ArrayCell[i, j] = new Cell((char)eGameSigns.None, i, j);
                }
            }

            if (m_Size == (int)eBoardSize.Eight)
            {
                m_ArrayCell[3, 4].SignValue = (char)eGameSigns.X;
                m_ArrayCell[4, 3].SignValue = (char)eGameSigns.X;
                m_ArrayCell[3, 3].SignValue = (char)eGameSigns.O;
                m_ArrayCell[4, 4].SignValue = (char)eGameSigns.O;
            }
            else if (m_Size == (int)eBoardSize.Ten)
            {
                m_ArrayCell[4, 5].SignValue = (char)eGameSigns.X;
                m_ArrayCell[5, 4].SignValue = (char)eGameSigns.X;
                m_ArrayCell[4, 4].SignValue = (char)eGameSigns.O;
                m_ArrayCell[5, 5].SignValue = (char)eGameSigns.O;
            }
            else if (m_Size == (int)eBoardSize.Twelve)
            {
                m_ArrayCell[5, 6].SignValue = (char)eGameSigns.X;
                m_ArrayCell[6, 5].SignValue = (char)eGameSigns.X;
                m_ArrayCell[5, 5].SignValue = (char)eGameSigns.O;
                m_ArrayCell[6, 6].SignValue = (char)eGameSigns.O;
            }
            else if (m_Size == (int)eBoardSize.Fourteen)
            {
                m_ArrayCell[6, 7].SignValue = (char)eGameSigns.X;
                m_ArrayCell[7, 6].SignValue = (char)eGameSigns.X;
                m_ArrayCell[6, 6].SignValue = (char)eGameSigns.O;
                m_ArrayCell[7, 7].SignValue = (char)eGameSigns.O;
            }
        }
    }
}
