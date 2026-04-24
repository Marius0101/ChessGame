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
            var piece = board[originalPostion.x, originalPostion.y].Piece;
            if(piece == null)
            {
                Debug.Log("Invalid move: No piece at the original position.");
                return false;
            }
            var availableMoves = piece.MoveStrategy.GetAvailableMoves(originalPostion, board);
            if (!availableMoves.Contains(newPostion))
            {
                Debug.Log("Invalid move: Position is not in the list of available moves.");
                return false;
            }
            Debug.Log("Valid move: Position is in the list of available moves.");
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
                Success = true,
                CapturedPiece = capturedPiece
            };
        }
    }


}