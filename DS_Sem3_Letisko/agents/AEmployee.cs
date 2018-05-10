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
        public List<Employee> Employees { get; set; }

        private Queue<Passenger> Front { get; set; }
        private StatLength SFront_Length { get; set; }
        private StatTime SFront_Time { get; set; }
        private StatTime SEmployee_WorkingTime { get; set; }

		public AEmployee(int id, Simulation mySim, Agent parent) : base(id, mySim, parent)
		{
			Init();

            Front = new Queue<Passenger>();
            SFront_Length = new StatLength();
            SFront_Time = new StatTime();
            SEmployee_WorkingTime = new StatTime();
        }

		public override void PrepareReplication()
		{
			base.PrepareReplication();
            // Setup component for the next replication
            foreach (Employee e in Employees)
                e.ResetEmployee();
            ResetStats();

            Front.Clear(); // to be sure (not necessary)
		}

        public void SetEmpl(int Count) {
            Employees = new List<Employee>(Count);
            for (int i = 0; i < Count; i++)
                Employees.Add(new Employee((MySimulation)MySim, i));
        } 

        public void ResetStats()
        {
            foreach (Employee e in Employees)
                e.TimeOfWorking = 0;
            SFront_Time.Reset();
            SFront_Length.Reset();
        }

        public double GetStats(int which) {
            switch (which)
            {
                case 1:
                    return SFront_Time.GetStat();
                case 2:
                    return SFront_Length.GetStat();
            }
            return 0;
        }

        public int FrontSize() => Front.Count;

        public Passenger GetFromQueue ()
        {
            if (Front.Count == 0)
                return null;
            SFront_Length.AddStat(MySim.CurrentTime, Front.Count);
            Passenger p = Front.Dequeue();
            if (p.ArrivedAt < 3)
                SFront_Time.AddStat(MySim.CurrentTime - p.ArrivalTimeCR); // type terminals
            else
                SFront_Time.AddStat(MySim.CurrentTime - p.ArrivalTime); // type 3
            return p;
        }

        public void AddToQueue(Passenger p)
        {
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

        public bool ArePeopleHere() => Front.Count > 0 || Employees.Where(e => !e.Free).Count() > 0;

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