namespace TetrisApp
{
    public class Block
    {
        public int RowIndex = 0;
        public int ColIndex = 0;

        public Block Down()
        {
            return new Block()
            {
                RowIndex = RowIndex + 1,
                ColIndex = ColIndex
            };
        }
    }
}