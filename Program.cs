using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeGame
{
    /// <summary>
    /// This Tic Tac Toe game has no intelligence - all it does is placing an oponent mark directly adjacent
    /// to the player's mark to hopefully prevent the player from placing markers in a row
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            TicTacToe t = new TicTacToe();
            t.DisplayTheBoard();
            string inputPos;
            while (!t.GameOver)
            {
                Console.Write("Enter your position (X) To Exit:");
                inputPos = Console.ReadLine();

                if (inputPos.ToLower() == "x")
                {
                    t.GameOver = true;
                    break;
                }
                if (!t.PutMark(inputPos, 'X'))
                    Console.WriteLine(t.Message);
                else
                {
                    t.DisplayTheBoard();
                }

            }
        }
    }

    public delegate bool? CheckSurrounding(byte i, byte j);

    public class TicTacToe
    {
        private char[,] Board;
        private string[] ValidRows;
        private Dictionary<string, byte> RowNumbers;
        private string[] ValidCols;
        public bool GameOver;
        public string Message;

        public TicTacToe()
        {
            this.Board = new char[3, 3];
            this.ValidRows = new string[3] { "a", "b", "c" };
            this.ValidCols = new string[3] { "1", "2", "3" };
            this.RowNumbers = new Dictionary<string,byte>();
            this.RowNumbers.Add("a", 1);
            this.RowNumbers.Add("b", 2);
            this.RowNumbers.Add("c", 3);
            this.GameOver = false;
        }

        public bool PutMark(string mark, char marker)
        {
            this.Message = "";
            if (mark.Length != 2)
            {
                this.Message = "Invalid input - 2 haracter, a letter and as number";
                return false;
            }
            if (!this.ValidRows.Contains(mark.Substring(0,1)) | !this.ValidCols.Contains(mark.Substring(1,1))) {
                this.Message = "Invalid input - valid inputs are a1, a2, a3, b1, ..., c3";
                return false;
            }

            byte col = this.RowNumbers[mark.Substring(0,1)];
            byte row = byte.Parse(mark.Substring(1, 1));
            byte i = (byte)(col - 1);
            byte j = (byte)(row - 1);


            if (this.Board[i, j] == 0 )
            {
                this.Board[i, j] = marker;
                CheckSurrounding check;
                if (row < 3)
                {
                    check = new CheckSurrounding(this.CheckRight);
                    if (check(i, j) == null)
                    {
                        return true;
                    }
                }
                else if (col == 3)
                {
                    check = new CheckSurrounding(this.CheckLeft);
                    if (check(i, j) == null)
                    {
                        return true;
                    }
                }
                else
                {
                    check = new CheckSurrounding(this.CheckRight);
                    if (check(i, j) == null)
                    {
                        return true;
                    }
                    check = this.CheckLeft;
                    if (check(i, j) == null)
                    {
                        return true;
                    }
                }

                if (row < 3)
                {
                    check = this.CheckBelow;
                    if (check(i, j) == null)
                    {
                        return true;
                    }
                }
                if (row > 1)
                {
                    check = this.CheckAbove;
                    if (check(i, j) == null)
                    {
                        return true;
                    }
                }

                if (col == 2 & row == 2)
                {
                    check = this.CheckRightLower;
                    if (check(i, j) == null)
                    {
                        return true;
                    }
                    check = this.CheckRightUpper;
                    if (check(i, j) == null)
                    {
                        return true;
                    }
                    check = this.CheckLeftUpper;
                    if (check(i, j) == null)
                    {
                        return true;
                    }
                    check = this.CheckLeftLower;
                    if (check(i, j) == null)
                    {
                        return true;
                    }
                }
                else if (col < 3 & row < 3)
                {
                    check = this.CheckRightLower;
                    if (check(i, j) == null)
                    {
                        return true;
                    }
                }
                else if (col < 3 & row == 3)
                {
                    check = this.CheckRightUpper;
                    if (check(i, j) == null)
                    {
                        return true;
                    }
                }
                else if (col > 1 & row < 3)
                {
                    check = this.CheckLeftLower;
                    if (check(i, j) == null)
                    {
                        return true;
                    }
                }
                else if (col > 1 & row == 3)
                {
                    check = this.CheckLeftUpper;
                    if (check(i, j) == null)
                    {
                        return true;
                    }
                }

            }
            return true;
        }

        private void MachineResponse(byte i, byte j)
        {
            this.Board[i, j] = 'O';
        }

        private bool? CheckLeft(byte i, byte j)
        {
            if (this.Board[i - 1, j] == 'X')
                return true;
            else if (this.Board[i - 1, j] == 'O')
                return false;
            else
            {
                this.MachineResponse((byte)(i - 1), j);
                return null;
            }
        }

        private bool? CheckRight(byte i, byte j)
        {
            if (this.Board[i + 1, j] == 'X')
                return true;
            else if (this.Board[i + 1, j] == 'O')
                return false;
            else
            {
                this.MachineResponse((byte)(i + 1), j);
                return null;
            }
        }

        private bool? CheckAbove(byte i, byte j)
        {
            if (this.Board[i, j - 1] == 'X')
                return true;
            else if (this.Board[i, j - 1] == 'O')
                return false;
            else
            {
                this.MachineResponse(i, (byte)(j - 1));
                return null;
            }
        }

        private bool? CheckBelow(byte i, byte j)
        {
            if (this.Board[i, j + 1] == 'X')
                return true;
            else if (this.Board[i, j + 1] == 'O')
                return false;
            else
            {
                this.MachineResponse(i, (byte)(j + 1));
                return null;
            }
        }

        private bool? CheckLeftUpper(byte i, byte j)
        {
            if (this.Board[i - 1, j - 1] == 'X')
                return true;
            else if (this.Board[i - 1, j - 1] == 'O')
                return false;
            else
            {
                this.MachineResponse((byte)(i - 1), (byte)(j - 1));
                return null;
            }
        }

        private bool? CheckRightUpper(byte i, byte j)
        {
            if (this.Board[i + 1, j - 1] == 'X')
                return true;
            else if (this.Board[i + 1, j - 1] == 'O')
                return false;
            else
            {
                this.MachineResponse((byte)(i + 1), (byte)(j - 1));
                return null;
            }
        }

        private bool? CheckLeftLower(byte i, byte j)
        {
            if (this.Board[i - 1, j + 1] == 'X')
                return true;
            else if (this.Board[i - 1, j + 1] == 'O')
                return false;
            else
            {
                this.MachineResponse((byte)(i - 1), (byte)(j + 1));
                return null;
            }
        }

        private bool? CheckRightLower(byte i, byte j)
        {
            if (this.Board[i + 1, j + 1] == 'X')
                return true;
            else if (this.Board[i + 1, j + 1] == 'O')
                return false;
            else
            {
                this.MachineResponse((byte)(i + 1), (byte)(j + 1));
                return null;
            }
        }


        public void DisplayTheBoard()
        {
            Console.WriteLine("Tic Tac Toe Board:");
            Console.WriteLine("   1|2|3");
            for (byte i = 0; i < 3; i++)
            {
                Console.Write(ValidRows[i] + ": ");
                for (byte j = 0; j < 3; j++)
                {
                    Console.Write(this.Board[i, j]) ;
                    if (j < 2)
                        Console.Write("|");
                }
                if (i < 2)
                    Console.WriteLine("\r\n   -----");
            }
            Console.WriteLine();
        }

    }
}
