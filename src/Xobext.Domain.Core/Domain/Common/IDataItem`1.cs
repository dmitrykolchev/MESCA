using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xobex.Domain.Common;

public interface IDataItem<TKey>: IDataItemBase
{
    new TKey Id { get; }
}
