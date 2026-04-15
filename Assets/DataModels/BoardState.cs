namespace Assets.DataModels
{
    public class BoardState
    {
        public SquareData[,] board;
        public int Size => board.GetLength(0);
        public BoardState(int size)
        {
            board = new SquareData[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = new SquareData();
                }
            }
        }
    }
}