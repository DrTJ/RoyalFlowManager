using System;
using System.Collections.Generic;
using System.Linq;
using RoyalFlowManager.Flows;

namespace RoyalFlowManager
{
    public static class FlowManager
    {
        private static List<IFlow> flowsList = new List<IFlow>();

        public static IFlow CurrentFlow => flowsList.LastOrDefault();

        public static void StartFlow(IFlow flow, Action<IFlow> flowInitializingAction = null, bool finishCurrentFlow = false)
        {
            CurrentFlow?.OnDeactivated();
            if(finishCurrentFlow)
            {
                CurrentFlow?.OnFinished();
                flowsList.Remove(CurrentFlow);
            }

            flowsList.Add(flow);

			flowInitializingAction?.Invoke(flow);

            flow.OnStarted();
            flow.InitializeFlow();
        }

        public static void FinishCurrentFlow()
        {
            CurrentFlow?.OnFinished();
            flowsList.Remove(CurrentFlow);
            CurrentFlow?.OnReactivated();
        }
    }
}
