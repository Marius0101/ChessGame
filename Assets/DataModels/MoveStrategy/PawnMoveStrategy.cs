using System.Collections.Generic;
using UnityEngine;

namespace Assets.DataModels.Type
{
    public class PawnMoveStrategy : IMoveStrategy
    {
        public List<Vector2Int> GetAvailableMoves(Vector2Int position, BoardState board)
        {
            return new List<Vector2Int>();
        }
    }
}