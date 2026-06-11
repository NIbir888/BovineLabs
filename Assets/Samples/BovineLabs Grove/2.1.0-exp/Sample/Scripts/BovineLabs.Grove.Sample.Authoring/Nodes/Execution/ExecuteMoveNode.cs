// <copyright file="MoveNode.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Authoring.Nodes.Execution
{
    using BovineLabs.Grove.Authoring;
    using BovineLabs.Grove.Authoring.Nodes;
    using BovineLabs.Grove.Sample.Data.Core;
    using BovineLabs.Grove.Sample.Data.Nodes.Execution;
    using Unity.Entities;

    public sealed class ExecuteMoveAuth : GroveExecutionAuth<ExecuteMoveData>
    {
        public float Speed = 1;

        /// <inheritdoc />
        public override int NodeType => (int)ExecutionType.Move;

        /// <inheritdoc/>
        protected override void Init(ref BlobBuilder builder, ref ExecuteMoveData execution, IGroveAuthState state)
        {
            execution.Speed = this.Speed;
        }
    }
}
