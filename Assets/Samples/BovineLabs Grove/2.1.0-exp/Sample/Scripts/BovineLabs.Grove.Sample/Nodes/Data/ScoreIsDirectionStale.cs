// <copyright file="ScoreIsDirectionStale.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Nodes.Data
{
    using BovineLabs.Grove.Attributes;
    using BovineLabs.Grove.Core;
    using BovineLabs.Grove.Sample.Data.Core;
    using BovineLabs.Grove.Sample.Data.Nodes.Data;

    public static class ScoreIsDirectionStale
    {
        [DataNode((int)DataType.IsDirectionStale)]
        public static float Calculate(in ScoreIsDirectionStaleData data, in GroveContext groveContext, ref MyContext context)
        {
            var state = context.GetState(groveContext);
            ref var lastDirectionChange = ref state.GetOrAddRefUnsafe(GraphStateUtil.GetKey(groveContext, (ulong)StateKeys.LastDirectionChange), double.MinValue);
            var isDirectionStale = lastDirectionChange + data.Duration <= groveContext.ElapsedTime;
            return data.Score.Score(isDirectionStale);
        }
    }
}

