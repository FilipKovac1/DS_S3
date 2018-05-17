using System;
using System.Windows.Forms;
using Actors;
using System.Collections.Generic;
using System.Drawing;
using simulation;
using OSPABA;
using System.Linq;

namespace DS_Sem3_Letisko
{
    /// <summary>
    /// Main GUI
    /// </summary>
    public partial class Form1 : Form
    {
        MySimulation Simulation = null;

        private static readonly double[] SimSpeed = new double[] {1, 0.75, 0.5, 0.25, 0.1, 0.075, 0.05, 0.025, 0.01, 0.0075, 0.005, 0.0025, 0.001, 0.00075, 0.0005, 0.00025, 0.0001, 0.0005, 0.00001, 0.000001};
        private static readonly string[] SimSpeedString = new string[] { "1:1", "2:1", "5:1", "10:1", "15:1", "20:1", "25:1", "50:1", "75:1", "100:1", "125:1", "150:1", "175:1", "200:1", "250:1", "300:1", "350:1", "400:1", "450:1", "500:1" };

        public Form1()
        {
            InitializeComponent();
            // gui events

            for (int i = 0; i < Const.CapacityOptions.Length; i++)
                _cb_TypeMinis.Items.Insert(i, Const.CapacityOptions[i]);
            _cb_TypeMinis.SelectedIndex = 0;

            Simulation = new MySimulation();
        }

        /// <summary>
        /// Init table (labels) for employees and minibuses
        /// </summary>
        /// <param name="minis"></param>
        /// <param name="empls"></param>
        private void InitInfoComponents(int minis, int empls)
        {
            tables_b.Controls.Clear();
            tables_e.Controls.Clear();
            int y = 0, x = 0;
            for (int i = 0; i < minis; i++)
            {
                tables_b.Controls.Add(
                    new Label
                    {
                        Location = new Point(x, y),
                        Text = (i + 1) + ". minibus: ",
                        AutoSize = true
                    });
                tables_b.Controls.Add(
                    new Label
                    {
                        Location = new Point(x + 80, y),
                        Text = "",
                        Name = i + "_mini",
                        AutoSize = true
                    });

                y += 15;
            }
            y = 0;
            for (int i = 0; i < empls; i++)
            {
                tables_e.Controls.Add(
                    new Label
                    {
                        Location = new Point(x, y),
                        Text = (i + 1) + ". employee: ",
                        //Text = "Employees (Working / All): ",
                        AutoSize = true
                    });
                tables_e.Controls.Add(
                    new Label
                    {
                        Location = new Point(x + 150, y),
                        Text = "",
                        Name = i + "_empl",
                        AutoSize = true
                    });
                y += 15;
            }
        }

