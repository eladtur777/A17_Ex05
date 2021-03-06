﻿using System.Collections.Generic;
using System.Text;

namespace Ex02_Othelo
{
    public class GameModel
    {
        private Player m_FirstPlayer = null;
        private Player m_SecondPlayer = null;
        private Board m_Board = null;
        private GameRules m_GameRules = null;
        private StringBuilder m_BoardGame = new StringBuilder();

        public event DelegateContainer.UpdatePictureBoxCellDelegate<char, Point> SignValueTrnsferingFromLogicToUI;

        public event DelegateContainer.UpdateLegalPictureBoxCellDelegate<Point> LegalCellCoordinatesTransferingFromLogicToUI;

        public GameModel(int i_BoardSize, string i_FirstPlayerName, string i_SecondPlayerName)
        {
            m_FirstPlayer = new Player(i_FirstPlayerName);
            m_SecondPlayer = new Player(i_SecondPlayerName);
            m_Board = new Board(i_BoardSize);
            m_GameRules = new GameRules(ref m_Board);
            m_GameRules.UpdatingSignValueFromLogicToUI += listenerForUpdateSignValue_updatingSignValueFromLogicToUI;
            m_GameRules.UpdatingLegalCellFromLogicToUI += listenerForLegalityCellOption_updatingLegalCellFromLogicToUI;
        }

        public bool ThereIsExisitingLegalMove(Player i_player)
        {
            return m_GameRules.ThereIsExisitingLegalMove(i_player);
        }

        public bool LegalMove(Player i_player)
        {
            bool moveResult = m_GameRules.LegalMove(i_player);
            List<int> scores = m_GameRules.GameScore;
            m_FirstPlayer.PlayerScore = scores[0];
            m_SecondPlayer.PlayerScore = scores[1];
            return moveResult;
        }

        public bool LegalMove(Player i_player, Point i_point, eOnlyCheck i_mode)
        {
            bool moveResult = m_GameRules.LegalMove(i_player, i_point, i_mode);
            List<int> scores = m_GameRules.GameScore;
            m_FirstPlayer.PlayerScore = scores[0];
            m_SecondPlayer.PlayerScore = scores[1];
            return moveResult;
        }

        public Board Board
        {
            get
            {
                return m_Board;
            }
        }

        public Player FirstPlayer
        {
            get
            {
                return m_FirstPlayer;
            }
        }

        public Player SecondPlayer
        {
            get
            {
                return m_SecondPlayer;
            }
        }

        public int GetFirstPlayersScore
        {
            get
            {
                return m_FirstPlayer.PlayerScore;
            }
        }

        public int GetSecondPlayersScore
        {
            get
            {
                return m_SecondPlayer.PlayerScore;
            }
        }

        private void listenerForUpdateSignValue_updatingSignValueFromLogicToUI(char i_PlayerWasher, Point i_Point)
        {
            onTransferingSignValueUpdate(i_PlayerWasher, i_Point);
        }

        private void listenerForLegalityCellOption_updatingLegalCellFromLogicToUI(Point i_Point)
        {
            onTrnsferingLegalityCellOption(i_Point);
        }

        private void onTransferingSignValueUpdate(char i_PlayerWasher, Point i_Point)
        {
            if (this.SignValueTrnsferingFromLogicToUI != null)
            {
                this.SignValueTrnsferingFromLogicToUI(i_PlayerWasher, i_Point);
            }
        }

        private void onTrnsferingLegalityCellOption(Point i_Point)
        {
            if (this.LegalCellCoordinatesTransferingFromLogicToUI != null)
            {
                this.LegalCellCoordinatesTransferingFromLogicToUI(i_Point);
            }
        }
    }
}