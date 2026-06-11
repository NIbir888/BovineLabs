// <copyright file="DebugLogKind.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Data.Core
{
    public enum DebugLogKind
    {
        Default = 0,
        ContextContinuation = 1,
        BeforeSubgraph = 2,
        InsideSubgraph = 3,
        StateEnter = 4,
        StateExit = 5,
    }
}
