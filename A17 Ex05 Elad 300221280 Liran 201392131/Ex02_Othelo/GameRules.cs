using System;
using System.Collections.Generic;

namespace Ex02_Othelo
{
    public class GameRules
    {
        private Board m_Board = null;
        private Point? m_StartRange = null;
        private Point? m_EndRange = null;
		private Random m_RandomGenerator = new Random();
		private List<Point> m_LegalMovesForComputer = new List<Point>();
        private List<int> m_ScoreList = new List<int>();
        
        public GameRules(ref Board io_Board)
        {
            m_Board = io_Board;
            m_StartRange = new Point();
            m_EndRange = new Point(io_Board.Boardsize - 1, io_Board.Boardsize - 1);
        }

        private char GetAgainstCurrentPlayerSign(Player i_Player)
        {
            char sign = (char)EnumGameSigns.e_Signs.None;
            switch (i_Player.PlayerWasher)
            {
                case (char)EnumGameSigns.e_Signs.X:
                    {
                        sign = (char)EnumGameSigns.e_Signs.O;
                        break;
                    }

                case (char)EnumGameSigns.e_Signs.O:
                    {
                        sign = (char)EnumGameSigns.e_Signs.X;
                        break;
                    }
            }

            return sign;
        }

        public bool ThereIsExisitingLegalMove(Player i_Player)
        {
            bool moveResult = false;
			bool breakLoop = false;
            m_LegalMovesForComputer.Clear();
            for (int i = 1; i < m_Board.Boardsize - 1; i++)
            {
                for (int j = 1; j < m_Board.Boardsize - 1; j++)
                {
                    if (m_Board.GetCellOnBoard(i, j).SignValue == (char)EnumGameSigns.e_Signs.None)
                    {
                        Point checkPoint = new Point(i, j);
                        moveResult = LegalMove(i_Player, checkPoint, EnumCheckOrChange.e_OnlyCheck.Yes);
                        if (moveResult)
                        {
                            if (i_Player.PlayerType.Equals(EnumUserType.e_UserType.Computer))
                            {
                                m_LegalMovesForComputer.Add(checkPoint);
                            }
                            else
                            {
                                breakLoop = true;
                                break;
                            }
                        }
                    }
                }

				if (breakLoop)
				{
					break;
				}
            }

			if (i_Player.PlayerType.Equals(EnumUserType.e_UserType.Computer))
			{
                moveResult = m_LegalMovesForComputer.Count > 0;	
			}
	
            return moveResult;
        }

        public bool LegalMove(Player i_Player)
		{
			bool moveResult = false;
			Point randomPoint = m_LegalMovesForComputer[m_RandomGenerator.Next(m_LegalMovesForComputer.Count)];
			moveResult = LegalMove(i_Player, randomPoint, EnumCheckOrChange.e_OnlyCheck.No);
            return moveResult;
        }

        public bool LegalMove(Player i_Player, Point i_Point, EnumCheckOrChange.e_OnlyCheck i_Mode)
        {
            bool moveResult = false;
            if (m_Board.GetCellOnBoard(i_Point).SignValue == (char)EnumGameSigns.e_Signs.None)
            {
                if (i_Point > m_StartRange && i_Point < m_EndRange)
                {
                    moveResult = SummaryCallsCheckLegalMoveNotInBorders(i_Player, i_Point, i_Mode);
                }
            }

            return moveResult;
        }

        private bool SummaryCallsCheckLegalMoveNotInBorders(Player i_Player, Point i_Point, EnumCheckOrChange.e_OnlyCheck i_Mode)
        {
            bool[] legalMoves = new bool[8];
            int indexLegalMove;
            indexLegalMove = 0;
            legalMoves[indexLegalMove++] = UpdateBorder(i_Player, i_Point, EnumRules.e_Rules.UpperLeftDiagonal, i_Mode);
            legalMoves[indexLegalMove++] = UpdateBorder(i_Player, i_Point, EnumRules.e_Rules.UpperRow, i_Mode);
            legalMoves[indexLegalMove++] = UpdateBorder(i_Player, i_Point, EnumRules.e_Rules.UpperRightDiagonal, i_Mode);
            legalMoves[indexLegalMove++] = UpdateBorder(i_Player, i_Point, EnumRules.e_Rules.RightColoum, i_Mode);
            legalMoves[indexLegalMove++] = UpdateBorder(i_Player, i_Point, EnumRules.e_Rules.DownRightDiagonal, i_Mode);
            legalMoves[indexLegalMove++] = UpdateBorder(i_Player, i_Point, EnumRules.e_Rules.DownRow, i_Mode);
            legalMoves[indexLegalMove++] = UpdateBorder(i_Player, i_Point, EnumRules.e_Rules.DownLeftDiagonal, i_Mode);
            legalMoves[indexLegalMove++] = UpdateBorder(i_Player, i_Point, EnumRules.e_Rules.LeftColoumn, i_Mode);
            return IndicateForLegalMoves(legalMoves);
        }

