using System.Collections.Generic;
using UnityEngine;

namespace Assets.DataModels.Type
{
    public class KnightMoveStrategy : IMoveStrategy
    {
        public List<Vector2Int> GetAvailableMoves(Vector2Int position, SquareData[,] board)
        {
            List<Vector2Int> availableMoves = new();
            Piece piece = board[position.x, position.y].Piece;
            if (piece == null || piece.Type != PieceType.Knight)
                return availableMoves;
            
            Vector2Int leftTop1 = new Vector2Int(position.x+1, position.y-2);
            TryAddMove(leftTop1, board, availableMoves, piece);
            Vector2Int leftTop2 = new Vector2Int(position.x+2, position.y-1);
            TryAddMove(leftTop2, board, availableMoves, piece);

            Vector2Int rightTop1 = new Vector2Int(position.x+1, position.y+2);
            TryAddMove(rightTop1, board, availableMoves, piece);
            Vector2Int rightTop2 = new Vector2Int(position.x+2, position.y+1);
            TryAddMove(rightTop2, board, availableMoves, piece);

            Vector2Int leftBottom1 = new Vector2Int(position.x-1, position.y-2);
            TryAddMove(leftBottom1, board, availableMoves, piece);
            Vector2Int leftBottom2 = new Vector2Int(position.x-2, position.y-1);
            TryAddMove(leftBottom2, board, availableMoves, piece);

            Vector2Int rightBottom1 = new Vector2Int(position.x-1, position.y+2);
            TryAddMove(rightBottom1, board, availableMoves, piece);
            Vector2Int rightBottom2 = new Vector2Int(position.x-2, position.y+1);
            TryAddMove(rightBottom2, board, availableMoves, piece);

            return availableMoves;
        }

        private static void TryAddMove(Vector2Int position, SquareData[,] board, List<Vector2Int> availableMoves, Piece piece)
        {
            if(!IsInsideBoard(position, board))
                return;
            Piece targetPiece = board[position.x, position.y].Piece;
            if (targetPiece == null)
            {
                availableMoves.Add(position);
                return;
            }
            if(targetPiece.Color != piece.Color)
                availableMoves.Add(position);
        }
        private static bool IsInsideBoard(Vector2Int position, SquareData[,] board)
        {
            return position.x >= 0 && position.x < board.GetLength(0) &&
                     position.y >= 0 && position.y < board.GetLength(1);
        }
    }
}