using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xobex.Domain.Common;
public interface INamedItemBase
{
    object Id { get; }

    string Name { get; }
}
