// <copyright file="ScoreIsDirectionStaleNode.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Authoring.Nodes.Data
{
    using BovineLabs.Grove.Authoring;
    using BovineLabs.Grove.Authoring.Nodes;
    using BovineLabs.Grove.Core;
    using BovineLabs.Grove.Sample.Data.Core;
    using BovineLabs.Grove.Sample.Data.Nodes.Data;
    using Unity.Entities;

    public sealed class ScoreIsDirectionStaleAuth : GroveDataAuth<ScoreIsDirectionStaleData, float>
    {
        public float Duration = 10;
        public Scorer Score = Scorer.Default;

        /// <inheritdoc />
        public override int NodeType => (int)DataType.IsDirectionStale;

        /// <inheritdoc/>
        protected override void Init(ref BlobBuilder builder, ref ScoreIsDirectionStaleData data, IGroveAuthState state)
        {
            data.Duration = this.Duration;
            data.Score = this.Score;
        }
    }
}

