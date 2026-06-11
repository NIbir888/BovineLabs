// <copyright file="SetDirection.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Nodes.Execution
{
    using BovineLabs.Grove.Attributes;
    using BovineLabs.Grove.Core;
    using BovineLabs.Grove.Sample.Data.Core;
    using Unity.Mathematics;

    public static class ExecuteSetDirection
    {
        [ExecuteNode((int)ExecutionType.SetDirection)]
        public static void Execute(in GroveContext groveContext, ref MyContext context)
        {
            var state = context.GetState(groveContext);
            ref var direction = ref state.GetOrAddRefUnsafe(GraphStateUtil.GetKey(groveContext, (ulong)StateKeys.Direction), math.forward());
            direction = -direction;

            state.AddOrSet(GraphStateUtil.GetKey(groveContext, (ulong)StateKeys.LastDirectionChange), groveContext.ElapsedTime);
        }
    }
}
