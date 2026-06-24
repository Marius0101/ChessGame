using System.Collections.Generic;
using Assets.DataModels.Game.Type;
using UnityEngine;

namespace Assets.DataModels.Game.Move.MoveStrategy
{
    public class KnightMoveStrategy : IMoveStrategy
    {
        public List<Vector2Int> GetAvailableMoves(Vector2Int position, SquareData[,] board)
        {
            List<Vector2Int> availableMoves = new();
            Piece piece = board[position.x, position.y].Piece;
            if (piece == null || piece.Type != PieceType.Knight)
                return availableMoves;
            
            foreach (var move in LocationToCheck())
            {
                Vector2Int newPosition = position + move;
                TryAddMove(newPosition, board, availableMoves, piece);
            }
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

        internal bool KnightExist(Vector2Int position, SquareData[,] board, ColorType color)
        {
            foreach (var move in LocationToCheck())
            {
                Vector2Int knightPosition = position + move;
                if (!IsInsideBoard(knightPosition, board))
                    continue;
                Piece piece = board[knightPosition.x, knightPosition.y].Piece;
                if (piece != null && piece.Type == PieceType.Knight && piece.Color != color)
                    return true;
            }
            return false;
        }
        private static List<Vector2Int> LocationToCheck()
        {
            return new List<Vector2Int>
            {
                new Vector2Int(1, 2),
                new Vector2Int(2, 1),
                new Vector2Int(1, -2),
                new Vector2Int(2, -1),
                new Vector2Int(-1, 2),
                new Vector2Int(-2, 1),
                new Vector2Int(-1, -2),
                new Vector2Int(-2, -1)
            };
        }
    }
}