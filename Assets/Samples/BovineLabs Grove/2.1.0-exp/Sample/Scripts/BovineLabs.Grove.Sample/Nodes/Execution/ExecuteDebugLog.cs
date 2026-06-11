// <copyright file="ExecuteDebugLog.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.Grove.Sample.Nodes.Execution
{
    using BovineLabs.Core;
    using BovineLabs.Grove.Attributes;
    using BovineLabs.Grove.Core;
    using BovineLabs.Grove.Sample.Data.Core;
    using BovineLabs.Grove.Sample.Data.Nodes.Execution;

    public static class ExecuteDebugLog
    {
        [ExecuteNode((int)ExecutionType.DebugLog)]
        public static void Execute(ref ExecuteDebugLogData data, in GroveContext groveContext, ref MyContext context)
        {
            BLGlobalLogger.LogInfoString(GetMessage(data.Kind));
            data.Next.Value.Execute(groveContext, ref context);
        }

        private static string GetMessage(DebugLogKind kind)
        {
            return kind switch
            {
                DebugLogKind.ContextContinuation => "Grove Sample: context node continued into its next execution node.",
                DebugLogKind.BeforeSubgraph => "Grove Sample: main graph reached the subgraph boundary.",
                DebugLogKind.InsideSubgraph => "Grove Sample: subgraph execution node ran.",
                DebugLogKind.StateEnter => "Grove Sample: state enter branch ran.",
                DebugLogKind.StateExit => "Grove Sample: state exit branch ran.",
                _ => "Grove Sample: debug log node ran.",
            };
        }
    }
}
