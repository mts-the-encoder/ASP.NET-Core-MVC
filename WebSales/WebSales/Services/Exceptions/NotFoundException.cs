﻿using System;

namespace WebSales.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string msg) : base(msg)
        {
        }
    }
}
