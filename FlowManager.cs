using RoyalFlowManager.Flows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalFlowManager
{
    public static class FlowManager
    {
        private static List<IFlow> flowsList = new List<IFlow>();

        public static IFlow CurrentFlow => flowsList.LastOrDefault();

        public static void StartFlow(IFlow flow, Action<IFlow> flowInitializedAction = null, bool finishCurrentFlow = false)
        {
            CurrentFlow?.OnDeactivated();
            if(finishCurrentFlow)
            {
                CurrentFlow?.OnFinished();
                flowsList.Remove(CurrentFlow);
            }

            flowsList.Add(flow);
            flow.OnStarted();
            
            flowInitializedAction?.Invoke(flow);
        }

        public static void FinishCurrentFlow()
        {
            CurrentFlow?.OnFinished();
            flowsList.Remove(CurrentFlow);
            CurrentFlow?.OnReactivated();
        }
    }
}
