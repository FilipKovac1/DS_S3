using OSPABA;
using simulation;
using managers;
using Statistics;
using continualAssistants;

namespace agents
{
	//meta! id="2"
	public class AEnv : Agent
    {
        private int NumberOfEntersTerminal1 { get; set; }
        private int NumberOfEntersTerminal2 { get; set; }
        private int NumberOfLeavesTerminals { get; set; }
        private int NumberOfEntersCR { get; set; }
        private int NumberOfLeavesCR { get; set; }

        private StatTime TimeInSystemRental { get; set; }
        private StatTime TimeInSystemReturn { get; set; }

        public AEnv(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
            TimeInSystemRental = new StatTime();
            TimeInSystemReturn = new StatTime();

			Init();
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();

            this.InitStats();
            // Setup component for the next replication
        }

        public void InitStats()
        {
            this.TimeInSystemReturn.Reset();
            this.TimeInSystemRental.Reset();

            this.NumberOfEntersCR = 0;
            this.NumberOfLeavesCR = 0;
            this.NumberOfEntersTerminal1 = 0;
            this.NumberOfEntersTerminal2 = 0;
            this.NumberOfLeavesTerminals = 0;
        }

        public void AddToStat(int type, MyMessage message)
        {
            switch (type)
            {
                case 1:
                    NumberOfLeavesTerminals++;
                    TimeInSystemRental.AddStat(MySim.CurrentTime - message.Passenger.ArrivalTime);
                    break;
                case 2:
                    NumberOfLeavesCR++;
                    TimeInSystemReturn.AddStat(MySim.CurrentTime - message.Passenger.ArrivalTime);
                    break;
            }
        }

        public void IncrementEnter(int enter_place)
        {
            switch (enter_place)
            {
                case 1:
                    NumberOfEntersTerminal1++;
                    break;
                case 2:
                    NumberOfEntersTerminal2++;
                    break;
                case 3:
                    NumberOfEntersCR++;
                    break;
            }
        }

        public void IncrementLeave(int enter_place)
        {
            switch (enter_place)
            {
                case 1:
                    NumberOfLeavesTerminals++;
                    break;
                case 3:
                    NumberOfLeavesCR++;
                    break;
            }
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new AEnvManager(SimId.AEnvManager, MySim, this);
			new EnterCR(SimId.EnterCR, MySim, this);
			new EnterT1(SimId.EnterT1, MySim, this);
			new EnterT2(SimId.EnterT2, MySim, this);
			AddOwnMessage(Mc.ResetStat);
			AddOwnMessage(Mc.Init);
			AddOwnMessage(Mc.LeaveT3);
			AddOwnMessage(Mc.LeaveCR);
            AddOwnMessage(Mc.ProcessPassenger);
        }
		//meta! tag="end"
    }
}