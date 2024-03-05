using System;

namespace Repository.Entities
{
    internal class ForeingKeyAttribute : Attribute
    {
        private string v;

        public ForeingKeyAttribute(string v)
        {
            this.v = v;
        }
    }
}