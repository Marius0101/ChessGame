using UnityEngine;

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

        private bool IsValidMove(Vector2Int from, Vector2Int to)
        {
            var piece = board[from.x, from.y].Piece;
            Debug.Log($"Piece at original position: {piece?.Type.ToString() ?? "None"}");
            return true;
        }

        public MoveResult UpdateState(Vector2Int originalPostion, Vector2Int newPostion)
        {
            return new MoveResult
            {
                Success = IsValidMove(originalPostion, newPostion),
                CapturedPiece = board[newPostion.x, newPostion.y].Piece
            };
        }
    }


}