using Assets.DataModels;
using System;
using UnityEngine;

public class BoardSpawning : MonoBehaviour
{
    public GameObject squarePrefab;
    public GameObject pawnPrefab;
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
        board[0, 1].Piece = new Piece(PieceType.Pawn, ColorType.White);
        UpdateVisualAt(0, 1);
    }

    private void UpdateVisualAt(int row, int column)
    {
        PieceType piece = board[row, column].Piece.Type;
        if (piece == PieceType.None)
            return;
        GameObject prefabToSpawn = null;

        switch (piece)
        {
            case PieceType.Pawn:
                prefabToSpawn = pawnPrefab;
                break;
        }
        if (prefabToSpawn != null)
        {
            Vector3 position = new Vector3(column, row, -2);
            Instantiate(prefabToSpawn, position, Quaternion.identity);
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
