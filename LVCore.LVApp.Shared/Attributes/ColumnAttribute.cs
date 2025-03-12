using System;

namespace LVCore.LVApp.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class ColumnAttribute : Attribute
    {
        public string Name { get; }
        public bool IsPrimaryKey { get; }

        public ColumnAttribute(string name, bool isPrimaryKey = false)
        {
            Name = name;
            IsPrimaryKey = isPrimaryKey;
        }
    }
}
