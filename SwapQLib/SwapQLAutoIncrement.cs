namespace SwapQLib
{
    public sealed class SwapQLAutoIncrement
    {
        public string table;
        public string column;

        public SwapQLAutoIncrement(string table, string column)
        {
            this.table = table;
            this.column = column;
        }
    }
}
