using OSPABA;
using simulation;
using agents;
namespace instantAssistants
{
	//meta! id="68"
	public class AddToQ : Action
	{
		public AddToQ(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void Execute(MessageForm message)
		{
		}
		public new AEmployee MyAgent
		{
			get
			{
				return (AEmployee)base.MyAgent;
			}
		}
	}
}