using OSPABA;
using simulation;
using agents;
namespace instantAssistants
{
	//meta! id="36"
	public class SetActualPosition : Action
	{
		public SetActualPosition(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void Execute(MessageForm message)
		{
		}
		public new AMinibus MyAgent
		{
			get
			{
				return (AMinibus)base.MyAgent;
			}
		}
	}
}