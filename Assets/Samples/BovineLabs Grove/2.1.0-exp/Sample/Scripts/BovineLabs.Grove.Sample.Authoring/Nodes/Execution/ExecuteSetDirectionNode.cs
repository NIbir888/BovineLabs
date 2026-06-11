// <copyright file="SetDirectionNode.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Authoring.Nodes.Execution
{
    using BovineLabs.Grove.Authoring.Nodes;
    using BovineLabs.Grove.Sample.Data.Core;

    public sealed class ExecuteSetDirectionAuth : GroveExecutionEmptyAuth
    {
        /// <inheritdoc />
        public override int NodeType => (int)ExecutionType.SetDirection;
    }
}
