using OSPABA;
using agents;
using Statistics;
using System;
using Actors;

namespace simulation
{
    public class MySimulation : Simulation
    {
        private int Repl_C;
        public double StartOfSimulation;
        public bool Slow;
        public double Slow_interval;
        public double Slow_duration;

        public StatTime STimeFromTerminal { get; set; }
        public SIntervalOfConfidence STimeFromTerminal_I { get; set; }
        public StatTime STimeFromAirRental { get; set; }
        public SIntervalOfConfidence STimeFromAirRental_I { get; set; }

        public StatTime SWaitCR { get; set; }
        public SIntervalOfConfidence SWaitCR_I { get; set; }
        public StatTime SWaitCR_Length { get; set; }
        public SIntervalOfConfidence SWaitCR_Length_I { get; set; }
        public StatTime SWaitCR_Out { get; set; }
        public SIntervalOfConfidence SWaitCR_Out_I { get; set; }
        public StatTime SWaitCR_Length_Out { get; set; }
        public SIntervalOfConfidence SWaitCR_Length_Out_I { get; set; }
        public StatTime SWaitT1 { get; set; }
        public SIntervalOfConfidence SWaitT1_I { get; set; }
        public StatTime SWaitT1_Length { get; set; }
        public SIntervalOfConfidence SWaitT1_Length_I { get; set; }
        public StatTime SWaitT2 { get; set; }
        public SIntervalOfConfidence SWaitT2_I { get; set; }
        public StatTime SWaitT2_Length { get; set; }
        public SIntervalOfConfidence SWaitT2_Length_I { get; set; }

        public double Driver_Salary;
        public StatTime SMinibus_Cost { get; set; }
        public SIntervalOfConfidence SMinibus_Cost_I { get; set; }
        public double Employee_Salary;
        public StatTime SEmployee_Cost { get; set; }
        public SIntervalOfConfidence SEmployee_Cost_I { get; set; }

        public StatTime SEmployee_Working { get; set; }
        public SIntervalOfConfidence SEmployee_Working_I { get; set; }

        public StatTime SMinibus_Working { get; set; }
        public SIntervalOfConfidence SMinibus_Working_I { get; set; }

        public MySimulation()
        {
            Init();

            STimeFromTerminal = new StatTime();
            STimeFromTerminal_I = new SIntervalOfConfidence();
            STimeFromAirRental = new StatTime();
            STimeFromAirRental_I = new SIntervalOfConfidence();
            SWaitCR = new StatTime();
            SWaitCR_I = new SIntervalOfConfidence();
            SWaitCR_Length = new StatTime();
            SWaitCR_Length_I = new SIntervalOfConfidence();
            SWaitCR_Out = new StatTime();
            SWaitCR_Out_I = new SIntervalOfConfidence();
            SWaitCR_Length_Out = new StatTime();
            SWaitCR_Length_Out_I = new SIntervalOfConfidence();
            SWaitT1 = new StatTime();
            SWaitT1_I = new SIntervalOfConfidence();
            SWaitT1_Length = new StatTime();
            SWaitT1_Length_I = new SIntervalOfConfidence();
            SWaitT2 = new StatTime();
            SWaitT2_I = new SIntervalOfConfidence();
            SWaitT2_Length = new StatTime();
            SWaitT2_Length_I = new SIntervalOfConfidence();

            SMinibus_Cost = new StatTime();
            SMinibus_Cost_I = new SIntervalOfConfidence();
            SMinibus_Working = new StatTime();
            SMinibus_Working_I = new SIntervalOfConfidence();

            SEmployee_Cost = new StatTime();
            SEmployee_Cost_I = new SIntervalOfConfidence();
            SEmployee_Working = new StatTime();
            SEmployee_Working_I = new SIntervalOfConfidence();
        }

        public void SetParams(int Minis_C, int Minis_T, int Empl_C, double Days_S, double Days_E, double HeatUp_L, int Repl_C, bool slow, double interval, double duration, double Driver_Salary, double Employee_Salary)
        {
            AMinibus.SetMinis(Minis_C, Minis_T);
            AEmployee.SetEmpl(Empl_C);
            AAirport.DayStart = Days_S;
            AAirport.SetHeatUp(HeatUp_L);
            StartOfSimulation = Days_S - HeatUp_L;

            this.Driver_Salary = Driver_Salary;
            this.Employee_Salary = Employee_Salary;

            this.Repl_C = Repl_C;

            if (slow)
                SetSpeed(interval, duration);
            else
                SetMaxSpeed();
        }

        public void Start() => SimulateAsync(Repl_C);

        public void SetSpeed(double interval, double duration)
        {
            Slow = true;
            Slow_interval = interval;
            Slow_duration = duration;
            base.SetSimSpeed(Slow_interval, Slow_duration);
        }

        public void SetMaxSpeed()
        {
            Slow = false;
            base.SetMaxSimSpeed();
        }

