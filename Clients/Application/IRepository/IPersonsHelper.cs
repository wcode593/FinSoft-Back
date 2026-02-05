namespace Application.IRepository;

public interface IPersonsHelper
{
    Task<bool> CedulaExistsAsync(string cedula);
}
