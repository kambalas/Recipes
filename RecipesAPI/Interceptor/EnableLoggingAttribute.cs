using System;

namespace RecipesAPI.Interceptor;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class EnableLoggingAttribute : Attribute
{
    public EnableLoggingAttribute()
    {
    }
}

