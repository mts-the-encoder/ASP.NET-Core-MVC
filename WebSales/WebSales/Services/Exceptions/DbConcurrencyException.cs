﻿using System;

namespace WebSales.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException
    {
        public DbConcurrencyException(string msg) : base(msg)
        {
        }
    }
}
