namespace Xobex.Domain.Common;

public interface IAuditable
{
    DateTimeOffset CreatedOn { get; set; }

    int CreatedBy { get; set; }

    DateTimeOffset ModifiedOn { get; set; }

    int ModifiedBy { get; set; }
}

public interface ISimpleAuditable
{
    DateTimeOffset ModifiedOn { get; set; }

    int ModifiedBy { get; set; }
}
