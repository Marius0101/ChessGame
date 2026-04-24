using System.Collections.Generic;
using UnityEngine;

namespace Assets.DataModels.Type
{
    public class KingMoveStrategy : IMoveStrategy
    {
        public List<Vector2Int> GetAvailableMoves(Vector2Int position, SquareData[,] board)
        {
            List<Vector2Int> availableMoves = new();
            Piece piece = board[position.x, position.y].Piece;
            if (piece == null || piece.Type != PieceType.King)
                return availableMoves;
            RookMoveStrategy rookMoveStrategy = new RookMoveStrategy();
            rookMoveStrategy.GetMoves(position, board, availableMoves, piece, 2);
            BishopMoveStrategy bishopMoveStrategy = new BishopMoveStrategy();
            bishopMoveStrategy.GetMoves(position, board, availableMoves, piece, 2);
            foreach (var move in availableMoves.ToArray())
            {
                if(IsCheck(move, board, piece.Color))
                    availableMoves.Remove(move);
            }
            
            return availableMoves;
        }

        public bool IsCheck(Vector2Int position, SquareData[,] board, ColorType color)
        {
            KnightMoveStrategy knightMoveStrategy = new KnightMoveStrategy();
            if(knightMoveStrategy.KnightExist(position, board, color))
                return true;
            if(LateralAndVerticalCheck(position, board, color))
                return true;
            if(DiagonalCheck(position, board, color))
                return true;
            return false;
        }

        

        private static bool IsInsideBoard(Vector2Int position, SquareData[,] board)
        {
            return position.x >= 0 && position.x < board.GetLength(0) &&
                     position.y >= 0 && position.y < board.GetLength(1);
        }
        private bool DiagonalCheck(Vector2Int position, SquareData[,] board, ColorType color)
        {
            bool isValid = true;
            int counter = 1;
            while (isValid && counter < 8)
            {
                Vector2Int rightTop = new Vector2Int(position.x+counter, position.y+counter);
                if (!IsInsideBoard(rightTop, board))
                    break;
                Piece piece = board[rightTop.x, rightTop.y].Piece;
                if(piece == null)
                {
                    counter++;
                    continue;
                }
                if(IsSameColor(piece, color))
                    break;
                if(counter == 1 && (isKing(piece) || (isPawn(piece) && piece.Color == ColorType.Black)))
                    return true;
                if(isBishop(piece) || isQueen(piece))
                    return true;
                break;
            }
            isValid = true;
            counter = 1;
            while (isValid && counter < 8)
            {
                Vector2Int rightBottom = new Vector2Int(position.x-counter, position.y+counter);
                if (!IsInsideBoard(rightBottom, board))
                    break;
                Piece piece = board[rightBottom.x, rightBottom.y].Piece;
                if(piece == null)
                {
                    counter++;
                    continue;
                }
                if(IsSameColor(piece, color))
                    break;
                if(counter == 1 && (isKing(piece) || (isPawn(piece) && piece.Color == ColorType.White)))
                    return true;
                if(isBishop(piece) || isQueen(piece))
                    return true;
                break;
            }
            isValid = true;
            counter = 1;
            while (isValid && counter < 8)
            {
                Vector2Int leftTop = new Vector2Int(position.x+counter, position.y-counter);
                if (!IsInsideBoard(leftTop, board))
                    break;
                Piece piece = board[leftTop.x, leftTop.y].Piece;
                if(piece == null)
                {
                    counter++;
                    continue;
                }
                if(IsSameColor(piece, color))
                    break;
                if(counter == 1 && (isKing(piece) || (isPawn(piece) && piece.Color == ColorType.Black)))
                    return true;
                if(isBishop(piece) || isQueen(piece))
                    return true;
                break;
            }
            isValid = true;
            counter = 1;
            while (isValid && counter < 8)
            {
                Vector2Int leftBottom = new Vector2Int(position.x-counter, position.y-counter);
                if (!IsInsideBoard(leftBottom, board))
                    break;
                Piece piece = board[leftBottom.x, leftBottom.y].Piece;
                if(piece == null)
                {
                    counter++;
                    continue;
                }
                if(IsSameColor(piece, color))
                    break;
                if(counter == 1 && (isKing(piece) || (isPawn(piece) && piece.Color == ColorType.White)))
                    return true;
                if(isBishop(piece) || isQueen(piece))
                    return true;
                break;
            }
            return false;
        }

        internal bool LateralAndVerticalCheck(Vector2Int position, SquareData[,] board, ColorType color)
        {
            bool isValid = true;
            int counter = 1;
            while (isValid && counter < 8)
            {
                Vector2Int top = new Vector2Int(position.x + counter, position.y);
                if (!IsInsideBoard(top, board))
                    break;
                Piece piece = board[top.x, top.y].Piece;
                if(piece == null)
                {
                    counter++;
                    continue;
                }
                if(IsSameColor(piece, color))
                    isValid = false;
                if(isKing(piece) && counter == 1 && !IsSameColor(piece, color))
                    return true;
                if((isRook(piece) || isQueen(piece)) && !IsSameColor(piece, color))
                    return true;
                break;
            }
            isValid = true;
            counter = 1;
            while (isValid && counter < 8)
            {
                Vector2Int bottom = new Vector2Int(position.x - counter, position.y);
                if (!IsInsideBoard(bottom, board))
                    break;
                Piece piece = board[bottom.x, bottom.y].Piece;
                if(piece == null)
                {
                    counter++;
                    continue;
                }
                if(isKing(piece) && counter == 1 && !IsSameColor(piece, color))
                    return true;
                if(IsSameColor(piece, color))
                    isValid = false;
                if((isRook(piece) || isQueen(piece)) && !IsSameColor(piece, color))
                    return true;
                break;
            }
            isValid = true;
            counter = 1;
            while (isValid && counter < 8)
            {
                Vector2Int right = new Vector2Int(position.x, position.y + counter);
                if (!IsInsideBoard(right, board))
                    break;
                Piece piece = board[right.x, right.y].Piece;
                if(piece == null)
                {
                    counter++;
                    continue;
                }
                if(IsSameColor(piece, color))
                    isValid = false;
                if(isKing(piece) && counter == 1 && !IsSameColor(piece, color))
                    return true;
                if((isRook(piece) || isQueen(piece)) && !IsSameColor(piece, color))
                    return true;
                break;
            }
            isValid = true;
            counter = 1;
            while (isValid && counter < 8)
            {
                Vector2Int left = new Vector2Int(position.x, position.y - counter);
                if (!IsInsideBoard(left, board))
                    break;
                Piece piece = board[left.x, left.y].Piece;
                if(piece == null)
                {
                    counter++;
                    continue;
                }
                if(IsSameColor(piece, color))
                    isValid = false;
                if(isKing(piece) && counter == 1 && !IsSameColor(piece, color))
                    return true;
                if((isRook(piece) || isQueen(piece)) && !IsSameColor(piece, color))
                    return true;
                break;
            }
            return false;
        }

        private bool isKing(Piece piece)
        {
            return piece.Type == PieceType.King;
        }

        private static bool isRook(Piece piece)
        {
            return piece.Type == PieceType.Rook;
        }
        private static bool isQueen(Piece piece)
        {
            return piece.Type == PieceType.Queen;
        }
        private static bool IsSameColor(Piece piece, ColorType color)
        {
            return piece.Color == color;
        }
        private static bool isBishop(Piece piece)
        {
            return piece.Type == PieceType.Bishop;
        }
        private static bool isPawn(Piece piece)
        {
            return piece.Type == PieceType.Pawn;
        }
    }
}