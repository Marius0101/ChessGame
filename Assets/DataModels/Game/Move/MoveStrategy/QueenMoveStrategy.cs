using System.Collections.Generic;
using Assets.DataModels.Game.Type;
using UnityEngine;

namespace Assets.DataModels.Game.Move.MoveStrategy
{
    public class QueenMoveStrategy : IMoveStrategy
    {
        public List<Vector2Int> GetAvailableMoves(Vector2Int position, SquareData[,] board)
        {
            List<Vector2Int> availableMoves = new();
            Piece piece = board[position.x, position.y].Piece;
            if (piece == null || piece.Type != PieceType.Queen)
                return availableMoves;
            RookMoveStrategy rookMoveStrategy = new RookMoveStrategy();
            rookMoveStrategy.GetMoves(position, board, availableMoves, piece);
            BishopMoveStrategy bishopMoveStrategy = new BishopMoveStrategy();
            bishopMoveStrategy.GetMoves(position, board, availableMoves, piece);
            return availableMoves;
        }
    }
}