        /// <summary>
        /// Method fill labels of minibuses and employees with data
        /// </summary>
        /// <param name="minis"></param>
        /// <param name="empls"></param>
        private void ShowActorsInfo(List<Minibus> minis, List<Employee> empls)
        {
            foreach (Minibus m in minis)
                tables_b.Controls.Find(m.Index + "_mini", true)[0].Text = m.ToString(Simulation.CurrentTime);

            foreach (Employee e in empls)
                tables_e.Controls.Find(e.Index + "_empl", true)[0].Text = e.ToString(Simulation.CurrentTime);

            //tables.Controls.Find("_empl", true)[0].Text = empls.Where(e => !e.Free).Count() + " / " + empls.Count;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            SetBtnText(btnPause, "Pause");
            if (tabSimGraph.SelectedTab.Name == "tabGraphs") { }
            else
                try
                {
                    double days_S = Double.Parse(_tb_Day_Start.Text) * Const.HourToSecond;
                    double days_E = Double.Parse(_tb_Day_End.Text) * Const.HourToSecond;
                    double heatUp = Double.Parse(_tb_HeatUp.Text) * Const.HourToSecond;
                    int repl_C = Int32.Parse(_tb_Repl_Count.Text);

                    int mini_C = Int32.Parse(_tb_Mini_Count.Text);
                    int empl_C = Int32.Parse(_tb_Empl_Count.Text);
                    InitInfoComponents(mini_C, empl_C);

                    int mini_T = _cb_TypeMinis.SelectedIndex;

                    bool Slow = simModeSlow.Checked;
                    double interval = simulationSpeed.Value; // <1;20>
                    double duration = SimSpeed[simulationSpeed.Value - 1];

                    double Driver_Salary = Double.Parse(_tb_Mini_Salary.Text);
                    double Employee_Salary = Double.Parse(_tb_Empl_Salary.Text);

                    Simulation.SetParams(mini_C, mini_T, empl_C, days_S, days_E, heatUp, repl_C, Slow, interval, duration, Driver_Salary, Employee_Salary);
                    Simulation.OnReplicationDidFinish(sim => RefreshUIReplication(sim));
                    Simulation.OnSimulationDidFinish(sim => RefreshUISimulation(sim));
                    Simulation.OnRefreshUI(sim => RefreshUI(sim));
                    Simulation.OnPause(sim => { SetLabelText(_l_Simulation_Time, ComputeHours(sim, "", null)); });

                    Simulation.Start();
                }
                catch (Exception err)
                {
                    MessageBox.Show("Inputs were of wrong type. All inputs have to be of type integer.");
                }
        }

        private void RefreshUIReplication(Simulation simulation)
        {
            if (this.InvokeRequired)
                this.Invoke(new System.Action(() => RefreshReplicationStats((MySimulation)simulation)));
            else
                RefreshReplicationStats((MySimulation)simulation);
        }

        private void RefreshUISimulation(Simulation simulation)
        {

        }

