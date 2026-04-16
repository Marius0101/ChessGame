using Assets.DataModels.Type;
using UnityEngine;

namespace Assets.DataModels
{
    public class Piece
    {
        public PieceType Type { get; set; }
        public ColorType Color { get; set; }
        public GameObject VisualObject {get; set;}

        Piece()
        {
            Type = PieceType.None;
            Color = ColorType.None;
            VisualObject = null;
        }
        public Piece(PieceType pieceType, ColorType colorType)
        {
            Type = pieceType;
            Color = colorType;
            VisualObject = null;
        }
    }
}
