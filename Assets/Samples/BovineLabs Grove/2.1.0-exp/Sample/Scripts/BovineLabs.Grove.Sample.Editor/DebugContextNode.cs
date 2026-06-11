// <copyright file="DebugContextNode.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Editor
{
    using System;
    using BovineLabs.Grove.Authoring.Nodes;
    using BovineLabs.Grove.Editor;
    using BovineLabs.Grove.Sample.Authoring.Nodes.Execution;
    using BovineLabs.Grove.Sample.Data.Nodes.Execution;
    using Unity.GraphToolkit.Editor;

    [Serializable]
    [UseWithGraph(typeof(MyGraphGraph))]
    public sealed class DebugContextNode : GroveContextNode<DebugContextAuth, DebugContextData, int>
    {
        private const string OutputName = "Output";

        /// <inheritdoc/>
        protected override void Init(DebugContextAuth auth, IGroveNodeState state)
        {
            auth.Next = state.ResolveNode<GroveExecutionAuth>(this.GetOutputPortByName(OutputName));
        }

        /// <inheritdoc/>
        protected override void DefinePorts(IPortDefinitionContext context)
        {
            base.DefinePorts(context);
            context.AddOutputPort(OutputName).AsVertical().Build();
        }
    }
}
