﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Core.FileSystemGlobbing.Internal
{
    //
    // Resumen:
    //     This API supports infrastructure and is not intended to be used directly from
    //     your code. This API may change or be removed in future releases.
    public interface IPattern
    {
        IPatternContext CreatePatternContextForInclude();

        IPatternContext CreatePatternContextForExclude();
    }
}
