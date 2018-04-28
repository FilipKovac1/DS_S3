using OSPABA;
using simulation;
using managers;
using continualAssistants;
using instantAssistants;
using System.Collections.Generic;
using Actors;
using Statistics;

namespace agents
{
	//meta! id="5"
	public class AEmployee : Agent
	{
        private List<Employee> Employees { get; set; }

        private StatLength FrontLength { get; set; }
        private StatTime FronTime { get; set; }

		public AEmployee(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new AEmployeeManager(SimId.AEmployeeManager, MySim, this);
			new ServicePassenger(SimId.ServicePassenger, MySim, this);
			new AddToQ(SimId.AddToQ, MySim, this);
			AddOwnMessage(Mc.ServePassenger);
			AddOwnMessage(Mc.ResetStat);
		}
		//meta! tag="end"
	}
}