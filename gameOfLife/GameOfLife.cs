using System;
using System.Collections.Generic;

/// <summary>
/// Instructions for use:
/// Instantiate a GameOfLife Object using Width, Height, and ChanceOfLife parameters
/// To continuously update, use the following structure:
/// bool game = true;
/// while(game)
///     game = GameOfLifeObject.UpdateGrid();
/// </summary>
namespace ConwaysGameOfLife
{
    /// <summary>
    /// The Game of Life object
    /// </summary>
    public class GameOfLife
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[,] Grid { get; set; }
        public byte ChanceOfLife { get; set; }



        public GameOfLife()
        { }

        /// <summary>
        /// Declares GameOfLife Object. ChanceOfLife must not be less than 0 or greater than 100
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="chanceOfLife"></param>
        public GameOfLife(int height, int width, byte chanceOfLife = 50)
        {
            Width = width;
            Height = height;
            if (chanceOfLife >= 0 && chanceOfLife <= 100)
                ChanceOfLife = chanceOfLife;
            else
                throw new InvalidChanceOfLifeException("ChanceOfLife cannot be less than 0 or greater than 100");
            InitGrid();
        }

        public void InitGrid()
        {
            if (Width <= 0 || Height <= 0)
            {
                throw new NegativeGridDimensionException("Grid dimensions must be positive.");
            }
            else
            {
                Grid = new byte[Height, Width];
            }
            try
            {
                byte temp = 0; // to store the temp random position
                // this will set every place on the grid to either a 1 or a 0
                Random rnd = new Random((int)DateTime.Now.Ticks);
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        // gets number between 1 and 100 (including 0 and 100)
                        temp = (byte)rnd.Next(0, 101);
                        Grid[i, j] = (byte)rnd.Next(2);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateGrid()
        {
            List<CellChange> cellChanges = new List<CellChange>();

            bool changed = false;
            if (Width <= 0 || Height <= 0)
            {
                throw new NegativeGridDimensionException("Grid dimensions must be positive.");
            }
            try
            {
                byte cellValue;
                byte numNeighbors;

                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        // next, count the neighbors
                        cellValue = Grid[i, j];
                        numNeighbors = countNeighbors(i, j);
                        if (cellValue == 0) // if dead/inactive
                        {
                            if (numNeighbors == 3)
                            {
                                // becomes alive/reproduced
                                // Grid[i, j] = 1;
                                cellChanges.Add(new CellChange() { X_Coor = j, Y_Coor = i, Value = 1 });
                                changed = true;
                            }
                        }
                        else
                        {
                            if (cellValue == 1) // if living
                            {
                                if (numNeighbors < 2)
                                {
                                    // dies of underpopulation
                                    // Grid[i, j] = 0;
                                    cellChanges.Add(new CellChange() { X_Coor = j, Y_Coor = i, Value = 0 });
                                    changed = true;
                                }
                                else
                                {
                                    if (numNeighbors > 3)
                                    {
                                        //dies of overpopulation
                                        // Grid[i, j] = 0;
                                        cellChanges.Add(new CellChange() { X_Coor = j, Y_Coor = i, Value = 0 });
                                        changed = true;
                                    }
                                    else
                                    {
                                        // lives on

                                        changed = true;
                                    }
                                }
                            }
                            else
                            {
                                throw new InvalidGridStateException("Invalid Grid State '" + Grid[i, j] + "': must be either 1 or 0");
                            }
                        }
                    }
                }
                //finally, commit changes at end
                commitChanges(cellChanges);
            }
            catch (Exception)
            {
                throw;
            }

            return changed;
        }

        private void commitChanges(List<CellChange> changes)
        {
            foreach(CellChange change in changes)
            {
                Grid[change.Y_Coor, change.X_Coor] = change.Value;
            }
        }

        /// <summary>
        /// Takes coordinates i, j on the grid, and counts how many neighbors it has
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private byte countNeighbors(int first_index, int second_index)
        {
            byte neighbors = 0;

            // check the row above
            if(checkInRange(first_index-1, "y"))
            {
                // check top left
                if(checkInRange(second_index-1, "x"))
                {
                    if (Grid[first_index - 1, second_index - 1] == 1)
                        neighbors++;
                }
                // check top right
                if(checkInRange(second_index+1, "x"))
                {
                    if (Grid[first_index - 1, second_index + 1] == 1)
                        neighbors++;
                }
                // check top
                if (Grid[first_index - 1, second_index] == 1)
                    neighbors++;
            }
            // check left cell
            if(checkInRange(second_index-1, "x"))
            {
                if (Grid[first_index, second_index - 1] == 1)
                    neighbors++;
            }
            // check right cell
            if(checkInRange(second_index+1, "x"))
            {
                if (Grid[first_index, second_index + 1] == 1)
                    neighbors++;
            }

            // check the row below
            if(checkInRange(first_index+1, "y"))
            {
                // check bottom left
                if(checkInRange(second_index-1, "x"))
                {
                    if (Grid[first_index + 1, second_index - 1] == 1)
                        neighbors++;
                }
                // check bottom right
                if(checkInRange(second_index+1, "x"))
                {
                    if (Grid[first_index + 1, second_index + 1] == 1)
                        neighbors++;
                }
                // check bottom
                if (Grid[first_index + 1, second_index] == 1)
                    neighbors++;
            }
            

            return neighbors ;
        }
        private bool checkInRange(int index, string coor)
        {
            bool value = false;
            switch(coor)
            {
                case "x":
                    if (index >= 0 && index < Width)
                        value = true;
                    break;
                case "y":
                    if (index >= 0 && index < Height)
                        value = true;
                    break;
            }
            return value;
        }
    }
    public struct CellChange
    {
        public int X_Coor { get; set; }
        public int Y_Coor { get; set; }
        public byte Value { get; set; }

    }
    [Serializable]
    public class NegativeGridDimensionException : Exception
    {
        public NegativeGridDimensionException()
        {}
        public NegativeGridDimensionException(string message)
            :base(message)
        {}
        public NegativeGridDimensionException(string message, Exception innerException)
            :base(message,innerException)
        {}
    }
    [Serializable]
    public class InvalidChanceOfLifeException : Exception
    {
        public InvalidChanceOfLifeException()
        { }
        public InvalidChanceOfLifeException(string message)
            :base(message)
        { }
        public InvalidChanceOfLifeException(string message, Exception innerException)
            :base(message,innerException)
        { }
    }
    [Serializable]
    public class InvalidGridStateException : Exception
    {
        public InvalidGridStateException()
        { }
        public InvalidGridStateException(string message)
            : base(message)
        { }
        public InvalidGridStateException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
