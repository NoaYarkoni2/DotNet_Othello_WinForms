using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Board
    {
        private const string k_Empty = " ";
        private const string k_Black = "X";
        private const string k_White = "O";
        private int m_BoardSize;

        private readonly string[,] r_Board;
        public Board(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            r_Board = new string[i_BoardSize, i_BoardSize];

            for (int row = 0; row < i_BoardSize; row++)
            {
                for (int col = 0; col < i_BoardSize; col++)
                {
                    r_Board[row, col] = k_Empty;
                }
            }

            r_Board[((i_BoardSize / 2) - 1), ((i_BoardSize / 2) - 1)] = k_White;
            r_Board[((i_BoardSize / 2) - 1), (i_BoardSize / 2)] = k_Black;
            r_Board[(i_BoardSize / 2), ((i_BoardSize / 2) - 1)] = k_Black;
            r_Board[(i_BoardSize / 2), (i_BoardSize / 2)] = k_White;
        }
        public void PrintBoard()
        {
            Console.WriteLine();

            Console.Write("    ");
            for (int index = 0; index < r_Board.GetLength(0); index++)
            {
                Console.Write(Convert.ToChar('A' + index));
                Console.Write("   ");
            }
            Console.WriteLine();
            Console.WriteLine("  " + new string('=', r_Board.GetLength(0) * 4) + "=");

            for (int row = 0; row < r_Board.GetLength(0); row++)
            {
                Console.Write(row + 1);
                Console.Write(" |");
                for (int col = 0; col < r_Board.GetLength(0); col++)
                {
                    Console.Write(" ");
                    Console.Write(r_Board[row, col]);
                    Console.Write(" |");
                }
                Console.WriteLine();
                Console.WriteLine("  " + new string('=', r_Board.GetLength(0) * 4) + "=");
            }
            Console.WriteLine();
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
            set { m_BoardSize = value; }
        }

        public string[,] MBoard
        {
            get { return r_Board; }
        }

       
    }
}
