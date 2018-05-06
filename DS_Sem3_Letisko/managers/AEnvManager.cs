using OSPABA;
using Actors;
using simulation;
using agents;
using System;

namespace managers
{
	//meta! id="2"
	public class AEnvManager : Manager
	{
        private Random random = new Random(Generator.Seed.GetSeed());

        public AEnvManager(int id, Simulation mySim, Agent myAgent) : base(id, mySim, myAgent) => Init();

        public override void PrepareReplication()
		{
			if (PetriNet != null)
			{
				PetriNet.Clear();
			}
		}

        //meta! sender="ASim", id="29", type="Notice"
        public void ProcessResetStat(MessageForm message) => MyAgent.ResetStats();

        //meta! sender="ASim", id="15", type="Notice"
        public void ProcessInit(MessageForm message)
        {
            MyMessage m1 = (MyMessage)message.CreateCopy();
            m1.Addressee = MyAgent.FindAssistant(SimId.EnterT1);
            StartContinualAssistant(m1);
            MyMessage m2 = (MyMessage)message.CreateCopy();
            m2.Addressee = MyAgent.FindAssistant(SimId.EnterT2);
            StartContinualAssistant(m2);
            MyMessage m3 = (MyMessage)message.CreateCopy();
            m3.Addressee = MyAgent.FindAssistant(SimId.EnterCR);
            StartContinualAssistant(m3);
        }

        //meta! sender="ASim", id="14", type="Notice"
        public void ProcessLeaveT3(MessageForm message)
        {
            MyAgent.AddToStat(2, (MyMessage)message);
            if (!MyAgent.Generate)
                EndCooling(message);
        }

        //meta! sender="ASim", id="16", type="Notice"
        public void ProcessLeaveCR(MessageForm message)
        {
            MyAgent.AddToStat(1, (MyMessage)message);
            if (!MyAgent.Generate)
                EndCooling(message);
        }

        private void EndCooling(MessageForm message)
        {
            if (MyAgent.EqualsEnterLeave()) // all arriveals left the system
            {
                MyAgent.InsertToReplStats();
                ((MyMessage)message).Passenger = null;
                message.Code = Mc.EndCooling;
                message.Addressee = MySim.FindAgent(SimId.ASim);
                Notice(message);
            }
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

        private void ProcessFinishEnter(MessageForm message, int type)
        {
            ((MyMessage)message).Passenger = new Passenger((MySimulation)MySim, type, GetSizeOfGroup());
            MyAgent.IncrementEnter(type);

            message.Code = type > 2 ? Mc.EnterCR : (type > 1 ? Mc.EnterT2 : Mc.EnterT1);
            message.Addressee = MySim.FindAgent(SimId.ASim);
            Notice(message);
        }

        //meta! sender="EnterT1", id="79", type="Finish"
        public void ProcessFinishEnterT1(MessageForm message) => ProcessFinishEnter(message, 1);

        //meta! sender="EnterT2", id="81", type="Finish"
        public void ProcessFinishEnterT2(MessageForm message) => ProcessFinishEnter(message, 2);

        //meta! sender="EnterCR", id="83", type="Finish"
        public void ProcessFinishEnterCR(MessageForm message) => ProcessFinishEnter(message, 3);

        private int GetSizeOfGroup()
        {
            int ret = 1;
            double prob = random.NextDouble();
            foreach (double p in Const.GroupSizeCumProb)
                if (prob < p) ret++; else break;
            return ret;
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		public void Init()
		{
		}

		public override void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.Finish:
				switch (message.Sender.Id)
				{
				case SimId.EnterCR:
					ProcessFinishEnterCR(message);
				break;

				case SimId.EnterT1:
					ProcessFinishEnterT1(message);
				break;

				case SimId.EnterT2:
					ProcessFinishEnterT2(message);
				break;
				}
			break;

			case Mc.Init:
				ProcessInit(message);
			break;

			case Mc.LeaveT3:
				ProcessLeaveT3(message);
			break;

			case Mc.ResetStat:
				ProcessResetStat(message);
			break;

			case Mc.LeaveCR:
				ProcessLeaveCR(message);
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