﻿namespace QuickHire.Domain.Shared.Exceptions;

public class ConflictException : BaseException
{
    public override int StatusCode => 409;
    public ConflictException(string message, string? details = null) : base(message, details)
    {
    }
}
