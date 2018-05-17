using OSPABA;
using simulation;
using managers;
using continualAssistants;
using instantAssistants;
using System.Collections.Generic;
using Actors;
using Statistics;
using Generator;
using System;
using System.Linq;

namespace agents
{
	//meta! id="4"
	public class AMinibus : Agent
    {
        public List<Minibus> Minis { get; set; }

        private Queue<Passenger> FrontT1 { get; set; }

        private StatTime SFrontT1_Time { get; set; }
        private StatLength SFrontT1_Length { get; set; }

        private Queue<Passenger> FrontT2 { get; set; }

        private StatTime SFrontT2_Time { get; set; }
        private StatLength SFrontT2_Length { get; set; }

        private Queue<Passenger> FrontCR { get; set; }

        private StatTime SFrontCR_Time { get; set; }
        private StatLength SFrontCR_Length { get; set; }

        public AMinibus(int id, Simulation mySim, Agent parent) : base(id, mySim, parent)
		{
			Init();

            FrontT1 = new Queue<Passenger>();
            FrontT2 = new Queue<Passenger>();
            FrontCR = new Queue<Passenger>();

            SFrontCR_Length = new StatLength();
            SFrontCR_Time = new StatTime();
            SFrontT1_Length = new StatLength();
            SFrontT1_Time = new StatTime();
            SFrontT2_Length = new StatLength();
            SFrontT2_Time = new StatTime();
        }

        public void SetMinis(int number, int type)
        {
            Minis = new List<Minibus>(number);
            Random r = new Random(Seed.GetSeed());
            for (int i = 0; i < number; i++)
                Minis.Add(new Minibus((MySimulation) MySim, r.Next(Const.CapacityOptions.Length), type, i));
        }

		public override void PrepareReplication()
		{
			base.PrepareReplication();
            // Setup component for the next replication
            Random r = new Random(Seed.GetSeed());
            foreach (Minibus m in Minis)
                m.Reinit(r.Next(4) + 1);
            ResetStats();
        }

        public double GetStats(int which)
        {
            switch (which)
            {
                case 1:
                    return SFrontT1_Time.GetStat();
                case 2:
                    return SFrontT2_Time.GetStat();
                case 3:
                    return SFrontCR_Time.GetStat();

                case 11:
                    return SFrontT1_Length.GetStat(((MySimulation)MySim).StartOfSimulation);
                case 12:
                    return SFrontT2_Length.GetStat(((MySimulation)MySim).StartOfSimulation);
                case 13:
                    return SFrontCR_Length.GetStat(((MySimulation)MySim).StartOfSimulation);
            }

            return 0;
        }

        public int GetQueueCount(int which)
        {
            switch (which)
            {
                case 1:
                    return FrontT1.Sum(pass => pass.SizeOfGroup);
                case 2:
                    return FrontT2.Sum(pass => pass.SizeOfGroup);
                case 4:
                    return FrontCR.Sum(pass => pass.SizeOfGroup);
            }
            return 0;
        }

        public void ResetStats()
        {
            SFrontCR_Length.Reset();
            SFrontCR_Time.Reset();
            SFrontT1_Length.Reset();
            SFrontT1_Time.Reset();
            SFrontT2_Length.Reset();
            SFrontT2_Time.Reset();
        }

        public void AddToQueue (Passenger p)
        {
            switch (p.ArrivedAt)
            {
                case 1:
                    SFrontT1_Length.AddStat(MySim.CurrentTime, GetQueueCount(1));
                    FrontT1.Enqueue(p);
                    break;
                case 2:
                    SFrontT2_Length.AddStat(MySim.CurrentTime, GetQueueCount(2));
                    FrontT2.Enqueue(p);
                    break;
                case 3:
                    SFrontCR_Length.AddStat(MySim.CurrentTime, GetQueueCount(4));
                    p.ArrivalTimeCR = MySim.CurrentTime;
                    FrontCR.Enqueue(p);
                    break;
            }
        }

        public bool SomeoneWaiting() => FrontT1.Count > 0 || FrontT2.Count > 0 || FrontCR.Count > 0;

        public void RemoveFromQueue (int type, Passenger p)
        {
            switch (type)
            {
                case 1:
                    SFrontT1_Length.AddStat(MySim.CurrentTime, GetQueueCount(1));
                    SFrontT1_Time.AddStat(MySim.CurrentTime - p.ArrivalTime);
                    FrontT1 = new Queue<Passenger>(FrontT1.Where(pass => pass != p));
                    break;
                case 2:
                    SFrontT2_Length.AddStat(MySim.CurrentTime, GetQueueCount(2));
                    SFrontT2_Time.AddStat(MySim.CurrentTime - p.ArrivalTime);
                    FrontT2 = new Queue<Passenger>(FrontT2.Where(pass => pass != p));
                    break;
                case 4:
                    SFrontCR_Length.AddStat(MySim.CurrentTime, GetQueueCount(4));
                    SFrontCR_Time.AddStat(MySim.CurrentTime - p.ArrivalTimeCR);
                    FrontCR = new Queue<Passenger>(FrontCR.Where(pass => pass != p));
                    break;
            }
        }

        public bool IsEmpty(int type)
        {
            switch (type)
            {
                case 1:
                    return FrontT1.Count == 0;
                case 2:
                    return FrontT2.Count == 0;
                case 4:
                    return FrontCR.Count == 0;
            }

            return false;
        }

        public Queue<Passenger> GetQueue (int type)
        {
            switch (type)
            {
                case 1:
                    return FrontT1;
                case 2:
                    return FrontT2;
                case 3:
                    return new Queue<Passenger>();
                case 4:
                    return FrontCR;
            }

            return null;
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        private void Init()
		{
			new AMinibusManager(SimId.AMinibusManager, MySim, this);
			new GetIn(SimId.GetIn, MySim, this);
			new Transport(SimId.Transport, MySim, this);
			new AddToFront(SimId.AddToFront, MySim, this);
			new GetOut(SimId.GetOut, MySim, this);
			AddOwnMessage(Mc.ResetStat);
			AddOwnMessage(Mc.Move);
            AddOwnMessage(Mc.Stop); // stop the busses
            AddOwnMessage(Mc.ProcessPassenger);
		}
		//meta! tag="end"
	}
}
