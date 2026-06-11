// <copyright file="ExecuteMoveNode.cs" company="BovineLabs">
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
    [UseWithGraph(typeof(MyGraphGraph))]
    public sealed class ExecuteMoveNode : GroveExecutionNode<ExecuteMoveAuth, ExecuteMoveData>
    {
        private const string SpeedName = "Speed";
        private const float DefaultSpeed = 1f;

        /// <inheritdoc/>
        protected override void DefineOptions(IOptionDefinitionContext context)
        {
            context.AddOption<float>(SpeedName).ShowInInspectorOnly().WithDisplayName("Speed").WithDefaultValue(DefaultSpeed).Delayed().Build();
        }

        /// <inheritdoc/>
        protected override void Init(ExecuteMoveAuth auth, IGroveNodeState state)
        {
            auth.Speed = this.GetSpeed();
        }

        private float GetSpeed()
        {
            this.GetNodeOptionByName(SpeedName).TryGetValue(out float speed);
            return speed;
        }
    }
}
