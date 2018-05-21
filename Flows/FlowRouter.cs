using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RoyalFlowManager.Flows
{
    public class FlowRouter
    {
        #region Fields

        private static FlowRouter router;

        private Dictionary<Type, Action> routesDictionary = new Dictionary<Type, Action>();
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

        public FlowRouter After<T>(Action actionToExecute) where T : Page
        {
            routesDictionary.Add(typeof(T), actionToExecute);
            return this;
        }

        public FlowRouter After<T1, T2>(Action actionToExecute) where T1 : Page where T2 : Page 
        {
            routesDictionary.Add(typeof(T1), actionToExecute);
            routesDictionary.Add(typeof(T2), actionToExecute);

            return this;
        }

        public FlowRouter After<T1, T2, T3>(Action actionToExecute) where T1 : Page where T2 : Page where T3 : Page
        {
            routesDictionary.Add(typeof(T1), actionToExecute);
            routesDictionary.Add(typeof(T2), actionToExecute);
            routesDictionary.Add(typeof(T3), actionToExecute);

            return this;
        }

        public FlowRouter After<T1, T2, T3, T4>(Action actionToExecute) where T1 : Page where T2 : Page where T3 : Page where T4 : Page
        {
            routesDictionary.Add(typeof(T1), actionToExecute);
            routesDictionary.Add(typeof(T2), actionToExecute);
            routesDictionary.Add(typeof(T3), actionToExecute);
            routesDictionary.Add(typeof(T4), actionToExecute);

            return this;
        }

        public void Next(Page currentPage)
        {
            if (currentPage == null || !routesDictionary.ContainsKey(currentPage.GetType()))
                return;

            routesDictionary[currentPage.GetType()]?.Invoke();
        }

        #endregion
    }
}
