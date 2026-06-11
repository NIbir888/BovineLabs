// <copyright file="ExecuteDebugLogData.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Data.Nodes.Execution
{
    using BovineLabs.Grove.Core;
    using BovineLabs.Grove.Sample.Data.Core;
    using Unity.Entities;

    public struct ExecuteDebugLogData
    {
        internal DebugLogKind Kind;
        internal BlobPtr<ExecutionHeader> Next;
    }
}
