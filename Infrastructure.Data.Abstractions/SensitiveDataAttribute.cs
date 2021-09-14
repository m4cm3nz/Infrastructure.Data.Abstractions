using System;

namespace Infrastructure.Data.Abstractions
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SensitiveDataAttribute : Attribute { }
}
