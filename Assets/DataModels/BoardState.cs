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

        private bool IsValidMove(Vector2Int originalPostion, Vector2Int newPostion)
        {
            //TODO: get availbele moves for the piece and check if the new position is in that list
            // Make sublacks depending of the piece type, for example for a pawn it can only move forward, for a knight it can move in L shape, etc.

            var piece = board[originalPostion.x, originalPostion.y].Piece;
            var targetPiece = board[newPostion.x, newPostion.y].Piece;
            if(targetPiece != null && targetPiece.Color == piece?.Color)
            {
                Debug.Log("Invalid move: Cannot capture your own piece.");
                return false;
            }
            Debug.Log($"Piece at original position: {piece?.Type.ToString() ?? "None"}");
            return true;
        }

        public MoveResult UpdateState(Vector2Int originalPostion, Vector2Int newPostion)
        {
            if(!IsValidMove(originalPostion, newPostion))
            {
                return new MoveResult
                {
                    Success = false,
                    CapturedPiece = null
                };
            }
            Piece capturedPiece = board[newPostion.x, newPostion.y].Piece;
            board[newPostion.x, newPostion.y].Piece = board[originalPostion.x, originalPostion.y].Piece;
            board[originalPostion.x, originalPostion.y].Piece = null;
            return new MoveResult
            {
                Success = IsValidMove(originalPostion, newPostion),
                CapturedPiece = capturedPiece
            };
        }
    }


}