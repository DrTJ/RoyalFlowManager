using System;
using RoyalFlowManager.FlowStates;
using Xamarin.Forms;

namespace RoyalFlowManager.Flows
{
    public abstract class FlowBase : IFlow
    {
        #region Events

        public EventHandler<FlowStatusChangedEventArgs> OnFlowStatusChanged;

        #endregion

        #region Fields

        protected FlowRouter flowRouter;

        #endregion

        #region Constructors

        public FlowBase()
        {
            flowRouter = new FlowRouter();
        }
        
        #endregion

        #region Properties

        public IFlowState FlowState { get; set; }

        public FlowStatus CurrentStatus { get; set; }

        #endregion

        #region Methods

        public void Next(Page current)
        {
            flowRouter.Next(current);
            OnFlowStatusChanged?.Invoke(this, new FlowStatusChangedEventArgs() { Status = FlowStatus.Running });
        }

        public void InitializeFlow()
        {
            flowRouter.InitializeFlow();
            OnFlowStatusChanged?.Invoke(this, new FlowStatusChangedEventArgs() { Status = FlowStatus.Initialized });
        }

        public T GetFlowState<T>()
        {
            return FlowState is T ? (T)FlowState : (T)new object();
        }

        public virtual void OnStarted()
        {
            OnFlowStatusChanged?.Invoke(this, new FlowStatusChangedEventArgs() { Status = FlowStatus.Started });
        }

        public virtual void OnDeactivated()
        {
            OnFlowStatusChanged?.Invoke(this, new FlowStatusChangedEventArgs() { Status = FlowStatus.Deactivated });
        }

        public virtual void OnReactivated()
        {
            OnFlowStatusChanged?.Invoke(this, new FlowStatusChangedEventArgs() { Status = FlowStatus.Reactivated });
        }

        public virtual void OnFinished()
        {
            OnFlowStatusChanged?.Invoke(this, new FlowStatusChangedEventArgs() { Status = FlowStatus.Finished });
        }

        #endregion
    }

    public class FlowStatusChangedEventArgs : EventArgs
    {
        public FlowStatus Status { get; set; }
    }

    public enum FlowStatus
    {
        Initialized,
        Started,
        Deactivated,
        Reactivated, 
        Finished,
        Running
    }

}
