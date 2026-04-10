using Assets.DataModels;
using System;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;
using static UnityEngine.Rendering.DebugUI.Table;

public class BoardSpawning : MonoBehaviour
{
    public GameObject squarePrefab;
    public GameObject pawnPrefab;
    public GameObject bishopPrefab;
    public GameObject knightPrefab;
    public GameObject rookPrefab;
    public GameObject queenPrefab;
    public GameObject kingPrefab;
    public int numberOfSquares = 8;
    private SquareData[,] board;
    private GameObject[,] visualSquares;
    void Start()
    {
        InitializeBoard();
        visualSquares = new GameObject[numberOfSquares, numberOfSquares];
        for (int i = 0; i < numberOfSquares; i++)
        {
            for(int j = 0; j < numberOfSquares; j++)
            {
                board[i, j] = new SquareData();
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
        SpawnTestPieces();
    }

    private void SpawnTestPieces()
    {
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
            UpdateVisualAt(0, i);
            UpdateVisualAt(1, i);
            UpdateVisualAt(6, i);
            UpdateVisualAt(7, i);
        }

    }

    private void UpdateVisualAt(int row, int column)
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

    void InitializeBoard()
    {
        board = new SquareData[numberOfSquares, numberOfSquares];

        for (int i = 0; i < numberOfSquares; i++)
        {
            for (int j = 0; j < numberOfSquares; j++)
            {
                board[i, j] = new SquareData();
            }
        }
    }
}
