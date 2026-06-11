// <copyright file="ExecuteDebugLogNode.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Authoring.Nodes.Execution
{
    using BovineLabs.Grove.Authoring;
    using BovineLabs.Grove.Authoring.Nodes;
    using BovineLabs.Grove.Sample.Data.Core;
    using BovineLabs.Grove.Sample.Data.Nodes.Execution;
    using Unity.Entities;

    public sealed class ExecuteDebugLogAuth : GroveExecutionAuth<ExecuteDebugLogData>
    {
        public DebugLogKind Kind;
        public GroveExecutionAuth Next;

        /// <inheritdoc />
        public override int NodeType => (int)ExecutionType.DebugLog;

        /// <inheritdoc/>
        protected override void Init(ref BlobBuilder builder, ref ExecuteDebugLogData execution, IGroveAuthState state)
        {
            execution.Kind = this.Kind;
            AuthUtil.AllocateExecution(ref builder, ref execution.Next, state, this.Next);
        }
    }
}