        protected override void PrepareSimulation()
        {
            base.PrepareSimulation();
            // Create global statistcis

            STimeFromTerminal.Reset();
            STimeFromTerminal_I.Reset();
            STimeFromAirRental.Reset();
            STimeFromAirRental_I.Reset();
            SWaitCR.Reset();
            SWaitCR_I.Reset();
            SWaitCR_Length.Reset();
            SWaitCR_Length_I.Reset();
            SWaitT1.Reset();
            SWaitT1_I.Reset();
            SWaitT1_Length.Reset();
            SWaitT1_Length_I.Reset();
            SWaitT2.Reset();
            SWaitT2_I.Reset();
            SWaitT2_Length.Reset();
            SWaitT2_Length_I.Reset();
            SWaitCR_Out.Reset();
            SWaitCR_Out_I.Reset();
            SWaitCR_Length_Out.Reset();
            SWaitCR_Length_Out_I.Reset();

            SMinibus_Cost.Reset();
            SMinibus_Cost_I.Reset();
            SMinibus_Working.Reset();
            SMinibus_Working_I.Reset();

            SEmployee_Working.Reset();
            SEmployee_Working_I.Reset();
            SEmployee_Cost.Reset();
            SEmployee_Cost_I.Reset();
        }

        protected override void PrepareReplication()
        {
            base.PrepareReplication();
            // Reset entities, queues, local statistics, etc...

            ASim.InitReplication();
        }

        protected override void ReplicationFinished()
        {
            // Collect local statistics into global, update UI, etc...
            base.ReplicationFinished();

            STimeFromTerminal.AddStat(AEnv.GetStats(1));
            STimeFromTerminal_I.AddStat(AEnv.GetStats(1));
            STimeFromAirRental.AddStat(AEnv.GetStats(2));
            STimeFromAirRental_I.AddStat(AEnv.GetStats(2));

            SWaitCR.AddStat(AEmployee.GetStats(1));
            SWaitCR_I.AddStat(AEmployee.GetStats(1));
            SWaitCR_Length.AddStat(AEmployee.GetStats(2));
            SWaitCR_Length_I.AddStat(AEmployee.GetStats(2));

            SWaitT1.AddStat(AMinibus.GetStats(1));
            SWaitT1_I.AddStat(AMinibus.GetStats(1));
            SWaitT1_Length.AddStat(AMinibus.GetStats(11));
            SWaitT1_Length_I.AddStat(AMinibus.GetStats(11));
            SWaitT2.AddStat(AMinibus.GetStats(2));
            SWaitT2_I.AddStat(AMinibus.GetStats(2));
            SWaitT2_Length.AddStat(AMinibus.GetStats(12));
            SWaitT2_Length_I.AddStat(AMinibus.GetStats(12));
            SWaitCR_Out.AddStat(AMinibus.GetStats(3));
            SWaitCR_Out_I.AddStat(AMinibus.GetStats(3));
            SWaitCR_Length_Out.AddStat(AMinibus.GetStats(13));
            SWaitCR_Length_Out_I.AddStat(AMinibus.GetStats(13));

            double costs = GetMinibusCosts();

            SMinibus_Cost.AddStat(costs);
            SMinibus_Cost_I.AddStat(costs);

            costs = GetEmployeeCosts();

            SEmployee_Cost.AddStat(costs);
            SEmployee_Cost_I.AddStat(costs);

            costs = GetEmployeeWorking();

            SEmployee_Working.AddStat(costs);
            SEmployee_Working_I.AddStat(costs);

            costs = GetMinibusWorking();

            SMinibus_Working.AddStat(costs);
            SMinibus_Working_I.AddStat(costs);
        }

        public double GetEmployeeWorking() // workload average of employees
        {
            double worked_hours = CurrentTime - AAirport.DayStart;
            double costs = 0;
            double temp = 0;
            foreach (Employee e in AEmployee.Employees)
            {
                temp = e.GetWorkload(worked_hours);
                costs += temp;
                e.Workload.AddStat(temp);
            }
            return costs / AEmployee.Employees.Count;
        }

        public double GetMinibusWorking()
        {
            double costs = 0;
            foreach (Minibus m in AMinibus.Minis)
                costs += m.OnBoardStat_Global.GetStat();

            return costs / AMinibus.Minis.Count;
        }

        public double GetMinibusCosts()
        {
            double worked_hours = CurrentTime - AAirport.DayStart;
            double costs = AMinibus.Minis.Count * (worked_hours / Const.HourToSecond) * Driver_Salary;
            foreach (Minibus m in AMinibus.Minis)
            {
                m.OnBoardStat_Global.AddStat(m.OnBoardStat.GetStat());
                costs += m.GetCosts();
            }

            return costs;
        }

        public double GetEmployeeCosts()
        {
            double worked_hours = CurrentTime - AAirport.DayStart;
            double costs = AEmployee.Employees.Count * (worked_hours / Const.HourToSecond) * Driver_Salary;

            return costs;
        }

        protected override void SimulationFinished()
        {
            // Dysplay simulation results
            base.SimulationFinished();
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        private void Init()
        {
            ASim = new ASim(SimId.ASim, this, null);
            AEnv = new AEnv(SimId.AEnv, this, ASim);
            AAirport = new AAirport(SimId.AAirport, this, ASim);
            AMinibus = new AMinibus(SimId.AMinibus, this, AAirport);
            AEmployee = new AEmployee(SimId.AEmployee, this, AAirport);
        }
        public ASim ASim
        { get; set; }
        public AEnv AEnv
        { get; set; }
        public AAirport AAirport
        { get; set; }
        public AMinibus AMinibus
        { get; set; }
        public AEmployee AEmployee
        { get; set; }
        //meta! tag="end"
    }
}