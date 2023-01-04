﻿using Nt.Core.FileSystemGlobbing.Abstractions;
using System;

namespace Nt.Core.FileSystemGlobbing.Internal
{
    /// <summary>
    /// This API supports infrastructure and is not intended to be used directly from
    /// your code. This API may change or be removed in future releases.
    /// </summary>
    public interface IPatternContext
    {
        void Declare(Action<IPathSegment, bool> onDeclare);

        bool Test(DirectoryInfoBase directory);

        PatternTestResult Test(FileInfoBase file);

        void PushDirectory(DirectoryInfoBase directory);

        void PopDirectory();
    }
}
