using OSPABA;
using simulation;
using managers;
using Actors;
using Statistics;
using continualAssistants;
using instantAssistants;
using System;

namespace agents
{
	//meta! id="2"
	public class AEnv : Agent
    {
        private int NumberOfEntersTerminals { get; set; }
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

		override public void PrepareReplication()
		{
			base.PrepareReplication();

            this.InitStats();
            // Setup component for the next replication
        }

        private void InitStats()
        {
            this.TimeInSystemReturn.Reset();
            this.TimeInSystemRental.Reset();

            this.NumberOfEntersCR = 0;
            this.NumberOfLeavesCR = 0;
            this.NumberOfEntersTerminals = 0;
            this.NumberOfLeavesTerminals = 0;
        }

        public void AddToStat(int type, MyMessage message)
        {
            switch (type)
            {
                case 1:
                    TimeInSystemRental.AddStat(MySim.CurrentTime - message.Passenger.ArrivalTime);
                    break;
                case 2:
                    TimeInSystemReturn.AddStat(MySim.CurrentTime - message.Passenger.ArrivalTime);
                    break;
            }
        }

        public void IncrementEnter(int enter_place)
        {
            switch (enter_place)
            {
                case 1:
                    NumberOfEntersTerminals++;
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
			new StatLeaveAfterCR(SimId.StatLeaveAfterCR, MySim, this);
			new EnterT2(SimId.EnterT2, MySim, this);
			new EnterCR(SimId.EnterCR, MySim, this);
			new EnterT1(SimId.EnterT1, MySim, this);
			new StatLeaveAfterTerminal(SimId.StatLeaveAfterTerminal, MySim, this);
			AddOwnMessage(Mc.ResetStat);
			AddOwnMessage(Mc.Init);
			AddOwnMessage(Mc.LeaveT3);
			AddOwnMessage(Mc.LeaveCR);
            AddOwnMessage(Mc.ProcessPassenger);
		}
		//meta! tag="end"
	}
}