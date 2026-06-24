using System.Collections.Generic;
using Assets.DataModels.Game.Board;
using UnityEngine;

namespace Assets.DataModels.Game.Move.MoveStrategy
{
    public interface IMoveStrategy
    {
        List<Vector2Int> GetAvailableMoves(Vector2Int position, SquareData[,] board);
    }
}