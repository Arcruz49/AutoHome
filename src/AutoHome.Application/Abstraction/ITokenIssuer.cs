namespace AutoHome.Application.Abstractions;
public interface ITokenIssuer
{
    string Issue(Guid userId, string email, string name);
}