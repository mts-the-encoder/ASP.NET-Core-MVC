using System;

namespace WebSales.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string msg) : base(msg)
        {
        }
    }
}
