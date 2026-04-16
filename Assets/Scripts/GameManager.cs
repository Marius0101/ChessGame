using Assets.DataModels;
using Assets.DataModels.Type;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int numberOfSquares = 8;
    public BoardSpawning boardView;
    public BoardState state;

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
        if (!result.Success)
        {
            piece.ResetPosition();
            return;
        }
        if(result.CapturedPiece != null)
            Destroy(result.CapturedPiece.VisualObject);
        piece.ChangePosition(newPosition);
        string originalSquareName = SquareData.GetSqaureName(originalPosition.x, originalPosition.y);
        string newSquareName = SquareData.GetSqaureName(newPosition.x, newPosition.y);
        Debug.Log($"Moved piece from {originalSquareName} to {newSquareName}");
    }
}