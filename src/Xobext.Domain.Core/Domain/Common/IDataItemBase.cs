namespace Xobex.Domain.Common;

public interface IDataItemBase
{
    public object Id { get; }

    public short State { get; }

    public string Name { get; }
}
