// <copyright file="ScoreIsDirectionStaleNode.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Editor
{
    using System;
    using BovineLabs.Grove.Core;
    using BovineLabs.Grove.Editor;
    using BovineLabs.Grove.Sample.Authoring.Nodes.Data;
    using Unity.GraphToolkit.Editor;

    [Serializable]
    [UseWithGraph(typeof(MyGraphGraph))]
    public sealed class ScoreIsDirectionStaleNode : GroveDataNode<ScoreIsDirectionStaleAuth, float>
    {
        private const string DurationName = "Duration";
        private const string ScoreName = "Score";
        private const float DefaultDuration = 5f;

        /// <inheritdoc/>
        protected override void DefineOptions(IOptionDefinitionContext context)
        {
            context.AddOption<float>(DurationName).ShowInInspectorOnly().WithDisplayName("Duration").WithDefaultValue(DefaultDuration).Delayed().Build();
            context.AddOption<Scorer>(ScoreName).ShowInInspectorOnly().WithDisplayName("Score").WithDefaultValue(Scorer.Default).Build();
        }

        /// <inheritdoc/>
        protected override void DefinePorts(IPortDefinitionContext context)
        {
            context.AddOutputPort<float>(OutputName).WithDisplayName("Output").Build();
        }

        /// <inheritdoc/>
        protected override void Init(ScoreIsDirectionStaleAuth auth, IGroveNodeState state)
        {
            base.Init(auth, state);
            auth.Duration = this.GetDuration();
            auth.Score = this.GetScore();
        }

        private float GetDuration()
        {
            this.GetNodeOptionByName(DurationName).TryGetValue(out float duration);
            return duration;
        }

        private Scorer GetScore()
        {
            this.GetNodeOptionByName(ScoreName).TryGetValue(out Scorer score);
            return score;
        }
    }
}

