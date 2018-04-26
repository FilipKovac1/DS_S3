using OSPABA;
using simulation;
using agents;
namespace instantAssistants
{
	//meta! id="86"
	public class StatLeaveAfterTerminal : Query
	{
		public StatLeaveAfterTerminal(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void Execute(MessageForm message)
		{
		}
		public new AEnv MyAgent
		{
			get
			{
				return (AEnv)base.MyAgent;
			}
		}
	}
}