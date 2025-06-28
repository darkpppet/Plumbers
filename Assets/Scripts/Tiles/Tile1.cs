namespace Tiles
{
    /**
     * ㅡ 모양
     */
    public class Tile1 : TileBase
    {
        public override bool Left { get; protected set; } = true;
        public override bool Right { get; protected set; } = true;
        public override bool Top { get; protected set; } = false;
        public override bool Bottom { get; protected set; } = false;

        public override int Cost { get; protected set; } = 40;
    }
}
