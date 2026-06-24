using System;
using Assets.DataModels.Game.Move.MoveStrategy;
using Assets.DataModels.Game.Type;
using UnityEngine;

namespace Assets.DataModels.Game.Board
{
    public class Piece
    {
        public PieceType Type;
        public ColorType Color;
        public GameObject VisualObject;
        public IMoveStrategy MoveStrategy;

        Piece()
        {
            Type = PieceType.None;
            Color = ColorType.None;
            VisualObject = null;
            MoveStrategy = CreateStrategy(Type);
        }


        public Piece(PieceType pieceType, ColorType colorType)
        {
            Type = pieceType;
            Color = colorType;
            VisualObject = null;
            MoveStrategy = CreateStrategy(Type);
        }
        public bool IsWhite()
        {
            return Color == ColorType.White;
        }
        private IMoveStrategy CreateStrategy(PieceType type)
        {
            switch (type)
            {
                case PieceType.Pawn: 
                    return new PawnMoveStrategy();
                case PieceType.Knight:
                    return new KnightMoveStrategy();
                case PieceType.Bishop:
                    return new BishopMoveStrategy();
                case PieceType.Rook:
                    return new RookMoveStrategy();
                case PieceType.Queen:
                    return new QueenMoveStrategy();
                case PieceType.King:
                    return new KingMoveStrategy();
                default:
                    throw new ArgumentException($"No move strategy defined for piece type: {type}");
            }
        }
    }
}
