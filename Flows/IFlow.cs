using RoyalFlowManager.FlowStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RoyalFlowManager.Flows
{
    public interface IFlow
    {
        #region Properties

        IFlowState FlowState { get; set; }

        #endregion

        #region Methods

        void Next(Page current);

        T GetFlowState<T>();

        void OnStarted();

        void OnDeactivated();

        void OnReactivated();

        void OnFinished();

        #endregion
    }
}
