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

        public event DelegateContainer.Updater<char, Point> UpdatingSignValue;

        public event DelegateContainer.Updater<Point> UpdatingLegalityCellOption;

        public GameRules(ref Board io_Board)
        {
            m_Board = io_Board;
            m_StartRange = new Point();
            m_EndRange = new Point(io_Board.Boardsize - 1, io_Board.Boardsize - 1);
        }

        private char GetAgainstCurrentPlayerSign(Player i_Player)
        {
            char sign = (char)eGameSigns.None;
            switch (i_Player.PlayerWasher)
            {
                case (char)eGameSigns.X:
                    {
                        sign = (char)eGameSigns.O;
                        break;
                    }

                case (char)eGameSigns.O:
                    {
                        sign = (char)eGameSigns.X;
                        break;
                    }
            }

            return sign;
        }

        public bool ThereIsExisitingLegalMove(Player i_Player)
        {
            bool moveResult = false;
            m_LegalMovesForComputer.Clear();
            for (int i = 1; i < m_Board.Boardsize - 1; i++)
            {
                for (int j = 1; j < m_Board.Boardsize - 1; j++)
                {
                    if (m_Board.CellOnBoardByLocation(i, j).SignValue == (char)eGameSigns.None)
                    {
                        Point checkPoint = new Point(i, j);
                       
                        if (LegalMove(i_Player, checkPoint, eOnlyCheck.Yes))
                        {
                            moveResult = true;
                            if (i_Player.PlayerType.Equals(eUserType.Computer))
                            {
                                m_LegalMovesForComputer.Add(checkPoint);
                            }
                        }   
                    }
                }
            }

            if (i_Player.PlayerType.Equals(eUserType.Computer))
            {
                moveResult = m_LegalMovesForComputer.Count > 0;
            }

            return moveResult;
        }

        public bool LegalMove(Player i_Player)
        {
            bool moveResult = false;
            Point randomPoint = m_LegalMovesForComputer[m_RandomGenerator.Next(m_LegalMovesForComputer.Count)];
            moveResult = LegalMove(i_Player, randomPoint, eOnlyCheck.No);
            return moveResult;
        }

        public bool LegalMove(Player i_Player, Point i_Point, eOnlyCheck i_Mode)
        {
            bool moveResult = false;
            if (m_Board.GetCellOnBoard(i_Point).SignValue == (char)eGameSigns.None)
            {
                if (i_Point > m_StartRange && i_Point < m_EndRange)
                {
                    moveResult = SummaryCallsCheckLegalMoveNotInBorders(i_Player, i_Point, i_Mode);
                }
            }

            return moveResult;
        }

        private bool SummaryCallsCheckLegalMoveNotInBorders(Player i_Player, Point i_Point, eOnlyCheck i_Mode)
        {
            bool[] legalMoves = new bool[8];
            int indexLegalMove;
            indexLegalMove = 0;
            legalMoves[indexLegalMove++] = UpdateBoard(i_Player, i_Point, eDirections.UpperLeftDiagonal, i_Mode);
            legalMoves[indexLegalMove++] = UpdateBoard(i_Player, i_Point, eDirections.UpperRow, i_Mode);
            legalMoves[indexLegalMove++] = UpdateBoard(i_Player, i_Point, eDirections.UpperRightDiagonal, i_Mode);
            legalMoves[indexLegalMove++] = UpdateBoard(i_Player, i_Point, eDirections.RightColoum, i_Mode);
            legalMoves[indexLegalMove++] = UpdateBoard(i_Player, i_Point, eDirections.DownRightDiagonal, i_Mode);
            legalMoves[indexLegalMove++] = UpdateBoard(i_Player, i_Point, eDirections.DownRow, i_Mode);
            legalMoves[indexLegalMove++] = UpdateBoard(i_Player, i_Point, eDirections.DownLeftDiagonal, i_Mode);
            legalMoves[indexLegalMove++] = UpdateBoard(i_Player, i_Point, eDirections.LeftColoumn, i_Mode);
            return IndicateForLegalMoves(legalMoves);
        }

        private bool UpdateBoard(Player i_Player, Point i_Point, eDirections i_Rule, eOnlyCheck i_Mode)
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

            if (moveResult && i_Mode == eOnlyCheck.No)
            {
                for (int i = 0; i < amountOppositeWasher; i++)
                {
                    copyPoint = RecoverPreviousPointValue(copyPoint, i_Rule);
                    m_Board.UpdateSignCellOnBoardByPoint(i_Player.PlayerWasher, copyPoint);
                    OnUpdateCellSignValue(i_Player.PlayerWasher, copyPoint);
                }
            }
            else if (moveResult && i_Mode == eOnlyCheck.Yes)
            {
                for (int i = 0; i < amountOppositeWasher; i++)
                {
                    copyPoint = RecoverPreviousPointValue(copyPoint, i_Rule);
                }

                OnUpdateCellOption(copyPoint);
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

        private Point CalculateCellsToChange(Point i_Point, eDirections i_Rule)
        {
            switch (i_Rule)
            {
                case eDirections.UpperLeftDiagonal:
                    {
                        i_Point.AxisXValue -= 1;
                        i_Point.AxisYValue -= 1;
                        break;
                    }

                case eDirections.UpperRow:
                    {
                        i_Point.AxisXValue -= 1;
                        break;
                    }

                case eDirections.UpperRightDiagonal:
                    {
                        i_Point.AxisXValue -= 1;
                        i_Point.AxisYValue += 1;
                        break;
                    }

                case eDirections.RightColoum:
                    {
                        i_Point.AxisYValue += 1;
                        break;
                    }

                case eDirections.DownRightDiagonal:
                    {
                        i_Point.AxisYValue += 1;
                        i_Point.AxisXValue += 1;
                        break;
                    }

                case eDirections.DownRow:
                    {
                        i_Point.AxisXValue += 1;
                        break;
                    }

                case eDirections.DownLeftDiagonal:
                    {
                        i_Point.AxisXValue += 1;
                        i_Point.AxisYValue -= 1;
                        break;
                    }

                case eDirections.LeftColoumn:
                    {
                        i_Point.AxisYValue -= 1;
                        break;
                    }
            }

            return i_Point;
        }

        private Point RecoverPreviousPointValue(Point i_Point, eDirections i_Rule)
        {
            switch (i_Rule)
            {
                case eDirections.UpperLeftDiagonal:
                    {
                        i_Point.AxisXValue += 1;
                        i_Point.AxisYValue += 1;
                        break;
                    }

                case eDirections.UpperRow:
                    {
                        i_Point.AxisXValue += 1;
                        break;
                    }

                case eDirections.UpperRightDiagonal:
                    {
                        i_Point.AxisXValue += 1;
                        i_Point.AxisYValue -= 1;
                        break;
                    }

                case eDirections.RightColoum:
                    {
                        i_Point.AxisYValue -= 1;
                        break;
                    }

                case eDirections.DownRightDiagonal:
                    {
                        i_Point.AxisYValue -= 1;
                        i_Point.AxisXValue -= 1;
                        break;
                    }

                case eDirections.DownRow:
                    {
                        i_Point.AxisXValue -= 1;
                        break;
                    }

                case eDirections.DownLeftDiagonal:
                    {
                        i_Point.AxisXValue -= 1;
                        i_Point.AxisYValue += 1;
                        break;
                    }

                case eDirections.LeftColoumn:
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
                        if (m_Board.CellBoard[i, j].SignValue == (char)eGameSigns.O)
                        {
                            secondPlayerScore++;
                        }

                        if (m_Board.CellBoard[i, j].SignValue == (char)eGameSigns.X)
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

        private void OnUpdateCellSignValue(char i_PlayerWasher, Point i_Point)
        {
            if (this.UpdatingSignValue != null)
            {
                this.UpdatingSignValue(i_PlayerWasher, i_Point);
            }
        }

        private void OnUpdateCellOption(Point i_Point)
        {
            if (this.UpdatingLegalityCellOption != null)
            {
                this.UpdatingLegalityCellOption(i_Point);
            }
        }
    }
}