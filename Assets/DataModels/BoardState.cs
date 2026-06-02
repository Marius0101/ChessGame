using System;
using Assets.DataModels.Type;
using NUnit.Framework;
using UnityEngine;

namespace Assets.DataModels
{
    public class BoardState
    {
        public SquareData[,] board;
        public int Size => board.GetLength(0);
        private bool whiteTurn;
        private KingMoveStrategy kingMoveStrategy = new KingMoveStrategy();
        public BoardState(int size)
        {
            board = new SquareData[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = new SquareData();
                }
            }
            whiteTurn = true;
        }

        private bool IsValidMove(Vector2Int originalPostion, Vector2Int newPostion)
        {
            var piece = board[originalPostion.x, originalPostion.y].Piece;
            if(piece == null)
            {
                Debug.Log("Invalid move: No piece at the original position.");
                return false;
            }
            if (!isPlayerTurn(piece))
            {
                Debug.Log("Invalid move: It's not the player's turn.");
                return false;
            }
            var availableMoves = piece.MoveStrategy.GetAvailableMoves(originalPostion, board);
            if (!availableMoves.Contains(newPostion))
            {
                Debug.Log("Invalid move: Position is not in the list of available moves.");
                return false;
            }
            if(IsKingInCheck(piece.Color,originalPostion, newPostion,piece))
            {
                Debug.Log("Invalid move: King is in check.");
                return false;
            }
            Debug.Log("Valid move: Position is in the list of available moves.");
            whiteTurn = !whiteTurn;
            return true;
        }

        
        public MoveResult UpdateState(Vector2Int originalPostion, Vector2Int newPostion)
        {
            if(!IsValidMove(originalPostion, newPostion))
            {
                return new MoveResult
                {
                    Success = false,
                    CapturedPiece = null
                };
            }
            Piece capturedPiece = board[newPostion.x, newPostion.y].Piece;
            board[newPostion.x, newPostion.y].Piece = board[originalPostion.x, originalPostion.y].Piece;
            board[originalPostion.x, originalPostion.y].Piece = null;
            return new MoveResult
            {
                Success = true,
                CapturedPiece = capturedPiece
            };
        }
        private bool IsKingInCheck(ColorType color,Vector2Int originalPosition, Vector2Int newPosition, Piece piece)
        {
            SquareData[,] tempBoard =  CloneBoard();
            tempBoard[newPosition.x, newPosition.y].Piece = piece;
            tempBoard[originalPosition.x, originalPosition.y].Piece = null;
            Vector2Int kingPosition = FindKingPosition(tempBoard,color);
            return kingMoveStrategy.IsCheck(kingPosition, tempBoard, color);
        }

        private Vector2Int FindKingPosition(SquareData[,] board, ColorType color)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Piece piece = board[i, j].Piece;
                    if (piece != null && piece.Type == PieceType.King && piece.Color == color)
                    {
                        return new Vector2Int(i, j);
                    }
                }
            }
            throw new Exception("King not found on the board.");
        }
        private bool isPlayerTurn(Piece piece)
        {
            if (piece.IsWhite())
                return whiteTurn;
            return !whiteTurn;
        }
        private SquareData[,] CloneBoard()
        {
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);

            SquareData[,] clone = new SquareData[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    clone[r, c] = new SquareData
                    {
                        Piece = board[r, c].Piece
                    };
                }
            }

            return clone;
        }
    }


}