using Assets.DataModels;
using Assets.DataModels.Type;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int numberOfSquares = 8;
    public BoardSpawning boardView;

    void Start()
    {
        BoardState state = new BoardState(numberOfSquares);
        boardView.Initialize(state, BoardSpawnType.Startup);
    }
}