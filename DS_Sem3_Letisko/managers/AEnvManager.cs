using OSPABA;
using Actors;
using Statistics;
using simulation;
using agents;
using continualAssistants;
using instantAssistants;
namespace managers
{
	//meta! id="2"
	public class AEnvManager : Manager
	{
        private StatLength T1_length { get; set; }
        private StatTime T1_time { get; set; }
        private StatLength T2_length { get; set; }
        private StatTime T2_time { get; set; }
        private StatLength CR_length { get; set; }
        private StatTime CR_time { get; set; }

        public AEnvManager(int id, Simulation mySim, Agent myAgent) :
			base(id, mySim, myAgent)
		{
			Init();
		}

        public override void PrepareReplication()
		{
            this.InitStats();

			if (PetriNet != null)
			{
				PetriNet.Clear();
			}
		}

		//meta! sender="ASim", id="29", type="Notice"
		public void ProcessResetStat(MessageForm message)
		{
		}

		//meta! sender="ASim", id="15", type="Notice"
		public void ProcessInit(MessageForm message)
		{
		}

		//meta! sender="ASim", id="14", type="Notice"
		public void ProcessLeaveT3(MessageForm message)
		{
		}

		//meta! sender="ASim", id="16", type="Notice"
		public void ProcessLeaveCR(MessageForm message)
		{
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
			}
		}

        private void InitStats()
        {
            T1_length = new StatLength();
            T2_length = new StatLength();
            CR_length = new StatLength();
            T1_time = new StatTime();
            T2_time = new StatTime();
            CR_time = new StatTime();
        }

		//meta! sender="EnterT2", id="81", type="Finish"
		public void ProcessFinishEnterT2(MessageForm message)
		{
		}

		//meta! sender="EnterT1", id="79", type="Finish"
		public void ProcessFinishEnterT1(MessageForm message)
		{
		}

		//meta! sender="EnterCR", id="83", type="Finish"
		public void ProcessFinishEnterCR(MessageForm message)
		{
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		public void Init()
		{
		}

		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.LeaveT3:
				ProcessLeaveT3(message);
			break;

			case Mc.Init:
				ProcessInit(message);
			break;

			case Mc.LeaveCR:
				ProcessLeaveCR(message);
			break;

			case Mc.Finish:
				switch (message.Sender.Id)
				{
				case SimId.EnterT2:
					ProcessFinishEnterT2(message);
				break;

				case SimId.EnterT1:
					ProcessFinishEnterT1(message);
				break;

				case SimId.EnterCR:
					ProcessFinishEnterCR(message);
				break;
				}
			break;

			case Mc.ResetStat:
				ProcessResetStat(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AEnv MyAgent
		{
			get
			{
				return (AEnv)base.MyAgent;
			}
		}
	}
}