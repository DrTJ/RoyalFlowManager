﻿using System;
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

        public T GetFlowState<T>() where T : class
        {
            if ((FlowState as T) == null)
            {
                throw new Exception($"Cannot find {typeof(T).Name} in {GetType().Name}.");
            }

            return (T)FlowState;
        }

        public bool Implements<TQuery>() where TQuery : class
        {
            return FlowState is TQuery;
        }

        public virtual void OnStarted()
        {
            OnFlowStatusChanged?.Invoke(this, new FlowStatusChangedEventArgs() { Status = FlowStatus.Started });
        }

        public virtual void OnDeactivated()
        {
            OnFlowStatusChanged?.Invoke(this, new FlowStatusChangedEventArgs() { Status = FlowStatus.Deactivated });
        }

        public virtual void OnReactivated(IFlow previousFlow)
        {
            OnFlowStatusChanged?.Invoke(this,
                                        new FlowStatusChangedEventArgs()
                                        {
                                            Status = previousFlow == null ? FlowStatus.ReactivatedFromCanceledFlow : FlowStatus.ReactivatedFromFinishedFlow
                                        });

            if(previousFlow != null)
            {
				flowRouter.OnFlowFinished(previousFlow);
            }
        }

        public virtual void OnFinished()
        {
            OnFlowStatusChanged?.Invoke(this, new FlowStatusChangedEventArgs() { Status = FlowStatus.Finished });
        }

        public virtual void OnCanceled()
        {
            OnFlowStatusChanged?.Invoke(this, new FlowStatusChangedEventArgs() { Status = FlowStatus.Canceled });
        }

        #endregion
    }

    public class FlowStatusChangedEventArgs : EventArgs
    {
        public FlowStatus Status { get; set; }

        public static FlowStatusChangedEventArgs ReactivatedFromCanceledFlowEventArgs = new FlowStatusChangedEventArgs() { Status = FlowStatus.ReactivatedFromCanceledFlow };
        public static FlowStatusChangedEventArgs ReactivatedFromFinishedFlowEventArgs = new FlowStatusChangedEventArgs() { Status = FlowStatus.ReactivatedFromFinishedFlow };
    }

    public enum FlowStatus
    {
        Initialized,
        Started,
        Deactivated,
        ReactivatedFromCanceledFlow, 
        ReactivatedFromFinishedFlow, 
        Finished,
        Canceled,
        Running
    }

}
