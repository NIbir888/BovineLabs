// <copyright file="DebugContext.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Nodes.Execution
{
    using BovineLabs.Core;
    using BovineLabs.Grove.Attributes;
    using BovineLabs.Grove.Core;
    using BovineLabs.Grove.Sample.Data.Core;
    using BovineLabs.Grove.Sample.Data.Nodes.Execution;

    public static class DebugContext
    {
        [ExecuteNode((int)ExecutionType.DebugContext)]
        public static void Execute(ref DebugContextData data, in GroveContext groveContext, ref MyContext context)
        {
            BLGlobalLogger.LogInfoString("Grove Sample: context node ran.");

            var value = 0;
            for (var i = 0; i < data.Blocks.Length; i++)
            {
                data.Blocks[i].Value.Execute(groveContext, ref context, ref value);
            }

            data.Next.Value.Execute(groveContext, ref context);
        }
    }
}
