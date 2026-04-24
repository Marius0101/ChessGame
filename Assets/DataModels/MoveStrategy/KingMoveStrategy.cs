using System.Collections.Generic;
using UnityEngine;

namespace Assets.DataModels.Type
{
    public class KingMoveStrategy : IMoveStrategy
    {
        public List<Vector2Int> GetAvailableMoves(Vector2Int position, SquareData[,] board)
        {
            List<Vector2Int> availableMoves = new();
            Piece piece = board[position.x, position.y].Piece;
            if (piece == null || piece.Type != PieceType.King)
                return availableMoves;
            RookMoveStrategy rookMoveStrategy = new RookMoveStrategy();
            rookMoveStrategy.GetMoves(position, board, availableMoves, piece, 2);
            BishopMoveStrategy bishopMoveStrategy = new BishopMoveStrategy();
            bishopMoveStrategy.GetMoves(position, board, availableMoves, piece, 2);
            return availableMoves;
        }
    }
}