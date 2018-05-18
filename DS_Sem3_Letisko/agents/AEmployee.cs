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
                e.ResetEmployee();
            ResetStats();

            Front.Clear(); // to be sure (not necessary)
		}

        /// <summary>
        /// Init employees
        /// </summary>
        /// <param name="Count">how many employees to create</param>
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

        /// <summary>
        /// Get statistics of agent employee
        /// </summary>
        /// <param name="which">
        /// 1 -> Time
        /// 2 -> Length
        /// </param>
        /// <returns></returns>
        public double GetStats(int which) {
            switch (which)
            {
                case 1:
                    return SFront_Time.GetStat();
                case 2:
                    return SFront_Length.GetStat(((MySimulation)MySim).StartOfSimulation);
            }
            return 0;
        }

        /// <summary>
        /// Return the size of front in front of CR
        /// </summary>
        /// <returns></returns>
        public int FrontSize() => Front.Count;

        /// <summary>
        /// Return the first arrived passenger (in system) in front and remove him from it
        /// Save data into statistics
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Add passenger into queue
        /// </summary>
        /// <param name="p"></param>
        public void AddToQueue(Passenger p)
        {
            SFront_Length.AddStat(MySim.CurrentTime, Front.Count);
            Front.Enqueue(p);
        }

        /// <summary>
        /// Get first free employee
        /// </summary>
        /// <returns></returns>
        public Employee GetFirstFree()
        {
            Employee e = Employees.Where(em => em.Free).FirstOrDefault();
            if (e == null)
                return null;
            e.Free = false;
            return e;
        }

        /// <summary>
        /// Indicates if there is any passenger on CR
        /// </summary>
        /// <returns></returns>
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