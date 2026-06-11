// <copyright file="ExecuteDebugLogNode.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Editor
{
    using System;
    using BovineLabs.Grove.Authoring.Nodes;
    using BovineLabs.Grove.Editor;
    using BovineLabs.Grove.Sample.Authoring.Nodes.Execution;
    using BovineLabs.Grove.Sample.Data.Core;
    using BovineLabs.Grove.Sample.Data.Nodes.Execution;
    using Unity.GraphToolkit.Editor;

    [Serializable]
    [UseWithGraph(typeof(MyGraphGraph))]
    public sealed class ExecuteDebugLogNode : GroveExecutionNode<ExecuteDebugLogAuth, ExecuteDebugLogData>
    {
        private const string KindName = "Kind";

        /// <inheritdoc/>
        protected override void DefineOptions(IOptionDefinitionContext context)
        {
            context.AddOption<DebugLogKind>(KindName).ShowInInspectorOnly().WithDisplayName("Kind").WithDefaultValue(DebugLogKind.Default).Build();
        }

        /// <inheritdoc/>
        protected override void DefinePorts(IPortDefinitionContext context)
        {
            base.DefinePorts(context);
            context.AddOutputPort(OutputName).AsVertical().Build();
        }

        /// <inheritdoc/>
        protected override void Init(ExecuteDebugLogAuth auth, IGroveNodeState state)
        {
            auth.Kind = this.GetKind();
            auth.Next = this.GetOutputAuth(OutputName, state) as GroveExecutionAuth;
        }

        private DebugLogKind GetKind()
        {
            this.GetNodeOptionByName(KindName).TryGetValue(out DebugLogKind kind);
            return kind;
        }
    }
}
