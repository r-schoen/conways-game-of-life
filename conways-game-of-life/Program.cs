using ConwaysGameOfLife;
using System;

namespace conways_game_of_life
{
    class Program
    {
        static void Main(string[] args)
        {
            GameOfLife game = new GameOfLife(20, 50);
            bool gameState = true;
            while(gameState)
            {
                PrintGrid(game);
                Console.WriteLine();
                gameState = game.UpdateGrid();
                System.Threading.Thread.Sleep(100);
            }
            Console.ReadKey();
        }
        static void PrintGrid(GameOfLife game)
        {
            for(int i = 0; i < game.Height; i++)
            {

                for(int j=0;j<game.Width;j++)
                {
                    switch(game.Grid[i,j])
                    {
                        case 0:
                            Console.Write(".");
                            break;
                        case 1:
                            Console.Write("#");
                            break;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
