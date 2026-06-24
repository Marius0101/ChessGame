using Assets.DataModels.Game.Board;

namespace Assets.DataModels.Game.Board
{
    public class SquareData
    {
        public Piece Piece { get; set; }
        public SquareData()
        {
            Piece = null;
        }

        public static string GetSqaureName(int rowCoordinates, int columnCoordinates)
        {
            char column = (char)('a' + columnCoordinates);
            int row = rowCoordinates + 1;

            return $"{column}{row}";
        }
    }
}
