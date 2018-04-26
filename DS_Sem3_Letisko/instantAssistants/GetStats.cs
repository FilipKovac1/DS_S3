using OSPABA;
using simulation;
using agents;
namespace instantAssistants
{
	//meta! id="66"
	public class GetStats : Query
	{
		public GetStats(int id, Simulation mySim, CommonAgent myAgent) :
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