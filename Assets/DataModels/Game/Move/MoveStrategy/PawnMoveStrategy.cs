using System.Collections.Generic;
using Assets.DataModels.Game.Type;
using UnityEngine;

namespace Assets.DataModels.Game.Move.MoveStrategy
{
    public class PawnMoveStrategy : IMoveStrategy
    {
        public List<Vector2Int> GetAvailableMoves(Vector2Int position, SquareData[,] board)
        {
            List<Vector2Int> availableMoves = new();
            Piece piece = board[position.x, position.y].Piece;
            if (piece == null || piece.Type != PieceType.Pawn)
                return availableMoves;
            int direction = piece.Color == ColorType.White ? 1 : -1;
            Vector2Int frontPosition = new Vector2Int(position.x + direction, position.y);
            TryAddMove(frontPosition, board, availableMoves);
            if (FirstMove(position.x, piece.Color))
            {
                Vector2Int secondPosition = new Vector2Int(position.x + 2 * direction, position.y);
                TryAddMove(secondPosition, board, availableMoves);
            }
            Vector2Int diagonalRight = new Vector2Int(position.x + direction, position.y + 1);
            TryAddCaptureMoves(diagonalRight, board, availableMoves, piece);
            
            Vector2Int diagonalLeft = new Vector2Int(position.x + direction, position.y - 1);
            TryAddCaptureMoves(diagonalLeft, board, availableMoves, piece);
            return availableMoves;
        }

        private void TryAddCaptureMoves(Vector2Int position, SquareData[,] board, List<Vector2Int> availableMoves, Piece piece)
        {
            if(!IsInsideBoard(position, board))
                return;
            var targetPiece = board[position.x, position.y].Piece;
            if (targetPiece != null && targetPiece.Color != piece.Color)
                availableMoves.Add(position);
        }

        private static void TryAddMove(Vector2Int position, SquareData[,] board, List<Vector2Int> availableMoves)
        {
            if(!IsInsideBoard(position, board))
                return;
            Piece frontPiece = board[position.x, position.y].Piece;
            if (frontPiece == null)
                availableMoves.Add(position);
        }
        private static bool IsInsideBoard(Vector2Int position, SquareData[,] board)
        {
            return position.x >= 0 && position.x < board.GetLength(0) &&
                     position.y >= 0 && position.y < board.GetLength(1);
        }

        private bool FirstMove(int x, ColorType color)
        {
            if(color == ColorType.White && x == 1)
                return true;
            if(color == ColorType.Black && x == 6)
                return true;
            return false;
        }
    }
}