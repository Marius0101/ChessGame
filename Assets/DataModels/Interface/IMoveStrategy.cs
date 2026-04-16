using System.Collections.Generic;
using UnityEngine;

namespace Assets.DataModels.Type
{
    public interface IMoveStrategy
    {
        List<Vector2Int> GetAvailableMoves(Vector2Int position, BoardState board);
    }
}