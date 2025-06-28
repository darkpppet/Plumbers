namespace Tiles
{
    /**
     * + 모양
     */
    public class Tile4 : TileBase
    {
        public override bool Left { get; protected set; } = true;
        public override bool Right { get; protected set; } = true;
        public override bool Top { get; protected set; } = true;
        public override bool Bottom { get; protected set; } = true;

        public override int Cost { get; protected set; } = 10;
        
        public override float TileDamage { get; protected set; } = 2.5f;
    }
}
