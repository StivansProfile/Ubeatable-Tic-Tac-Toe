using ConsoleApp1;

class HumanPlayer
{
    GameBoard board = new GameBoard();
    public void MakeAMove()
    {
        Console.WriteLine("Enter a x coordinate: ");
        int x = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter an y coordinate: ");
        int y = Convert.ToInt32(Console.ReadLine());

        // update board
        board.initialBoardState[x,y] = 1;
        board.PrintBoard();
    }
}

class AiPlayer
{
    GameBoard board = new GameBoard();

    public void MakeAMove()
    {
        // use the mini-max here
        MiniMax miniMax = new MiniMax(board.initialBoardState, 2);
        miniMax.FindBestMove();

    }
}



class GameBoard
{
    public int[,] initialBoardState =
    {
        { 1, 0, 0 },
        { 2, 1, 2 },
        { 2, 0, 0 }
    }; 

    // prints 2d array from above
    public void PrintBoard()
    {
        for(int i = 0; i < initialBoardState.GetLength(0); i++)
        {
            for(int j = 0; j < initialBoardState.GetLength(1); j++)
            {
                Console.Write(initialBoardState[i, j]);
            }
            Console.WriteLine();
        }
    }
}


namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {   
            // print the initial board
            GameBoard board = new GameBoard();
            board.PrintBoard();
               
            // lets the human player go first
            HumanPlayer player = new HumanPlayer();
            player.MakeAMove();

            // let the AI go next
            AiPlayer aiPlayer = new AiPlayer();
            aiPlayer.MakeAMove();

            // execute game loop here
        }
    }
}
