using RoyalFlowManager.Flows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalFlowManager.FlowStates
{
    public abstract class FlowStateBase : IFlowState
    {
        #region Fields

        #endregion

        #region Constructors

        #endregion

        #region Properties

        #endregion

        #region Methods

        public abstract void ClearFlowState();

        public abstract Task CommitFlow();

        #endregion
    }
}
