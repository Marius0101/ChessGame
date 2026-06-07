using UnityEngine;

namespace Assets.DataModels
{
    public class MoveHistory
    {
        public Vector2Int OriginalPosition { get; set; }
        public Vector2Int NewPosition { get; set; }
        public Piece MovedPiece { get; set; }
        public Piece CapturedPiece { get; set; }
        public MoveHistory(Vector2Int originalPosition, Vector2Int newPosition, Piece movedPiece, Piece capturedPiece)
        {
            OriginalPosition = originalPosition;
            NewPosition = newPosition;
            MovedPiece = movedPiece;
            CapturedPiece = capturedPiece;
        }
    }
}