using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class Game
    {
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private readonly Board r_Board;
        private int m_TurnCounter;
        public enum eGameModes
        {
            ComputerGameMode = 1,
            HumanGameMode
        }
        public Game(int i_BoardSize)
        {
            r_Player1 = new Player("Black", "X", true);
            r_Player2 = new Player("White", "O", true);
            r_Board = new Board(i_BoardSize);
        }

        public void InitGame(int i_BoardSize, int i_UsersChoiceOfGameMode)
        {
            m_TurnCounter = 0;
            if (i_UsersChoiceOfGameMode == (int)eGameModes.ComputerGameMode)
            {
                r_Player2.IsHuman = false;
            }
        }
        public int GetSumOfPlayerPoints(int i_BoardSize, Player i_Player, string[,] i_Board)
        {
            int totalOfPlayerPoints = 0;

            for (int row = 0; row < i_BoardSize; row++)
            {
                for (int column = 0; column < i_BoardSize; column++)
                {
                    if (i_Board[row, column] == i_Player.Sign)
                    {
                        totalOfPlayerPoints += 1;
                    }
                }
            }
            return totalOfPlayerPoints;
        }

        public bool IsGameOver(int i_Size, string[,] i_Board, Player i_CurrentPlayer, Player i_OpponentPlayer)
        {
            List<Tuple<int, int>> currentPlayerMoves = GetPossibleMoves(i_CurrentPlayer, i_Size, i_Board);
            bool result = true;
            if (currentPlayerMoves.Count > 0)
            {
                result = false;
            }

            List<Tuple<int, int>> opponentPlayerMoves = GetPossibleMoves(i_OpponentPlayer, i_Size, i_Board);
            if (opponentPlayerMoves.Count > 0)
            {
                result = false;
            }

            return result;
        }

        public int ComputerMoves(List<Tuple<int, int>> i_ListOfPossibleMove)
        {
            int moveIndex = 0;
            if (i_ListOfPossibleMove.Count == 0)
            {
             //   Console.WriteLine("The computer has no legal moves. Skipping turn.");
             //להחליף תור
                moveIndex = -1;
            }
            else
            {
                Random rnd = new Random();
                moveIndex = rnd.Next(i_ListOfPossibleMove.Count);
            }
            return moveIndex;
        }

        public List<Tuple<int, int>> GetPossibleMoves(Player i_CurrentPlayer, int i_Size, string[,] i_Board)
        {

            List<Tuple<int, int>> possibleMoves = new List<Tuple<int, int>>();

            for (int currentRow = 0; currentRow < i_Size; currentRow++)
            {
                for (int currentColumn = 0; currentColumn < i_Size; currentColumn++)
                {
                    if (i_Board[currentRow, currentColumn] == " ")
                    {
                        if (isValidMove(currentRow, currentColumn, i_CurrentPlayer, i_Size, i_Board))
                        {
                            possibleMoves.Add(new Tuple<int, int>(currentRow, currentColumn));
                        }
                    }
                }
            }
            return possibleMoves;
        }

        public bool isValidMove(int i_Row, int i_Column, Player i_CurrentPlayer, int i_Size, string[,] i_Board)
        {
            bool validMove = true;
            if (i_Row < 0 || i_Row >= i_Size || i_Column < 0 || i_Column >= i_Size)
            {
                validMove = false;
            }

            if (i_Board[i_Row, i_Column] != " ")
            {
                validMove = false;
            }

            if (i_Board[i_Row, i_Column] == i_CurrentPlayer.Sign)
            {
                validMove = false;
            }

            if (!IsCapturesOpponent(i_Board, i_CurrentPlayer.Sign, i_Row, i_Column, i_Size))
            {
                validMove = false;
            }

            return validMove;
        }

        public bool IsCapturesOpponent(string[,] i_Board, string i_CurrentPlayerSign, int i_Row, int i_Column, int i_Size)
        {
            bool capture = true;
            int[] rowDirectionOptions = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] columnDirectionOptions = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int index = 0; index < 8; index++)
            {
                int currentRow = i_Row + rowDirectionOptions[index];
                int currentCol = i_Column + columnDirectionOptions[index];

                if (IsDiractionValid(currentRow, currentCol, i_Size) && i_Board[currentRow, currentCol] != " " && i_Board[currentRow, currentCol] != i_CurrentPlayerSign)
                {
                    while (IsDiractionValid(currentRow, currentCol, i_Size) && i_Board[currentRow, currentCol] != " " && i_Board[currentRow, currentCol] != i_CurrentPlayerSign)
                    {
                        currentRow += rowDirectionOptions[index];
                        currentCol += columnDirectionOptions[index];
                    }

                    if (IsDiractionValid(currentRow, currentCol, i_Size) && i_Board[currentRow, currentCol] == i_CurrentPlayerSign)
                    {
                        return capture;
                    }
                }
            }

            return !capture;
        }

        public bool IsDiractionValid(int i_CurrentRow, int i_CurrentCol, int size)
        {
            bool isValid = true;
            if (i_CurrentRow >= 0 && i_CurrentRow < size && i_CurrentCol >= 0 && i_CurrentCol < size)
            {
                return isValid;
            }
            return !isValid;
        }

        public void FlipPieces(string[,] i_Board, int i_Size, int i_Row, int i_Col, Player i_CurrentPlayer)
        {
            string enemySign;
            if (i_CurrentPlayer.Sign == "X")
            {
                enemySign = "O";
            }
            else
            {
                enemySign = "X";
            }
            for (int rowDirection = -1; rowDirection <= 1; rowDirection++)
            {
                for (int colDirection = -1; colDirection <= 1; colDirection++)
                {
                    if (rowDirection == 0 && colDirection == 0)
                    {
                        continue;
                    }
                    int row = i_Row + rowDirection;
                    int col = i_Col + colDirection;
                    if (row < 0 || row >= i_Board.GetLength(0) || col < 0 || col >= i_Board.GetLength(1))
                    {
                        continue;
                    }
                    if (i_Board[row, col] != enemySign)
                    {
                        continue;
                    }
                    while (true)
                    {
                        row += rowDirection;
                        col += colDirection;
                        if (row < 0 || row >= i_Board.GetLength(0) || col < 0 || col >= i_Board.GetLength(1))
                        {
                            break;
                        }
                        if (i_Board[row, col] == i_CurrentPlayer.Sign)
                        {
                            row -= rowDirection;
                            col -= colDirection;
                            while (row != i_Row || col != i_Col)
                            {
                                i_Board[row, col] = i_CurrentPlayer.Sign;
                                row -= rowDirection;
                                col -= colDirection;
                            }
                            break;
                        }
                    }
                }
            }
        }
        public Board Board
        {
            get { return r_Board; }         
        }

        public Player Player1
        {
            get { return r_Player1; }
        }

        public Player Player2
        {
            get { return r_Player2; }
        }
    }
}
