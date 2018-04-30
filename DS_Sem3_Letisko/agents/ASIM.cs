using OSPABA;
using simulation;
using managers;

namespace agents
{
	//meta! id="1"
	public class ASim : Agent
	{
        public ASim(int id, Simulation mySim, Agent parent) : base(id, mySim, parent) => Init();

        public override void PrepareReplication()
		{
			base.PrepareReplication();
            // Setup component for the next replication
        }

        public void InitReplication()
        {
            // create message to notify simulation about start generating arrivals and moving of minis
            MessageForm m = new MyMessage(MySim);
            m.Code = Mc.Init;
            m.Addressee = this;
            MyManager.Notice(m);
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ASimManager(SimId.ASimManager, MySim, this);
			AddOwnMessage(Mc.EnterT1);
			AddOwnMessage(Mc.ServePassenger);
			AddOwnMessage(Mc.ResetStat);
			AddOwnMessage(Mc.EnterCR);
			AddOwnMessage(Mc.EnterT2);
            AddOwnMessage(Mc.Init);
        }
		//meta! tag="end"
	}
}