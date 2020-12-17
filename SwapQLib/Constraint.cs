namespace SwapQLib
{
    public abstract class SwapQLConstraint
    {
        public string table;
        public string column;

        public SwapQLConstraint(string table, string column)
        {
            this.table = table;
            this.column = column;
        }
    }

    public class SwapQLPrimaryKeyConstraint : SwapQLConstraint
    {
        public SwapQLPrimaryKeyConstraint(string table, string column) : base(table, column) { }
    }

    public class SwapQLUniqueConstraint : SwapQLConstraint
    {
        public SwapQLUniqueConstraint(string table, string column) : base(table, column) { }
    }

    public class SwapQLNullConstraint : SwapQLConstraint
    {
        public SwapQLNullConstraint(string table, string column) : base(table, column) { }
    }

    public class SwapQLCheckConstraint : SwapQLConstraint
    {
        public string check;

        public SwapQLCheckConstraint(string table, string column, string check) : base(table, column)
        {
            this.check = check;
        }
    }
}
