using Assets.DataModels;
using Assets.DataModels.Type;
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
        //TODO: Add pieces to initial position
        //Temporary just set some tests pieces
        SpawnTestPieces(boardState);
        return boardState;
    }

    private void SpawnTestPieces(BoardState boardState)
    {
        SquareData[,] board = boardState.board;
        int numberOfSquares = boardState.Size;
        board[0, 0].Piece = new Piece(PieceType.Rook, ColorType.White);
        board[1, 0].Piece = new Piece(PieceType.Pawn, ColorType.White);
        board[0, 1].Piece = new Piece(PieceType.Knight, ColorType.White);
        board[0, 3].Piece = new Piece(PieceType.Queen, ColorType.White);
        board[0, 2].Piece = new Piece(PieceType.Bishop, ColorType.White);
        board[0, 4].Piece = new Piece(PieceType.King, ColorType.White);

        board[7, 0].Piece = new Piece(PieceType.Rook, ColorType.Black);
        board[6, 0].Piece = new Piece(PieceType.Pawn, ColorType.Black);
        board[7, 1].Piece = new Piece(PieceType.Knight, ColorType.Black);
        board[7, 3].Piece = new Piece(PieceType.Queen, ColorType.Black);
        board[7, 2].Piece = new Piece(PieceType.Bishop, ColorType.Black);
        board[7, 4].Piece = new Piece(PieceType.King, ColorType.Black);

        for(int i =0 ; i < numberOfSquares; i++)
        {
            UpdateVisualAt(board,0, i);
            UpdateVisualAt(board, 1, i);
            UpdateVisualAt(board, 6, i);
            UpdateVisualAt(board, 7, i);
        }

    }

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
            GameObject  newPiece= Instantiate(prefabToSpawn, position, Quaternion.identity);
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
