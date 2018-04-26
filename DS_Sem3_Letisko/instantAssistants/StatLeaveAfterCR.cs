using OSPABA;
using simulation;
using agents;
namespace instantAssistants
{
	//meta! id="84"
	public class StatLeaveAfterCR : Query
	{
		public StatLeaveAfterCR(int id, Simulation mySim, CommonAgent myAgent) :
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