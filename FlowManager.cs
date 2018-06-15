using System;
using System.Collections.Generic;
using System.Linq;
using RoyalFlowManager.Flows;

namespace RoyalFlowManager
{
    public static class FlowManager
    {
        #region Fields

        private static List<IFlow> flowsList = new List<IFlow>();
  
		#endregion

        #region Properties
        
        public static IFlow CurrentFlow => FlowsList.LastOrDefault();

        public static List<IFlow> FlowsList => flowsList;

        #endregion

        #region Methods

        public static void StartFlow(IFlow flow, Action<IFlow> flowInitializingAction = null, bool finishCurrentFlow = false)
        {
            CurrentFlow?.OnDeactivated();
            if(finishCurrentFlow)
            {
                CurrentFlow?.OnFinished();
                FlowsList.Remove(CurrentFlow);
            }

            FlowsList.Add(flow);

            flowInitializingAction?.Invoke(flow);

            flow.OnStarted();
            flow.InitializeFlow();
        }

        public static void FinishCurrentFlow(bool keepLastFlow = true)
        {
            var finishedFlow = CurrentFlow;
            CurrentFlow?.OnFinished();

            if (!(keepLastFlow && FlowsList.Count == 1))
            {
                FlowsList.Remove(CurrentFlow);
            }

            CurrentFlow?.OnReactivated(finishedFlow);
        }

        public static void CancelCurrentFlow()
        {
            CurrentFlow?.OnCanceled();
            FlowsList.Remove(CurrentFlow);

            CurrentFlow?.OnReactivated(null);
        }

		#endregion

    }
}
