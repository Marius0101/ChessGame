using System.Collections.Generic;
using UnityEngine;

namespace Assets.DataModels.Type
{
    public class BishopMoveStrategy : IMoveStrategy
    {
        public List<Vector2Int> GetAvailableMoves(Vector2Int position, SquareData[,] board)
        {
            List<Vector2Int> availableMoves = new();
            Piece piece = board[position.x, position.y].Piece;
            if (piece == null || piece.Type != PieceType.Bishop)
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
                Vector2Int rightTop = new Vector2Int(position.x+counter, position.y+counter);
                isValid = TryAddMove(rightTop, board, availableMoves, piece);
                counter++;
            }
            isValid = true;
            counter = 1;
            while (isValid && counter < maxSteps)
            {
                Vector2Int rightBottom = new Vector2Int(position.x-counter, position.y+counter);
                isValid = TryAddMove(rightBottom, board, availableMoves, piece);
                counter++;
            }
            isValid = true;
            counter = 1;
            while (isValid && counter < maxSteps)
            {
                Vector2Int leftTop = new Vector2Int(position.x+counter, position.y-counter);
                isValid = TryAddMove(leftTop, board, availableMoves, piece);
                counter++;
            }
            isValid = true;
            counter = 1;
            while (isValid && counter < maxSteps)
            {
                Vector2Int leftBottom = new Vector2Int(position.x-counter, position.y-counter);
                isValid = TryAddMove(leftBottom, board, availableMoves, piece);
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