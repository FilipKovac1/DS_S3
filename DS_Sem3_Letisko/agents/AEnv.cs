using OSPABA;
using simulation;
using managers;
using Actors;
using Statistics;
using continualAssistants;
using instantAssistants;
namespace agents
{
	//meta! id="2"
	public class AEnv : Agent
    {
        private StatLength T1_length { get; set; }
        private StatTime T1_time { get; set; }
        private StatLength T2_length { get; set; }
        private StatTime T2_time { get; set; }
        private StatLength CR_length { get; set; }
        private StatTime CR_time { get; set; }

        private int NumberOfEntersTerminals { get; set; }
        private int NumberOfLeavesTerminals { get; set; }
        private int NumberOfEntersCR { get; set; }
        private int NumberOfLeavesCR { get; set; }

        public AEnv(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();

            this.InitStats();
            // Setup component for the next replication
        }

        private void InitStats()
        {
            T1_length = new StatLength();
            T2_length = new StatLength();
            CR_length = new StatLength();
            T1_time = new StatTime();
            T2_time = new StatTime();
            CR_time = new StatTime();

            this.NumberOfEntersCR = 0;
            this.NumberOfLeavesCR = 0;
            this.NumberOfEntersTerminals = 0;
            this.NumberOfLeavesTerminals = 0;
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new AEnvManager(SimId.AEnvManager, MySim, this);
			new StatLeaveAfterCR(SimId.StatLeaveAfterCR, MySim, this);
			new EnterT2(SimId.EnterT2, MySim, this);
			new EnterCR(SimId.EnterCR, MySim, this);
			new EnterT1(SimId.EnterT1, MySim, this);
			new StatLeaveAfterTerminal(SimId.StatLeaveAfterTerminal, MySim, this);
			AddOwnMessage(Mc.ResetStat);
			AddOwnMessage(Mc.Init);
			AddOwnMessage(Mc.LeaveT3);
			AddOwnMessage(Mc.LeaveCR);
		}
		//meta! tag="end"
	}
}