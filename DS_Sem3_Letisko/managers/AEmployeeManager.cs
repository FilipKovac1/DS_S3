using OSPABA;
using simulation;
using agents;
using Actors;

namespace managers
{
	//meta! id="5"
	public class AEmployeeManager : Manager
	{
		public AEmployeeManager(int id, Simulation mySim, Agent myAgent) :
			base(id, mySim, myAgent)
		{
			Init();
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication

			if (PetriNet != null)
			{
				PetriNet.Clear();
			}
		}

		//meta! sender="AAirport", id="26", type="Notice"
		public void ProcessResetStat(MessageForm message)
		{
            MyAgent.ResetStats();
		}

		//meta! sender="AAirport", id="23", type="Request"
		public void ProcessServePassenger(MessageForm message)
		{
            if (((MyMessage)message).Passenger != null) // if there is someone
            {
                if (((MyMessage)message).Passenger.ArrivedAt < 3)
                    ((MyMessage)message).Passenger.ArrivalTimeCR = MySim.CurrentTime;

                Employee e = MyAgent.GetFirstFree();
                if (e != null)
                { // check if there is free prov
                    ((MyMessage)message).Employee = e;
                    message.Addressee = MyAgent.FindAssistant(SimId.ServicePassenger);
                    StartContinualAssistant(message);
                }
                else
                {
                    message.Addressee = MyAgent.FindAssistant(SimId.AddToQ);
                    Execute(message);
                }
            }
        }

		//meta! sender="ServicePassenger", id="52", type="Finish"
		public void ProcessFinish(MessageForm message)
        {
            ((MyMessage)message).Employee.Free = true;

            MessageForm m = message.CreateCopy();
            ((MyMessage)m).Passenger = MyAgent.GetFromQueue();
            if (((MyMessage)m).Passenger != null) // if not added to message get first from queue
                ProcessServePassenger(m);

            message.Code = Mc.ServePassenger;
            message.Addressee = MySim.FindAgent(SimId.AAirport);
            Response(message);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
                default:
                    break;
			}
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		public void Init()
		{
		}

		public override void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.ResetStat:
				ProcessResetStat(message);
			break;

			case Mc.ServePassenger:
				ProcessServePassenger(message);
			break;

			case Mc.Finish:
				ProcessFinish(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AEmployee MyAgent
		{
			get
			{
				return (AEmployee)base.MyAgent;
			}
		}
	}
}