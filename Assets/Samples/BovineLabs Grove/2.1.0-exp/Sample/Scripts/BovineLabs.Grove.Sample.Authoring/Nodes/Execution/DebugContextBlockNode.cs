// <copyright file="DebugContextBlockNode.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Authoring.Nodes.Execution
{
    using BovineLabs.Grove.Authoring;
    using BovineLabs.Grove.Authoring.Nodes;
    using BovineLabs.Grove.Sample.Data.Core;
    using BovineLabs.Grove.Sample.Data.Nodes.Execution;
    using Unity.Entities;

    public sealed class DebugContextBlockAuth : GroveBlockAuth<DebugContextBlockData, int>
    {
        public int Value = 1;

        /// <inheritdoc />
        public override int NodeType => (int)ExecutionType.DebugContextBlock;

        /// <inheritdoc/>
        protected override void Init(ref BlobBuilder builder, ref DebugContextBlockData processor, IGroveAuthState state)
        {
            processor.Value = this.Value;
        }
    }
}
