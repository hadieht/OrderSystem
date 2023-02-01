namespace Application.Common.Interfaces;

public interface IDatabaseTransaction : IDisposable
{
    Task CommitAsync();
}
