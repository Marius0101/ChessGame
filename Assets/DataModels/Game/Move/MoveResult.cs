using Assets.DataModels.Game.Board;
namespace Assets.DataModels
{
    public class MoveResult
    {
        public bool IsSuccess {get;set;}
        public Piece CapturedPiece {get;set;}

        public MoveResult(bool isSuccess, Piece capturedPiece)
        {
            IsSuccess = isSuccess;
            CapturedPiece = capturedPiece;
        }
    }
}