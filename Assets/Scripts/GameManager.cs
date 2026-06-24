using System.Linq;
using Assets.DataModels.Game.Board;
using Assets.DataModels.Game.Type;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int numberOfSquares = 8;
    public BoardSpawning boardView;
    public BoardState state;
    
    // public HistoryView historyView;
    void Start()
    {
        state = boardView.Initialize(numberOfSquares, BoardSpawnType.Startup);
    }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void MovePiece(Vector2Int originalPosition ,Vector2Int newPosition, PieceMoving piece)
    {
        var result = state.UpdateState(originalPosition,newPosition);
        if (!result.IsSuccess)
        {
            piece.ResetPosition();
            return;
        }
        if(result.CapturedPiece != null)
            Destroy(result.CapturedPiece.VisualObject);
        piece.ChangePosition(newPosition);

        state.moveHistory.Last();
        //Just for testing, will be removed later
        string originalSquareName = SquareData.GetSqaureName(originalPosition.x, originalPosition.y);
        string newSquareName = SquareData.GetSqaureName(newPosition.x, newPosition.y);
        Debug.Log($"Moved piece from {originalSquareName} to {newSquareName}");
    }
}