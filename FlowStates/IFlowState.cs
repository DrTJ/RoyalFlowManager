using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalFlowManager.FlowStates
{
    public interface IFlowState
    {
        Task CommitFlow();
        void ClearFlowState();
    }
}
