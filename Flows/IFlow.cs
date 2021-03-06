﻿using System;
using RoyalFlowManager.FlowStates;
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

        void InitializeFlow();

        T GetFlowState<T>() where T : class;

        bool Implements<TQuery>() where TQuery : class;

        void OnStarted();

        void OnDeactivated();

        void OnReactivated(IFlow previousFlow);

        void OnFinished();

        void OnCanceled();

        #endregion
    }
}
