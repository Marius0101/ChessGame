namespace Assets.DataModels
{
    public class Piece
    {
        public PieceType Type { get; set; }
        public ColorType Color { get; set; }

        Piece()
        {
            Type= PieceType.None;
            Color= ColorType.None;
        }
        public Piece(PieceType pieceType, ColorType colorType)
        {
            Type = pieceType;
            Color = colorType;
        }
    }
}
