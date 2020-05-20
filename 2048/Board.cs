using System;
using System.Windows.Controls;

namespace _2048
{
    class Board
    {
        private Cell[,]cells;

        private int score;
        private Random random;
        private Canvas canvas;

        public int getScore
        {
            get { return score; }
        }
        /**
        * Constructor for creating a new Board
        * sets all Cells to BlankCells
        */
        public Board(Canvas c){
		    this.score = 0;
		    this.cells = new Cell[4,4];
            this.random = new Random();
            this.canvas = c;

		    for(int i=0; i < cells.GetLength(0); i++)
			    for(int j=0; j < cells.GetLength(1); j++)
				    cells[i,j] = new BlankCell();
	    }
    
        /**
        * method for rotation the board right
        * @param number of times to rotate
        */
        public void RotateRight(int x)
        {
            while (x > 0)
            {
                for (int i = 0; i < (cells.GetLength(0) / 2); i++)
                {
                    for (int j = i; j < cells.GetLength(0) - 1 - i; j++)
                    {
                        Cell nextCache = cells[j,(cells.GetLength(0) - 1) - i];
                        Cell actualCache = cells[i,j];
                        for (int k = 4; k > 0; k--)
                        {
                            cells[j,(cells.GetLength(0) - 1) - i] = actualCache;
                            actualCache = nextCache;
                            int cacheCoord = i;
                            i = j;
                            j = (cells.GetLength(1) - 1) - cacheCoord;

                            nextCache = cells[j,(cells.GetLength(0) - 1) - i];
                        }
                    }
                }
                x--;
            }
        }
        
        /**
	    * moves 1 single row
	    * 
	    *move logic:
	    *search for a not blank cell (j)
	    *find the next blocked Cell (k)
	    *set j to k+1 and update shift counter
	    * @param coordinate of the row
	    * @return true if something has changed
	    */
        public bool MoveRow(int i)
        {
            bool changed = false;

            for (int j = 1; j < cells.GetLength(1); j++)
            {
                if (!cells[i,j].IsBlank())
                {
                    for (int k = j - 1; k >= -1; k--)
                    {

                        if (k == j - 1 && !cells[i,k].IsBlank())
                        {
                            break;
                        }
                        if ((k == -1) || (!cells[i,k].IsBlank()))
                        {
                            cells[i,k + 1] = cells[i,j];

                            score += cells[i,j].DeleteCell(canvas);
                            cells[i,j] = new BlankCell();

                            changed = true;
                            break;
                        }
                    }
                }

            }
            return changed;
        }

        /**
	    * merges 1 single row
	    * @param coordinate of the row
	    * @return true if something has changed
	    */
        public bool MergeRow(int i)
        {
            bool changed = false;

            for (int j = 0; j + 1 < cells.GetLength(1); j += 1)
            {
                if (cells[i,j].Merge(cells[i,j + 1].Moveable()) == 1)
                {
                    score += cells[i,j].value;
                    changed = true;

                    score += cells[i,j + 1].DeleteCell(canvas);

                    cells[i,j + 1] = new BlankCell();
                }
            }
            return changed;
        }

        /**
	    * shift 1 single row	
	    * @param coordinate of the row
	    * @return true if something has changed
	    */
        public bool ShiftRow(int i)
        {
            bool changed = false;

            changed = MoveRow(i) || changed;
            changed = MergeRow(i) || changed;
            changed = MoveRow(i) || changed;

            return changed;
        }

         /**
        * shifts the whole board	*
        * @return true if something has changed
        */
        public bool ShiftLeft()
        {
            bool changed = false;

            for (int i = 0; i < cells.GetLength(0); i++)
            {
                changed = ShiftRow(i) || changed;
            }
            return changed;
        }
        public bool ShiftDown()
        {
            bool changed; RotateRight(1); changed = ShiftLeft(); RotateRight(3);
            return changed;
        }
        public bool ShiftRight()
        {
            bool changed; RotateRight(2); changed = ShiftLeft(); RotateRight(2);
            return changed;
        }
        public bool ShiftUp()
        {
            bool changed; RotateRight(3); changed = ShiftLeft(); RotateRight(1);
            return changed;
        }

        /**
	    * creates new Cells and puts them on the board
	    * @return false if there is no space left
	    */
        public bool CreateCell()
        {
            int twoOrFour;
            if (random.Next(100) <= 8)
                twoOrFour = 4;
            else
                twoOrFour = 2;

            return CreateCell(twoOrFour);
        }

        public bool CreateCell(int value)
        {

            int count = CountFreeCells();
            //no space left
            if (count == 0)
            {
                return false;
            }
            int newCell = random.Next(count); 


            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    if (cells[i,j].IsBlank())
                    {
                        
                        if (newCell == 0)
                        {
                            cells[i, j].DeleteCell(canvas);
                            cells[i,j] = new NormalCell(value);
                            return true;
                        }
                        newCell--;
                    }
                }
            }
            return true;
        }

        public int CountFreeCells()
        {
            int count = 0;
            for (int i = 0; i < cells.GetLength(0); i++)
                for (int j = 0; j < cells.GetLength(1); j++)
                    if (cells[i,j].IsBlank())
                        count++;
            return count;
        }

        public bool LoseCondition()
        {
            if (CountFreeCells() == 0)
            {
                //horizontal
                for (int i = 0; i < cells.GetLength(0); i++)
                    for (int j = 0; j < cells.GetLength(1) - 1; j++)
                        if (cells[i,j].value == cells[i,j + 1].value)
                            return false;
                //vertical
                for (int i = 0; i < cells.GetLength(0) - 1; i++)
                    for (int j = 0; j < cells.GetLength(1); j++)
                        if (cells[i,j].value == cells[i + 1,j].value)
                            return false;
                return true;
            }
            return false;
        }
        //For Debugging purpuses
        public void SpawnAllColours()
        {
            int newCell = 2;

            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    cells[i, j] = new NormalCell(newCell);
                    newCell=newCell << 1;
                }
            }
        }
        public void Draw()
        {
           canvas.Children.Clear();
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    cells[i, j].Draw(canvas, i, j);
                }
            }
        }
    }
}
