// <copyright file="DebugContextNode.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Authoring.Nodes.Execution
{
    using BovineLabs.Grove.Authoring;
    using BovineLabs.Grove.Authoring.Nodes;
    using BovineLabs.Grove.Sample.Data.Core;
    using BovineLabs.Grove.Sample.Data.Nodes.Execution;
    using Unity.Entities;

    public sealed class DebugContextAuth : GroveContextAuth<DebugContextData, int>
    {
        /// <inheritdoc />
        public override int NodeType => (int)ExecutionType.DebugContext;

        public GroveExecutionAuth Next;

        /// <inheritdoc/>
        protected override void Init(ref BlobBuilder builder, ref DebugContextData execution, IGroveAuthState state)
        {
            AuthUtil.AllocateExecutionNodes(ref builder, ref execution.Blocks, state, this.Blocks);
            AuthUtil.AllocateExecution(ref builder, ref execution.Next, state, this.Next);
        }
    }
}
