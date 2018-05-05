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
        public double DayStart { get; set; }
        public int ActualDay { get; set; }

        private int ServedPass { get; set; }

        public AAirport(int id, Simulation mySim, Agent parent) : base(id, mySim, parent) => Init();

        public void SetHeatUp(double time) => HeatUp.HeatUp = time; // time for heat simulation up

        public override void PrepareReplication()
        {
            base.PrepareReplication();// Setup component for the next replication
            ActualDay = 0;

            ServedPass = 0;
        }

        public bool AllServed() => ServedPass == 0;
        public void ServedInc() => ServedPass++;
        public void ServedDec() => ServedPass--;

        //meta! userInfo="Generated code: do not modify", tag="begin"
        private void Init()
		{
			new AAirportManager(SimId.AAirportManager, MySim, this);
			HeatUp = new EndHeatUp(SimId.EndHeatUp, MySim, this);
			AddOwnMessage(Mc.ServePassenger);
			AddOwnMessage(Mc.Init);
			AddOwnMessage(Mc.Move);
            AddOwnMessage(Mc.Stop);
            AddOwnMessage(Mc.ProcessPassenger);
		}
		//meta! tag="end"
	}
}