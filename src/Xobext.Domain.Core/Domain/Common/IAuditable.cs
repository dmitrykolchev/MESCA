namespace Xobex.Domain.Common;

public interface IAuditable
{
    public DateTimeOffset CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    public DateTimeOffset ModifiedOn { get; set; }

    public int ModifiedBy { get; set; }
}

public interface ISimpleAuditable
{
    public DateTimeOffset ModifiedOn { get; set; }

    public int ModifiedBy { get; set; }
}
