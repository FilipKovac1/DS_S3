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
        public EndDay Day { get; set; }

		public AAirport(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

        public void SetHeatUp(int time) => HeatUp.HeatUp = time; // time for heat simulation up

        public void SetWorkDay(int time) => Day.WorkDay = time; // time of one work day

        public override void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new AAirportManager(SimId.AAirportManager, MySim, this);
			Day = new EndDay(SimId.EndDay, MySim, this);
			HeatUp = new EndHeatUp(SimId.EndHeatUp, MySim, this);
			AddOwnMessage(Mc.ServePassenger);
			AddOwnMessage(Mc.Init);
			AddOwnMessage(Mc.Move);
			AddOwnMessage(Mc.ProcessPassenger);
		}
		//meta! tag="end"
	}
}