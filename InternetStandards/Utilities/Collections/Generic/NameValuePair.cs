namespace InternetStandards.Utilities.Collections.Generic
{
    public struct NameValuePair<TValue>
    {
        public NameValuePair(string name, TValue value)
        {
            Name = name;
            Value = value;
        }

        public string Name
        {
            get;
        }

        public TValue Value
        {
            get;
        }
    }
}
