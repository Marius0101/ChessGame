using System.Collections.Generic;
using Assets.DataModels.Game.Type;
using UnityEngine;

namespace Assets.DataModels.Game.Move.MoveStrategy
{
    public class RookMoveStrategy : IMoveStrategy
    {
        public List<Vector2Int> GetAvailableMoves(Vector2Int position, SquareData[,] board)
        {
            List<Vector2Int> availableMoves = new();
            Piece piece = board[position.x, position.y].Piece;
            if (piece == null || piece.Type != PieceType.Rook)
                return availableMoves;
            GetMoves(position, board, availableMoves, piece);
            return availableMoves;
        }

        internal void GetMoves(Vector2Int position, SquareData[,] board, List<Vector2Int> availableMoves, Piece piece, int maxSteps = 8)
        {
            bool isValid = true;
            int counter = 1;
            while (isValid && counter < maxSteps)
            {
                Vector2Int top = new Vector2Int(position.x + counter, position.y);
                isValid = TryAddMove(top, board, availableMoves, piece);
                counter++;
            }
            isValid = true;
            counter = 1;
            while (isValid && counter < maxSteps)
            {
                Vector2Int bottom = new Vector2Int(position.x - counter, position.y);
                isValid = TryAddMove(bottom, board, availableMoves, piece);
                counter++;
            }
            isValid = true;
            counter = 1;
            while (isValid && counter < maxSteps)
            {
                Vector2Int right = new Vector2Int(position.x, position.y + counter);
                isValid = TryAddMove(right, board, availableMoves, piece);
                counter++;
            }
            isValid = true;
            counter = 1;
            while (isValid && counter < maxSteps)
            {
                Vector2Int left = new Vector2Int(position.x, position.y - counter);
                isValid = TryAddMove(left, board, availableMoves, piece);
                counter++;
            }
        }

        private static bool TryAddMove(Vector2Int position, SquareData[,] board, List<Vector2Int> availableMoves, Piece piece)
        {
            if(!IsInsideBoard(position, board))
                return false;
            Piece targetPiece = board[position.x, position.y].Piece;
            if (targetPiece == null)
            {
                availableMoves.Add(position);
                return true;
            }
            if(targetPiece.Color != piece.Color)
            {
                availableMoves.Add(position);
                return false;
            }
            return false;
        }
        private static bool IsInsideBoard(Vector2Int position, SquareData[,] board)
        {
            return position.x >= 0 && position.x < board.GetLength(0) &&
                     position.y >= 0 && position.y < board.GetLength(1);
        }
    }
}