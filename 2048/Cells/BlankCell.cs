namespace _2048
{
    class BlankCell : Cell
    {
        public BlankCell()
        {
            this.value = 0;
        }

        public override bool IsBlank()
        {
            return true;
        }

        public override int Merge(int x)
        {
            return 0;
        }
    }
}
