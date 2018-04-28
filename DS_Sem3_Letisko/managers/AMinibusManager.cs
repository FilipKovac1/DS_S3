using OSPABA;
using simulation;
using agents;
using continualAssistants;
using instantAssistants;
using System.Collections.Generic;
using Actors;
using Statistics;

namespace managers
{
	//meta! id="4"
	public class AMinibusManager : Manager
	{

        public AMinibusManager(int id, Simulation mySim, Agent myAgent) : base(id, mySim, myAgent)
		{
			Init();
        }

		override public void PrepareReplication()
		{
			base.PrepareReplication();
            // Setup component for the next replication

            if (PetriNet != null)
			{
				PetriNet.Clear();
			}
		}

		//meta! sender="AAirport", id="25", type="Notice"
		public void ProcessResetStat(MessageForm message)
		{ // reset after heat up 
            foreach (Minibus m in MyAgent.Minis) // just reset mileage to compute costs
                m.MileAge = 0;

            MyAgent.ResetStats();
		}

		/*!
		 * move of bus
		 */
		//meta! sender="AAirport", id="21", type="Request"
		public void ProcessMove(MessageForm message)
		{
            message.Addressee = MyAgent.FindAssistant(SimId.Transport);
            message.Code = Mc.Start;
            StartContinualAssistant(message);
		}

		/*!
		 * Request - Enter to front to wait for a bus
		 * Response - leave a bus
		 */
		//meta! sender="AAirport", id="58", type="Request"
		public void ProcessProcessPassenger(MessageForm message)
		{
            message.Addressee = MyAgent.FindAssistant(SimId.AddToFront);
            Execute(message);
		}

		//meta! sender="Transport", id="109", type="Finish"
		public void ProcessFinishTransport(MessageForm message)
		{
            // minibus arrived
            message.Code = Mc.Start;
            ((MyMessage)message).Minibus.OnWay = false;
            switch (((MyMessage)message).Minibus.State)
            {
                case 1: // get in
                    if (MyAgent.IsEmpty(((MyMessage)message).Minibus.State))
                    {
                        ProcessFinishGetIn(message);
                        break;
                    }
                    message.Addressee = MyAgent.FindAssistant(SimId.GetIn);
                    StartContinualAssistant(message);
                    break;
                case 2:
                    if (((MyMessage)message).Minibus.IsFull() || MyAgent.IsEmpty(((MyMessage)message).Minibus.State))
                    {
                        ProcessFinishGetIn(message);
                        break;
                    }
                    message.Addressee = MyAgent.FindAssistant(SimId.GetIn);
                    StartContinualAssistant(message);
                    break;
                case 3: // here is stil someone
                    message.Addressee = MyAgent.FindAssistant(SimId.GetOut);
                    StartContinualAssistant(message);
                    break;
                case 4:
                    if (((MyMessage)message).Minibus.IsEmpty())
                    {
                        ProcessFinishGetOut(message);
                        break;
                    }
                    message.Addressee = MyAgent.FindAssistant(SimId.GetOut);
                    StartContinualAssistant(message);
                    break;
            }
        }

		//meta! sender="GetIn", id="39", type="Finish"
		public void ProcessFinishGetIn(MessageForm message)
		{
            message.Code = Mc.Move;
            message.Addressee = MySim.FindAgent(SimId.AAirport);
            Response(message);
        }

        //meta! sender="GetOut", id="41", type="Finish"
        public void ProcessFinishGetOut(MessageForm message) => ProcessFinishGetIn(message);

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
			}
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		public void Init()
		{
		}

		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.Move:
				ProcessMove(message);
			break;

			case Mc.Finish:
				switch (message.Sender.Id)
				{
				case SimId.GetOut:
					ProcessFinishGetOut(message);
				break;

				case SimId.Transport:
					ProcessFinishTransport(message);
				break;

				case SimId.GetIn:
					ProcessFinishGetIn(message);
				break;
				}
			break;

			case Mc.ResetStat:
				ProcessResetStat(message);
			break;

			case Mc.ProcessPassenger:
				ProcessProcessPassenger(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AMinibus MyAgent
		{
			get
			{
				return (AMinibus)base.MyAgent;
			}
		}
	}
}
