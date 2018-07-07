using RoyalFlowManager.FlowStates;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace RoyalFlowManager.Flows
{
    public class FlowRouter
    {
        #region Fields

        private static FlowRouter router;

        private Dictionary<Type, Action> pageRoutesDictionary = new Dictionary<Type, Action>();
        private Dictionary<Type, Action<IFlowState>> flowRoutesDictionary = new Dictionary<Type, Action<IFlowState>>();
        private Action initializer;

        #endregion

        #region Properties

        public static FlowRouter Router => router = router ?? new FlowRouter();

        #endregion

        #region Methods

        public void InitializeFlow()
        {
            initializer?.Invoke();
        }

        public FlowRouter OnInitialize(Action actionToExecute)
        {
            initializer = actionToExecute;
            return this;
        }

        public FlowRouter AfterFlow<T>(Action actionToExecute) where T : IFlow
        {
            flowRoutesDictionary.Add(typeof(T), flowState => actionToExecute?.Invoke());
            return this;
        }

        public FlowRouter AfterFlow<T>(Action<IFlowState> actionToExecute) where T : IFlow
        {
            flowRoutesDictionary.Add(typeof(T), actionToExecute);
            return this;
        }

        public FlowRouter After<T>(Action actionToExecute) where T : Page
        {
            pageRoutesDictionary.Add(typeof(T), actionToExecute);
            return this;
        }

        public FlowRouter After<T1, T2>(Action actionToExecute) where T1 : Page where T2 : Page 
        {
            pageRoutesDictionary.Add(typeof(T1), actionToExecute);
            pageRoutesDictionary.Add(typeof(T2), actionToExecute);

            return this;
        }

        public FlowRouter After<T1, T2, T3>(Action actionToExecute) where T1 : Page where T2 : Page where T3 : Page
        {
            pageRoutesDictionary.Add(typeof(T1), actionToExecute);
            pageRoutesDictionary.Add(typeof(T2), actionToExecute);
            pageRoutesDictionary.Add(typeof(T3), actionToExecute);

            return this;
        }

        public FlowRouter After<T1, T2, T3, T4>(Action actionToExecute) where T1 : Page where T2 : Page where T3 : Page where T4 : Page
        {
            pageRoutesDictionary.Add(typeof(T1), actionToExecute);
            pageRoutesDictionary.Add(typeof(T2), actionToExecute);
            pageRoutesDictionary.Add(typeof(T3), actionToExecute);
            pageRoutesDictionary.Add(typeof(T4), actionToExecute);

            return this;
        }

        public void Next(Page currentPage)
        {
            if (currentPage == null || !pageRoutesDictionary.ContainsKey(currentPage.GetType()))
                return;

            pageRoutesDictionary[currentPage.GetType()]?.Invoke();
        }

        public void OnFlowFinished(IFlow flow)
        {
            if (flow == null || !flowRoutesDictionary.ContainsKey(flow.GetType()))
                return;

            flowRoutesDictionary[flow.GetType()]?.Invoke(flow.FlowState);
        }
        #endregion
    }
}
