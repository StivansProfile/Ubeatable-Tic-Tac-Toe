namespace ConsoleApp1
{
    public class MiniMax
    {
        private int[,] board;
        private int player;

        // Constructor
        public MiniMax(int[,] board, int player)
        {
            this.board = board;
            this.player = player;
        }

        /*
        Helper function that checks the winner rules
        if returned value is 1 human wins
        if its 2 AI won
        null if there is no winner
        */
        public int? CheckForWinner()
        {
            // Check rows for a winner
            for (int i = 0; i <= 2; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != 0)
                {
                    return board[i, 0];
                }
            }

            // Check columns for a winner
            for (int j = 0; j <= 2; j++)
            {
                if (board[0, j] == board[1, j] && board[1, j] == board[2, j] && board[0, j] != 0)
                {
                    return board[0, j];
                }
            }

            // Check diagonals for a winner
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != 0)
            {
                return board[0, 0];
            }

            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != 0)
            {
                return board[0, 2];
            }

            return null;
        }

        // Search for all possible moves
        // Return array of all empty spaces
        // a.k.a all zeros on board
        public List<(int, int)> AllPossibleMoves()
        {
            List<(int, int)> possibleMoves = new List<(int, int)>();

            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    if (board[i, j] == 0)
                    {
                        possibleMoves.Add((i, j));
                    }
                }
            }

            return possibleMoves;
        }

        // Make a move
        public void MakeMove((int, int) move, int player)
        {
            (int i, int j) = move;
            board[i, j] = player;
        }

        // Undo a move
        public void UndoMove((int, int) move)
        {
            (int i, int j) = move;
            board[i, j] = 0;
        }

        // Evaluation function
        // If 2 is returned, that means the AI is winning
        public int Evaluate()
        {
            int? winner = CheckForWinner();
            if (winner == 2)
            {
                return 1;
            }
            if (winner == 1)
            {
                return -1;
            }
            return 0;
        }

        // Actual minimax recursive function
        // Depth is how many empty spaces or possibilities it checks
        // Returns a score for the best move - ranks the best move
        public int Minimax(int depth, bool isAiTurn)
        {
            // Check for terminal state
            if (CheckForWinner() != null || depth == 0)
            {
                return Evaluate();
            }

            // If it's the AI's turn, we try to maximize
            if (isAiTurn)
            {
                int maxEval = int.MinValue;
                foreach (var move in AllPossibleMoves())
                {
                    MakeMove(move, 2);  // AI's move
                    int eval = Minimax(depth - 1, false);
                    UndoMove(move);
                    maxEval = Math.Max(maxEval, eval);
                }
                return maxEval;
            }
            // If it's the human player's turn, we try to minimize
            else
            {
                int minEval = int.MaxValue;
                foreach (var move in AllPossibleMoves())
                {
                    MakeMove(move, 1);  // Human's move
                    int eval = Minimax(depth - 1, true);
                    UndoMove(move);
                    minEval = Math.Min(minEval, eval);
                }
                return minEval;
            }
        }

        // Find the best move and return it
        public (int, int) FindBestMove()
        {
            int bestValue = int.MinValue;
            (int, int) bestMove = (0, 0);
            int depth = AllPossibleMoves().Count;

            foreach (var move in AllPossibleMoves())
            {
                MakeMove(move, 2);  // Simulate the AI move (2 represents AI)
                int moveValue = Minimax(depth - 1, false);  // Evaluate move using Minimax
                UndoMove(move);  // Undo the move

                if (moveValue > bestValue)  // Check if this move is better than the best so far
                {
                    bestValue = moveValue;
                    bestMove = move;
                }
            }
            Console.WriteLine(bestMove);
            return bestMove;
        }
    }
}