        private void RefreshUI(Simulation simulation)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new System.Action(() =>
                {
                    SetLabelText(_l_Simulation_Time, ComputeHours(simulation, "", null));
                    ShowActorsInfo(((MySimulation)simulation).AMinibus.Minis, ((MySimulation)simulation).AEmployee.Employees);
                    RefreshActualStats((MySimulation)simulation);
                }));
            }
            else
            {
                SetLabelText(_l_Simulation_Time, ComputeHours(simulation, "", null));
                ShowActorsInfo(((MySimulation)simulation).AMinibus.Minis, ((MySimulation)simulation).AEmployee.Employees);
                RefreshActualStats((MySimulation)simulation);
            }
        }

        private void RefreshActualStats(MySimulation sim)
        {
            SetLabelText(t1_count, sim.AMinibus.GetQueueCount(1).ToString());
            SetLabelText(t1_count_avg, DoubleToString(sim.AMinibus.GetStats(11)));
            SetLabelText(t1_time_avg, ComputeHours(sim, "HH:mm:ss", sim.AMinibus.GetStats(1)));

            SetLabelText(t2_count, sim.AMinibus.GetQueueCount(2).ToString());
            SetLabelText(t2_count_avg, DoubleToString(sim.AMinibus.GetStats(12)));
            SetLabelText(t2_time_avg, ComputeHours(sim, "HH:mm:ss", sim.AMinibus.GetStats(2)));

            SetLabelText(cr_count_in, sim.AMinibus.GetQueueCount(4).ToString());
            SetLabelText(cr_count_in_avg, DoubleToString(sim.AMinibus.GetStats(13)));
            SetLabelText(cr_time_in_avg, ComputeHours(sim, "HH:mm:ss", sim.AMinibus.GetStats(3)));

            SetLabelText(_l_act_people_in_sim, sim.AAirport.ServedPass.ToString());
            SetLabelText(_l_act_serv_people, sim.AEmployee.Employees.Where(e => !e.Free).Count().ToString());
            SetLabelText(_l_act_moving_people, sim.AMinibus.Minis.Sum(e => e.OnBoard_Count).ToString());

            SetLabelText(_l_act_cr_wait_size, sim.AEmployee.FrontSize().ToString());
            SetLabelText(_l_act_cr_wait_time, ComputeHours(sim, "HH:mm:ss", sim.AEmployee.GetStats(1)));
            SetLabelText(_l_act_cr_wait_size_avg, DoubleToString(sim.AEmployee.GetStats(2)));
        }

        private string DoubleToString(double val) => Double.IsNaN(val) ? "0.00" : String.Format("{0:0.00}", val);

        private void RefreshReplicationStats(MySimulation sim)
        {
            if (!sim.Slow)
                SetLabelText(_l_Simulation_Time, String.Format("Replication: {0}", sim.CurrentReplication + 1));
            // here will be global stats

            cr_length_out.Text = String.Format("{0,3:0.000}", sim.SWaitCR_Length_Out.GetStat()); // stat front size to be served 
            cr_length_out_ic.Text = ICtoString(sim.SWaitCR_Length_Out_I.GetLowerBound(), sim.SWaitCR_Length_Out_I.GetHigherBound(), false);
            cr_time_out.Text = ComputeHours(null, "HH:mm:ss", sim.SWaitCR_Out.GetStat()); // stat waiting time for serve
            cr_time_out_ic.Text = ICtoString(sim.SWaitCR_Out_I.GetLowerBound(), sim.SWaitCR_Out_I.GetHigherBound(), true);

            cr_time.Text = ComputeHours(null, "HH:mm:ss", sim.SWaitCR.GetStat()); // stat front size waiting for bus to T3
            cr_time_ic.Text = ICtoString(sim.SWaitCR_I.GetLowerBound(), sim.SWaitCR_I.GetHigherBound(), true);
            cr_length.Text = String.Format("{0,3:0.000}", sim.SWaitCR_Length.GetStat());
            cr_length_ic.Text = ICtoString(sim.SWaitCR_Length_I.GetLowerBound(), sim.SWaitCR_Length_I.GetHigherBound(), false);

            t1_time.Text = ComputeHours(null, "HH:mm:ss", sim.SWaitT1.GetStat());
            t1_time_ic.Text = ICtoString(sim.SWaitT1_I.GetLowerBound(), sim.SWaitT1_I.GetHigherBound(), true);
            t1_length.Text = String.Format("{0,3:0.000}", sim.SWaitT1_Length.GetStat());
            t1_length_ic.Text = ICtoString(sim.SWaitT1_Length_I.GetLowerBound(), sim.SWaitT1_Length_I.GetHigherBound(), false);

            t2_time.Text = ComputeHours(null, "HH:mm:ss", sim.SWaitT2.GetStat());
            t2_time_ic.Text = ICtoString(sim.SWaitT2_I.GetLowerBound(), sim.SWaitT2_I.GetHigherBound(), true);
            t2_length.Text = String.Format("{0,3:0.000}", sim.SWaitT2_Length.GetStat());
            t2_length_ic.Text = ICtoString(sim.SWaitT2_Length_I.GetLowerBound(), sim.SWaitT2_Length_I.GetHigherBound(), false);

            whole_time_in.Text = ComputeHours(sim, "HH:mm:ss", sim.STimeFromTerminal.GetStat());
            whole_time_in_ic.Text = ICtoString(sim.STimeFromTerminal_I.GetLowerBound(), sim.STimeFromTerminal_I.GetHigherBound(), true);
            whole_time_out.Text = ComputeHours(sim, "HH:mm:ss", sim.STimeFromAirRental.GetStat());
            whole_time_out_ic.Text = ICtoString(sim.STimeFromAirRental_I.GetLowerBound(), sim.STimeFromAirRental_I.GetHigherBound(), true);

            mini_cost.Text = ComputeMoney(sim.SMinibus_Cost.GetStat());
            mini_cost_ic.Text = ICtoString(sim.SMinibus_Cost_I.GetLowerBound(), sim.SMinibus_Cost_I.GetHigherBound(), false);
            empl_cost.Text = ComputeMoney(sim.SEmployee_Cost.GetStat());
            empl_cost_ic.Text = ICtoString(sim.SEmployee_Cost_I.GetLowerBound(), sim.SEmployee_Cost_I.GetHigherBound(), false);

            empl_workload.Text = String.Format("{0,2:00.00} %", sim.SEmployee_Working.GetStat());
            empl_workload_ic.Text = ICtoString(sim.SEmployee_Working_I.GetLowerBound(), sim.SEmployee_Working_I.GetHigherBound(), false);
            mini_workload.Text = String.Format("{0,2:0.00} ", sim.SMinibus_Working.GetStat());
            mini_workload_ic.Text = ICtoString(sim.SMinibus_Working_I.GetLowerBound(), sim.SMinibus_Working_I.GetHigherBound(), false);

            foreach (Employee e in sim.AEmployee.Employees)
                tables_e.Controls.Find(e.Index + "_empl", true)[0].Text = String.Format("{0,2:00.00} %", e.Workload.GetStat());

            foreach (Minibus m in sim.AMinibus.Minis)
                tables_b.Controls.Find(m.Index + "_mini", true)[0].Text = String.Format("{0} km, {1,1:0.0} passenger/s", m.MileAge, m.OnBoardStat_Global.GetStat());
        }

        private string ComputeMoney(double value) => String.Format("{0, 2: 0.00} €", value);

        private string ICtoString(double lower, double higher, bool? time)
        {
            if (time.HasValue && time.Value)
                return String.Format("< {0},{1} > ; < {2,2:0.##}, {3,2:0.##} >", ComputeHours(null, "HH:mm:ss", lower), ComputeHours(null, "HH:mm:ss", higher), lower, higher);
            else
                return String.Format("< {0,2:0.##}, {1,2:0.##} >", lower, higher);
        }

        private string ComputeHours(Simulation simulation, string format, double? time)
        {
            double t = time ?? simulation.CurrentTime;
            if (Double.IsNaN(t) || Double.IsInfinity(t))
                return "0";
            TimeSpan ts = TimeSpan.FromSeconds(t);
            switch (format)
            {
                case "HH:mm:ss":
                    return ts.ToString(@"hh\:mm\:ss");
                default:
                    return String.Format("Replication: {0} Time: {1}", simulation.CurrentReplication + 1, ts.ToString(@"hh\:mm\:ss"));
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            SetBtnText(btnPause, "Pause");
            Simulation.StopSimulation();
            Simulation = new MySimulation();
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            if (btnPause.Text == "Pause")
            {
                Simulation.PauseSimulation();
                SetBtnText(btnPause, "Continue");
            } else
            {
                Simulation.ResumeSimulation();
                SetBtnText(btnPause, "Pause");
            }
        }

        /// <summary>
        /// Set button text 
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="text"></param>
        private void SetBtnText(Button btn, string text)
        {
            if (btn.InvokeRequired)
                btn.Invoke(new System.Action(() => btn.Text = text));
            else
                btn.Text = text;
        }
        private void SetLabelText(Label label, string text)
        {
            if (label.InvokeRequired)
                label.Invoke(new System.Action(() => label.Text = text));
            else
                label.Text = text;
        }

        private void Tb_valueChanged(object sender, EventArgs e)
        {
            SetLabelText(simSpeedLabel, SimSpeedString[simulationSpeed.Value - 1]);
            if (simModeSlow.Checked) {
                Simulation.SetSpeed(simulationSpeed.Value * 20, SimSpeed[simulationSpeed.Value - 1]);
            }
        }

        private void SimModeChanged(object sender, EventArgs e)
        {
            if (simModeSlow.Checked)
            {
                Simulation.SetSpeed(simulationSpeed.Value * 20, SimSpeed[simulationSpeed.Value - 1]);
            } else
            {
                Simulation.SetMaxSpeed();
            }
        }
    }
}
