namespace SwapQLib
{
    public sealed class SwapQLAutoIncrement
    {
        public string table;
        public string column;
        public int startValue;

        public SwapQLAutoIncrement(string table, string column, int startValue)
        {
            this.table = table;
            this.column = column;
            this.startValue = startValue;
        }
    }
}
