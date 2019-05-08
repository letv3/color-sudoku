using ColorSudoku_Dnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorSudoku_Dnet
{
    [Serializable]
    public class Field
    {
        public int RowCount { get; private set; }
        public int Score { get; private set; }
        private DateTime StartTime;
        private Tile[][] SolvedField;
        private Tile[][] PreparedField;
        public GameState State;
        private int NumberOfHints;


        public Field()
        {
            RowCount = 9;
            State = GameState.PLAYING;
            NumberOfHints = 5;
            StartTime = DateTime.Now;
            newGame();
        }


        private void newGame()
        {
            SolvedField = GenerateSolvedField(new Tile[RowCount][]);
            
            PreparedField = PrepareField(Copy(SolvedField),79 );

        }


        public int GetScore()
        {
            int score =(int)(( DateTime.Now - StartTime).TotalSeconds);
            return score;
        }

        public void AskForHelp(int row, int column)
        {
            if (NumberOfHints > 0)
            {
                if (PreparedField[row][column].Locked==false)
                {
                    Console.WriteLine("Suggested value on this place = " + SolvedField[row][column].Value);
                    NumberOfHints--;
                    Console.Write(NumberOfHints + " hints left");
                }
                else
                {
                    Console.WriteLine("Very funny, value was set by a program");
                    Console.WriteLine(NumberOfHints + " hints left");
                }
            }
            else
                Console.WriteLine("U used all hints");

        }

        public void UpdateTile(int Value, int Row, int Col)
        {
            if (State == GameState.PLAYING)
            {
                try
                {
                    if (Value >= 1 && Value <= 9 && PreparedField[Row][Col].Locked==false)
                    {
                        PreparedField[Row][Col].Value = Value;
                    }
                    else
                    {
                        Console.WriteLine("this bound is locked or u entered wrong value(must be 1<=value<=9)");
                    }
                }
                catch(IndexOutOfRangeException E)
                {
                    ////
                }
                if (FieldIsSolved())
                {
                    Score = GetScore();
                    State = GameState.SOLVED;
                }
            }
        }

        private bool FieldIsSolved()
        {
            int victory = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (IsNumberSuitable(PreparedField, i, j, PreparedField[i][j].Value) || PreparedField[i][j].Value == 0)
                    {
                        victory++;
                        break;
                    }
                }
            }
            return victory == 0;
        }

        public Tile[][] GenerateSolvedField(Tile[][] tiles)
        {
            var numbers = new List<int>();
            for (int i = 1; i <= 9; i++) { numbers.Add(i); }

            var ShuffledNumbers = numbers.OrderBy(a => Guid.NewGuid()).ToList();
           

            //Console.WriteLine(ShuffledNumbers);
         
            for(int row=0; row<9; row++)
            {
                tiles[row] = new Tile[9];
                
            }
           

            for (int i = 0; i < 9; i++)
            {
                tiles[0][i] = new Tile { Value = ShuffledNumbers[i] };

            }
            //this cycle move the first line by NPLACES cyclically
            // NPlaces depends on i(iterator)
            // each next line has different koaficient of moving
            for (int i = 1; i <= 8; i++)
            {
                int nplaces = i % 3 == 0 ? 1 : 3;
                tiles[i] = ShiftLine(tiles[i - 1], nplaces);
            }
            return tiles;
        }

        private Tile[] ShiftLine(Tile[] line, int nPlaces)
        {
            if (nPlaces < 0 || line == null) return null;

            Tile[] bufferTiles = new Tile[RowCount];

            for (int i = 0; i < nPlaces; i++)
            {
                bufferTiles[i] = new Tile
                {
                    Value = line[RowCount - nPlaces + i].Value
                };
            }

            for (int i = 0; i < RowCount - nPlaces; i++)
            {
                bufferTiles[nPlaces + i] = new Tile
                {
                    Value = line[i].Value
                };
            }
            return bufferTiles;
        }

        private Tile[][] PrepareField(Tile[][] field, int numberOfLockedPositions)
        {
            // this function sets 0 on randomly generated positions in fields
            //numberOfLockedPositions -represents how many tiles with numbers will left
            if (numberOfLockedPositions > 81 || numberOfLockedPositions < 1) return null;

            var Positions = new List<int>();
            for (int i = 0; i < 81; i++) Positions.Add(i);
            var ShuffledPositions = Positions.OrderBy(a => Guid.NewGuid()).ToList();
        
            int row, column;
            int numberOfFreePositions = 81 - numberOfLockedPositions;

            for (int i = 0; i < numberOfFreePositions; i++)
            {
                int newFreeTile = ShuffledPositions[i];
                column = newFreeTile % 9;
                row = newFreeTile / 9;

                field[row][column].Value = 0;
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (field[i][j].Value != 0)
                    {
                        field[i][j].Locked = true;
                    }
                }
            }
            return field;
        }


        private bool IsNumberSuitable(Tile[][] field, int row, int column, int nextTile)
        {
            if (IsPossibleY(field, column, nextTile) || IsPossibleX(field, row, nextTile) || isPossibleBox(field, column, row, nextTile))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Check for the same Tile values in row
        private bool IsPossibleX(Tile[][] field, int row, int nextTile)
        {
            for (int columnIndex = 0; columnIndex < RowCount; columnIndex++)
            {
                if (field[columnIndex][row].Value == nextTile)
                {
                    return false;
                }
            }
            return true;
        }

        //Check for the same possible values in column
        private bool IsPossibleY(Tile[][] field, int column, int nextTile)
        {
            for (int rowIndex = 0; rowIndex < RowCount; rowIndex++)
            {
                if (field[column][rowIndex].Value == nextTile)
                {
                    return false;
                }
            }
            return true;
        }

        //Check for the same possible value in 3x3 part
        private bool isPossibleBox(Tile[][] field, int columnIndex, int rowIndex, int nextTile)
        {

            int startPointCol = columnIndex < 3 ? 0 : columnIndex < 6 ? 3 : 6;
            int startPointRow = rowIndex < 3 ? 0 : rowIndex < 6 ? 3 : 6;

            for (int startIndexCOL = startPointCol; startIndexCOL < startPointCol + 3; startIndexCOL++)
            {
                for (int startIndexROW = startPointRow; startIndexROW < startPointRow + 3; startIndexROW++)
                {
                    if (field[startIndexCOL][startIndexROW].Value == nextTile)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        private Tile[][] Copy(Tile[][] game)
        {
            Tile[][] copy = new Tile[9][];//?

            for (int row = 0; row < 9; row++) copy[row] = new Tile[9];
           

            for (int y = 0; y < 9; y++)
            {
                for (int cols = 0; cols < 9; cols++)
                {
                    copy[y][cols] = new Tile();
                    copy[y][cols].Value = game[y][cols].Value;
                }
            }
            return copy;
        }



       public Tile GetTile(int row, int column)
        {
            return PreparedField[row][column];
        }




    }

}
