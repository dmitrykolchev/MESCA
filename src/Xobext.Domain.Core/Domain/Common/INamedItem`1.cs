namespace Xobex.Domain.Common;

public interface INamedItem<TKey> : INamedItemBase
{
    new TKey Id { get; }
}
