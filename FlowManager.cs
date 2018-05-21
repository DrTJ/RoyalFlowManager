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
        private static Queue<IFlow> flowsQueue = new Queue<IFlow>();

        public static IFlow CurrentFlow => flowsQueue.Count == 0 ? null : flowsQueue.Peek();

        public static void StartFlow(IFlow flow, Action<IFlow> flowInitializedAction = null, bool finishCurrentFlow = false)
        {
            CurrentFlow?.OnDeactivated();
            if(finishCurrentFlow)
            {
                CurrentFlow?.OnFinished();
                flowsQueue.Dequeue();
            }

            flowsQueue.Enqueue(flow);
            flow.OnStarted();
            
            flowInitializedAction?.Invoke(flow);
        }

        public static void FinishCurrentFlow()
        {
            CurrentFlow?.OnFinished();
            flowsQueue.Dequeue();
            CurrentFlow?.OnReactivated();
        }
    }
}
