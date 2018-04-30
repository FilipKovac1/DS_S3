using OSPABA;
using simulation;
using agents;
namespace instantAssistants
{
	//meta! id="45"
	public class AddToFront : Action
	{
        public AddToFront(int id, Simulation mySim, CommonAgent myAgent) : base(id, mySim, myAgent) { }

        public override void Execute(MessageForm message) => MyAgent.AddToQueue(((MyMessage)message).Passenger);

        public new AMinibus MyAgent
		{
			get
			{
				return (AMinibus)base.MyAgent;
			}
		}
	}
}