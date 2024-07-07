namespace IHolder.Application.Common.Auth;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AuthorizationAttribute : Attribute
{
    public string? Permissions { get; set; }
    public string? Roles { get; set; }
}