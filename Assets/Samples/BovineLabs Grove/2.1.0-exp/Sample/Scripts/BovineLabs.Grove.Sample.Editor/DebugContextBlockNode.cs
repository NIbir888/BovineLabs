// <copyright file="DebugContextBlockNode.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Editor
{
    using System;
    using BovineLabs.Grove.Editor;
    using BovineLabs.Grove.Sample.Authoring.Nodes.Execution;
    using BovineLabs.Grove.Sample.Data.Nodes.Execution;
    using Unity.GraphToolkit.Editor;

    [Serializable]
    [UseWithContext(typeof(DebugContextNode))]
    public sealed class DebugContextBlockNode : GroveBlockNode<DebugContextBlockAuth, DebugContextBlockData, int>
    {
        private const string ValueName = "Value";
        private const int DefaultValue = 1;

        /// <inheritdoc/>
        protected override void DefineOptions(IOptionDefinitionContext context)
        {
            context.AddOption<int>(ValueName).ShowInInspectorOnly().WithDisplayName("Value").WithDefaultValue(DefaultValue).Delayed().Build();
        }

        /// <inheritdoc/>
        protected override void Init(DebugContextBlockAuth auth, IGroveNodeState state)
        {
            auth.Value = this.GetValue();
        }

        private int GetValue()
        {
            this.GetNodeOptionByName(ValueName).TryGetValue(out int value);
            return value;
        }
    }
}
