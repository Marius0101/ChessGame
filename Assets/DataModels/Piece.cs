using System;
using Assets.DataModels.Type;
using UnityEngine;

namespace Assets.DataModels
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
        }
        private IMoveStrategy CreateStrategy(PieceType type)
        {
            switch (type)
            {
                case PieceType.Pawn: 
                    return new PawnMoveStrategy();
                case PieceType.None: 
                    return null;
                default:
                    throw new ArgumentException($"No move strategy defined for piece type: {type}");
            }
        }
    }
}
