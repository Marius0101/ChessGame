using Assets.DataModels.Game.Board;
using Assets.DataModels.Game.Type;
using UnityEngine;

public class BoardSpawning : MonoBehaviour
{
    #region Prefab
    public GameObject squarePrefab;
    public GameObject pawnPrefab;
    public GameObject bishopPrefab;
    public GameObject knightPrefab;
    public GameObject rookPrefab;
    public GameObject queenPrefab;
    public GameObject kingPrefab;
    #endregion
    private GameObject[,] visualSquares;

    public BoardState Initialize(int size, BoardSpawnType boardSpawnType)
    {
        CreateBoardVisual(size);
        BoardState boardState = SpawnPieces(size,boardSpawnType);
        return boardState;
    }

    private BoardState SpawnPieces(int size ,BoardSpawnType boardSpawnType)
    {
        BoardState boardState = new BoardState(size);
        if(boardSpawnType == BoardSpawnType.None)
            return boardState;
        if(boardSpawnType == BoardSpawnType.Startup)
            boardState = CreateStartupPosition(boardState);
            return boardState;
    }

    private BoardState CreateStartupPosition(BoardState boardState)
    {
        SquareData[,] board = boardState.board;
        for (int i = 0; i < boardState.Size; i++)
        {
            board[0, i].Piece = new Piece(BackRank[i], ColorType.White);
            UpdateVisualAt(board,0, i);
            board[7, i].Piece = new Piece(BackRank[i], ColorType.Black);
            UpdateVisualAt(board, 7, i);

            board[1, i].Piece = new Piece(PieceType.Pawn, ColorType.White);
            UpdateVisualAt(board, 1, i);
            board[6, i].Piece = new Piece(PieceType.Pawn, ColorType.Black);
            UpdateVisualAt(board, 6, i);
        }
        return boardState;
    }
    private static readonly PieceType[] BackRank =
    {
        PieceType.Rook,
        PieceType.Knight,
        PieceType.Bishop,
        PieceType.Queen,
        PieceType.King,
        PieceType.Bishop,
        PieceType.Knight,
        PieceType.Rook
    };
    private void UpdateVisualAt(SquareData[,] board, int row, int column)
    {
        Piece piece = board[row, column].Piece;
        if (piece == null || piece.Type == PieceType.None)
            return;
        GameObject prefabToSpawn = null;

        switch (piece.Type)
        {
            case PieceType.Pawn:
                prefabToSpawn = pawnPrefab;
                break;
            case PieceType.Bishop:
                prefabToSpawn = bishopPrefab;
                break;
            case PieceType.Knight:
                prefabToSpawn = knightPrefab;
                break;
            case PieceType.Rook:
                prefabToSpawn = rookPrefab;
                break;
            case PieceType.Queen:
                prefabToSpawn = queenPrefab;
                break;
            case PieceType.King:
                prefabToSpawn = kingPrefab;
                break;
        }
        if (prefabToSpawn != null)
        {
            Vector3 position = new Vector3(column, row, -2);
            GameObject newPiece= Instantiate(prefabToSpawn, position, Quaternion.identity);
            ApplyColor(newPiece, piece.Color);
            board[row, column].Piece.VisualObject = newPiece; 
        }
    }

    private void ApplyColor(GameObject pieceObject, ColorType color)
    {
        SpriteRenderer outline = pieceObject.transform.Find("Outline").GetComponent<SpriteRenderer>();
        SpriteRenderer Inline = pieceObject.transform.Find("Inline").GetComponent<SpriteRenderer>();

        if (color == ColorType.Black)
        {
            outline.color = Color.white;
            Inline.color = Color.black;
        }
    }

    void CreateBoardVisual(int numberOfSquares)
    {
        visualSquares = new GameObject[numberOfSquares, numberOfSquares];

        for (int i = 0; i < numberOfSquares; i++)
        {
            for (int j = 0; j < numberOfSquares; j++)
            {
                Vector3 position = new Vector3(i, j, 0);
                GameObject square = Instantiate(squarePrefab, position, Quaternion.identity);
                visualSquares[i, j] = square;
                Renderer renderer = square.GetComponent<Renderer>();
                if ((i + j) % 2 == 0)
                    renderer.material.color = Color.white;
                else
                    renderer.material.color = Color.black;
            }
        }
    }
}
