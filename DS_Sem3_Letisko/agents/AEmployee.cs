using OSPABA;
using simulation;
using managers;
using continualAssistants;
using instantAssistants;
using System.Collections.Generic;
using Actors;
using Statistics;
using System.Linq;

namespace agents
{
	//meta! id="5"
	public class AEmployee : Agent
	{
        private List<Employee> Employees { get; set; }

        private Queue<Passenger> Front { get; set; }
        private StatLength SFront_Length { get; set; }
        private StatTime SFront_Time { get; set; }

		public AEmployee(int id, Simulation mySim, Agent parent) : base(id, mySim, parent)
		{
			Init();

            Front = new Queue<Passenger>();
            SFront_Length = new StatLength();
            SFront_Time = new StatTime();
		}

		public override void PrepareReplication()
		{
			base.PrepareReplication();
            // Setup component for the next replication
            foreach (Employee e in Employees)
                e.Free = true;
            ResetStats();

            Front.Clear();
		}

        public void SetEmpl(int Count) {
            Employees = new List<Employee>(Count);
            for (int i = 0; i < Count; i++)
                Employees.Add(new Employee((MySimulation)MySim, i));
        } 

        public void ResetStats()
        {
            SFront_Time.Reset();
            SFront_Length.Reset();
        }

        public Passenger GetFromQueue ()
        {
            if (Front.Count == 0)
                return null;
            SFront_Length.AddStat(MySim.CurrentTime, Front.Count);
            Passenger p = Front.Dequeue();
            SFront_Time.AddStat(MySim.CurrentTime - p.ArrivalTimeCR);
            return p;
        }

        public void AddToQueue(Passenger p)
        {
            p.ArrivalTimeCR = MySim.CurrentTime;
            SFront_Length.AddStat(MySim.CurrentTime, Front.Count);
            Front.Enqueue(p);
        }

        public Employee GetFirstFree()
        {
            Employee e = Employees.Where(em => em.Free).FirstOrDefault();
            if (e == null)
                return null;
            e.Free = false;
            return e;
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