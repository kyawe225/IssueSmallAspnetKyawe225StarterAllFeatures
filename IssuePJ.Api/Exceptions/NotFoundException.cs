using System;
using Microsoft.AspNetCore.Diagnostics;

namespace IssuePJ.Api.Exceptions;

public class NotFoundException : System.Exception
{
    public string Type { get; }
    public NotFoundException()
    {

    }
    public NotFoundException(string message, string type) : base(message)
    {
        Type = type;
    }
}
