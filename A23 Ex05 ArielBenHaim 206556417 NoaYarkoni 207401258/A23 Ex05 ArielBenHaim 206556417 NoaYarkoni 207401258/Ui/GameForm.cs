using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Logic.Game;

namespace Ui
{
    public partial class GameForm : Form
    {
        private bool m_IsPlayerOneTurn;
        private bool m_IsGameOver;
        private Game m_Game;
        private readonly int r_UsersChoiceOfGameMode;
        private Button[,] m_ButtonsMatrix;
        private int m_BoardSize;
        private Player m_CurrentPlayer;
        private Tuple<int, int> m_Move;
        private static int m_TotalGamesCounter;
        private static int m_TotalGamesPlayer1Win=0;
        private static int m_TotalGamesPlayer2Win = 0;

        public GameForm(int i_BoardSize, int i_GameModes, Game i_Game)
        {
            InitializeComponent();
            m_Game = i_Game;
            m_CurrentPlayer = m_Game.Player1;
            m_BoardSize = i_BoardSize;
            m_IsGameOver = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            r_UsersChoiceOfGameMode = i_GameModes;
            m_IsPlayerOneTurn = true;
            m_ButtonsMatrix = new Button[i_BoardSize, i_BoardSize];
            BuildButtonsMatrix(i_BoardSize, m_ButtonsMatrix);
            InitializeBoardForm(i_BoardSize);
            PrintBoard(m_ButtonsMatrix);
            m_TotalGamesCounter = 1;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        public void BuildButtonsMatrix(int boardSize, Button[,] buttonsMatrix)
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;
            int margin = 20;
            int buttonWidth = (formWidth - margin * 2) / boardSize;
            int buttonHeight = (formHeight - margin * 2) / boardSize;

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    Button button = new Button();
                    button.Size = new Size(buttonWidth, buttonHeight);
                    button.Location = new Point(margin + row * buttonWidth, margin + col * buttonHeight);
                    button.Text = m_Game.Board.MBoard[row, col];
                    buttonsMatrix[row, col] = button;
                }
            }
        }
        public void SetButtonAppearance()
        {
            for (int row = 0; row < m_BoardSize; row++)
            {
                for (int col = 0; col < m_BoardSize; col++)
                {
                    Button button = m_ButtonsMatrix[row, col];
                    if (button.Text == "X")
                    {
                        button.BackColor = Color.Black;
                        button.ForeColor = Color.White;
                        button.Enabled = false;
                    }
                    else if (button.Text == "O")
                    {
                        button.BackColor = Color.White;
                        button.ForeColor = Color.Black;
                        button.Enabled = false;
                    }
                }
            }
        }
        private void ColorLegalSquaresGreen()
        {
            List<Tuple<int, int>> legalSquares = m_Game.GetPossibleMoves(m_CurrentPlayer, m_BoardSize, m_Game.Board.MBoard);
            foreach (Tuple<int, int> legalSquare in legalSquares)
            {
                m_ButtonsMatrix[legalSquare.Item1, legalSquare.Item2].BackColor = Color.Green;
                m_ButtonsMatrix[legalSquare.Item1, legalSquare.Item2].Enabled = true;
            }
        }
        private void UpdateFormTitle()
        {
            if (m_IsPlayerOneTurn)
            {
                this.Text = "Othello - Black's Turn";
            }
            else
            {
                this.Text = "Othello - White's Turn";
            }
        }
        private void UpdateNextPlayer()
        {
            if (this.m_CurrentPlayer == m_Game.Player1)
            {
                this.m_CurrentPlayer = m_Game.Player2;
                m_IsPlayerOneTurn = false;
            }
            else
            {
                this.m_CurrentPlayer = m_Game.Player1;
                m_IsPlayerOneTurn = true;
            }
            this.UpdateFormTitle();
        }
        private void MakeMoveOnBoard(int i_Row, int i_Column)
        {
            m_Game.FlipPieces(m_Game.Board.MBoard, m_BoardSize, i_Row, i_Column, m_CurrentPlayer);
            m_Game.Board.MBoard[i_Row, i_Column] = m_CurrentPlayer.Sign;
            this.UpdateNextPlayer();
            this.UpdateMatrix();
            if (r_UsersChoiceOfGameMode == 1 && m_CurrentPlayer==m_Game.Player2)
            {
                List<Tuple<int, int>> listOfPossibleComputerMove = m_Game.GetPossibleMoves(m_CurrentPlayer, m_BoardSize, m_Game.Board.MBoard);
                if (listOfPossibleComputerMove.Count > 0)
                {
                    m_Move = listOfPossibleComputerMove[m_Game.ComputerMoves(listOfPossibleComputerMove)];
                    MakeMoveOnBoard(m_Move.Item1, m_Move.Item2);
                }
                else
                {
                    this.UpdateNextPlayer();
                }
            }
            List<Tuple<int, int>> currentPlayerlistNextOfPossibleMove = m_Game.GetPossibleMoves(m_CurrentPlayer, m_BoardSize, m_Game.Board.MBoard);
            if (currentPlayerlistNextOfPossibleMove.Count == 0)
            {
                this.UpdateNextPlayer();
                currentPlayerlistNextOfPossibleMove = m_Game.GetPossibleMoves(m_CurrentPlayer, m_BoardSize, m_Game.Board.MBoard);
                if (currentPlayerlistNextOfPossibleMove.Count == 0)
                {
                    m_IsGameOver = true;
                    if (m_IsGameOver)
                    {
                        EndRound();
                    }
                }
            }
            this.UpdateMatrix();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int buttonHeight = clickedButton.Height;
            int buttonWidth = clickedButton.Width;
            int colIndex = clickedButton.Location.Y / buttonHeight;
            int rowIndex = clickedButton.Location.X / buttonWidth;
            MakeMoveOnBoard(rowIndex, colIndex);
        }
        private void DisableEmptyButtons()
        {
            for (int row = 0; row < m_BoardSize; row++)
            {
                for (int col = 0; col < m_BoardSize; col++)
                {
                    if (m_Game.Board.MBoard[row, col] == " ")
                    {
                        m_ButtonsMatrix[row, col].Enabled = false;
                        m_ButtonsMatrix[row, col].BackColor = Color.LightSlateGray;
                    }
                }
            }
        }
        private void UpdateMatrix()
        {
            for (int row = 0; row < m_BoardSize; row++)
            {
                for (int col = 0; col < m_BoardSize; col++)
                {
                    m_ButtonsMatrix[row, col].Text = m_Game.Board.MBoard[row, col];
                    if (m_ButtonsMatrix[row, col].Text == "X")
                    {
                        m_ButtonsMatrix[row, col].BackColor = Color.Black;
                        m_ButtonsMatrix[row, col].ForeColor = Color.White;
                    }
                    else if (m_ButtonsMatrix[row, col].Text == "O")
                    {
                        m_ButtonsMatrix[row, col].BackColor = Color.White;
                        m_ButtonsMatrix[row, col].ForeColor = Color.Black;
                    }
                    else
                    {
                        m_ButtonsMatrix[row, col].BackColor = Color.LightSlateGray;
                    }
                }
            }
            PrintBoard(m_ButtonsMatrix);
        }
        private void InitializeBoardForm(int i_BoardSize)
         {
            this.UpdateFormTitle();
            for (int row = 0; row < m_BoardSize; row++)
            {
                for (int col = 0; col < m_BoardSize; col++)
                {
                    m_ButtonsMatrix[row, col].Click += button_Click;
                    Controls.Add(m_ButtonsMatrix[row, col]);
                }
            }
         }
        private void PrintBoard(Button[,] i_ButtonsMatrix)
        {            
            SetButtonAppearance();
            DisableEmptyButtons();
            ColorLegalSquaresGreen();
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    Label label = new Label();
                    label.Text = i_ButtonsMatrix[i, j].Text;
                    label.Left = (j * this.ClientSize.Width) + 20;
                    label.Top = (i * this.ClientSize.Width) + 20;
                    label.Size = new Size(this.ClientSize.Width, this.ClientSize.Width);
                    label.TextAlign = ContentAlignment.MiddleCenter;
                    this.Controls.Add(label);
                }
            }
        }
        private void RestartGame()
        {
            m_TotalGamesCounter++;
            m_Game = new Game(this.m_BoardSize);
            m_CurrentPlayer = m_Game.Player1;        
            m_IsPlayerOneTurn = true;
            UpdateFormTitle();
            UpdateMatrix();
        }

        public void UpdateEndGameRoundResult(int i_Player1Score, int i_Player2Score)
        {
            if (i_Player2Score > i_Player1Score)
            {
                m_TotalGamesPlayer2Win++;
            }
            else if (i_Player2Score < i_Player1Score)
            {
                m_TotalGamesPlayer1Win++;
            }
        }
        private void EndRound()
        {
            DialogResult result = DialogResult.None;
            int player1score = m_Game.GetSumOfPlayerPoints(m_BoardSize, m_Game.Player1, m_Game.Board.MBoard);
            int player2score = m_Game.GetSumOfPlayerPoints(m_BoardSize, m_Game.Player2, m_Game.Board.MBoard);
            UpdateEndGameRoundResult(player1score, player2score);

            if (player1score > player2score)
            {
                result = MessageBox.Show(m_Game.Player1.Name + " Won!! (" + player1score + "/" + player2score + ") (" + m_TotalGamesPlayer1Win + "/" + m_TotalGamesCounter + ")" + "\nWould you like another round?", "Game Over", MessageBoxButtons.YesNo);
            }
            else if (player1score < player2score)
            {
                result = MessageBox.Show(m_Game.Player2.Name + " Won!! (" + player2score + "/" + player1score + ") (" + m_TotalGamesPlayer2Win + "/" + m_TotalGamesCounter + ")" + "\nWould you like another round?", "Game Over", MessageBoxButtons.YesNo);
            }
            else if (player1score == player2score)
            {
                result = MessageBox.Show(" It's a tie!! (" + player1score + "/" + player2score + ")" + "\nWould you like another round?", "Game Over", MessageBoxButtons.YesNo);
            }

            if (result == DialogResult.Yes)
            {               
                RestartGame();
            }
            else if (result == DialogResult.No)
            {
                this.Close();
            }
        }
    }
}
 