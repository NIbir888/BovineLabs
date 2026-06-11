// <copyright file="DebugContextBlock.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Nodes.Execution
{
    using BovineLabs.Core;
    using BovineLabs.Grove.Attributes;
    using BovineLabs.Grove.Core;
    using BovineLabs.Grove.Sample.Data.Core;
    using BovineLabs.Grove.Sample.Data.Nodes.Execution;

    public static class DebugContextBlock
    {
        [ExecuteNode((int)ExecutionType.DebugContextBlock, typeof(int))]
        public static void Execute(ref DebugContextBlockData data, ref int input)
        {
            input += data.Value;
            BLGlobalLogger.LogInfoString($"Grove Sample: context block ran. Added={data.Value}, Invocation={input}");
        }
    }
}
