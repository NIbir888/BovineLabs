// <copyright file="DebugContextData.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Data.Nodes.Execution
{
    using BovineLabs.Grove.Core;
    using Unity.Entities;

    public struct DebugContextData
    {
        internal BlobArray<BlobPtr<ExecutionHeader<int>>> Blocks;
        internal BlobPtr<ExecutionHeader> Next;
    }
}