		private bool UpdateBorder(Player i_Player, Point i_Point, EnumRules.e_Rules i_Rule, EnumCheckOrChange.e_OnlyCheck i_Mode)
        {
            Point copyPoint = new Point(i_Point.AxisXValue, i_Point.AxisYValue);
            char washerAgainstPlayer = GetAgainstCurrentPlayerSign(i_Player);
            int amountOppositeWasher = 0;
            bool moveResult = false;
            while (copyPoint > m_StartRange && copyPoint < m_EndRange)
            {
                copyPoint = CalculateCellsToChange(copyPoint, i_Rule);
                if (m_Board.GetCellOnBoard(copyPoint).SignValue == washerAgainstPlayer)
                {
                    amountOppositeWasher++;
                }
                else if (amountOppositeWasher > 0 && m_Board.GetCellOnBoard(copyPoint).SignValue == i_Player.PlayerWasher)
                {
                    amountOppositeWasher++;
                    moveResult = true;
                    break;
                }
                else
                {
                    break;
                }
            }

            if (moveResult && i_Mode == EnumCheckOrChange.e_OnlyCheck.No)
            {
                for (int i = 0; i < amountOppositeWasher; i++)
                {
                    copyPoint = RecoverPreviousPointValue(copyPoint, i_Rule);
                    m_Board.SetSignCellOnBoard(i_Player.PlayerWasher, copyPoint);
                }
            }

            return moveResult;
        }

        private bool IndicateForLegalMoves(bool[] i_LegalMoves)
        {
            bool wasLegal = false;
            foreach (bool legalMove in i_LegalMoves)
            {
                if (legalMove)
                {
                    wasLegal = legalMove;
                    break;
                }
            }

            return wasLegal;
        }

        private Point CalculateCellsToChange(Point i_Point, EnumRules.e_Rules i_Rule)
        {
            switch (i_Rule)
            {
                case EnumRules.e_Rules.UpperLeftDiagonal:
                    {
                        i_Point.AxisXValue -= 1;
                        i_Point.AxisYValue -= 1;
                        break;
                    }

                case EnumRules.e_Rules.UpperRow:
                    {
                        i_Point.AxisXValue -= 1;
                        break;
                    }

                case EnumRules.e_Rules.UpperRightDiagonal:
                    {
                        i_Point.AxisXValue -= 1;
                        i_Point.AxisYValue += 1;
                        break;
                    }

                case EnumRules.e_Rules.RightColoum:
                    {
                        i_Point.AxisYValue += 1;
                        break;
                    }

                case EnumRules.e_Rules.DownRightDiagonal:
                    {
                        i_Point.AxisYValue += 1;
                        i_Point.AxisXValue += 1;
                        break;
                    }

                case EnumRules.e_Rules.DownRow:
                    {
                        i_Point.AxisXValue += 1;
                        break;
                    }

                case EnumRules.e_Rules.DownLeftDiagonal:
                    {
                        i_Point.AxisXValue += 1;
                        i_Point.AxisYValue -= 1;
                        break;
                    }

                case EnumRules.e_Rules.LeftColoumn:
                    {
                        i_Point.AxisYValue -= 1;
                        break;
                    }
            }

            return i_Point;
        }

        private Point RecoverPreviousPointValue(Point i_Point, EnumRules.e_Rules i_Rule)
        {
            switch (i_Rule)
            {
                case EnumRules.e_Rules.UpperLeftDiagonal:
                    {
                        i_Point.AxisXValue += 1;
                        i_Point.AxisYValue += 1;
                        break;
                    }

                case EnumRules.e_Rules.UpperRow:
                    {
                        i_Point.AxisXValue += 1;
                        break;
                    }

                case EnumRules.e_Rules.UpperRightDiagonal:
                    {
                        i_Point.AxisXValue += 1;
                        i_Point.AxisYValue -= 1;
                        break;
                    }

                case EnumRules.e_Rules.RightColoum:
                    {
                        i_Point.AxisYValue -= 1;
                        break;
                    }

                case EnumRules.e_Rules.DownRightDiagonal:
                    {
                        i_Point.AxisYValue -= 1;
                        i_Point.AxisXValue -= 1;
                        break;
                    }

                case EnumRules.e_Rules.DownRow:
                    {
                        i_Point.AxisXValue -= 1;
                        break;
                    }

                case EnumRules.e_Rules.DownLeftDiagonal:
                    {
                        i_Point.AxisXValue -= 1;
                        i_Point.AxisYValue += 1;
                        break;
                    }

                case EnumRules.e_Rules.LeftColoumn:
                    {
                        i_Point.AxisYValue += 1;
                        break;
                    }
            }

            return i_Point;
        }

        public List<int> GameScore
        {
             get
            {
                m_ScoreList.Clear();
                int firstPlayerScore = 0;
                int secondPlayerScore = 0;
                for (int i = 1; i < m_Board.Boardsize - 1; i++)
                {
                    for (int j = 1; j < m_Board.Boardsize - 1; j++)
                    {
                        if (m_Board.CellBoard[i, j].SignValue == (char)EnumGameSigns.e_Signs.O)
                        {
                            secondPlayerScore++;
                        }
                        
                        if (m_Board.CellBoard[i, j].SignValue == (char)EnumGameSigns.e_Signs.X)
                        {
                            firstPlayerScore++;
                        }
                    }
                }

                m_ScoreList.Add(firstPlayerScore);
                m_ScoreList.Add(secondPlayerScore);
                return m_ScoreList;
            }
        }
    }
}
