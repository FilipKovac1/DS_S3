using OSPABA;
using simulation;
using managers;
using continualAssistants;
namespace agents
{
	//meta! id="3"
	public class AAirport : Agent
	{
        public EndHeatUp HeatUp { get; set; }
        public EndDay DayEnd { get; set; }
        public double DayStart { get; set; }
        public int ActualDay { get; set; }

        public AAirport(int id, Simulation mySim, Agent parent) : base(id, mySim, parent) => Init();

        public void SetHeatUp(double time) => HeatUp.HeatUp = time; // time for heat simulation up

        public void SetWorkDay(double time) => DayEnd.WorkDay = time; // time of one work day

        public override void PrepareReplication()
        {
            base.PrepareReplication();// Setup component for the next replication
            ActualDay = 1;
            MySim.CurrentTime = DayStart - HeatUp.HeatUp;
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        private void Init()
		{
			new AAirportManager(SimId.AAirportManager, MySim, this);
			DayEnd = new EndDay(SimId.EndDay, MySim, this);
			HeatUp = new EndHeatUp(SimId.EndHeatUp, MySim, this);
			AddOwnMessage(Mc.ServePassenger);
			AddOwnMessage(Mc.Init);
			AddOwnMessage(Mc.Move);
			AddOwnMessage(Mc.ProcessPassenger);
		}
		//meta! tag="end"
	}
}