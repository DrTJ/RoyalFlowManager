using System.Threading.Tasks;

namespace RoyalFlowManager.FlowStates
{
    public abstract class FlowStateBase : IFlowState
    {
        #region Methods

        public abstract void ClearFlowState();

        public abstract Task CommitFlow();

        #endregion
    }
}
