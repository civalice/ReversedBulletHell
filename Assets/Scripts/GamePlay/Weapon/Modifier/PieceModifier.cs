namespace Urxxx.GamePlay
{
    public class PieceModifier : BaseModifier
    {
        public int BonusPieceCount = 1;

        public PieceModifier()
        {
            StatModifier.PieceCount = BonusPieceCount;
        }
    }
}