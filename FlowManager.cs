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
        
        public static IFlow CurrentFlow => flowsList.LastOrDefault();

        public static List<IFlow> FlowsList => flowsList;

        #endregion

        #region Methods

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
            var finishedFlow = CurrentFlow;
            CurrentFlow?.OnFinished();
            flowsList.Remove(CurrentFlow);

            CurrentFlow?.OnReactivated(finishedFlow);
        }

		#endregion

    }
}
