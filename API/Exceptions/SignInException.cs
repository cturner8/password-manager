﻿namespace API.Exceptions;

public class SignInException : Exception
{
    public SignInException() : base("Incorrect email or password.")
    {
    }
}
