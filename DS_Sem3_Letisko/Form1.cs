using System;
using System.Windows.Forms;
using Actors;
using System.Collections.Generic;
using System.Drawing;
using simulation;
using OSPABA;

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
                tables_e.Controls.Find(e.Index + "_empl", true)[0].Text = e.ToString();

            //tables.Controls.Find("_empl", true)[0].Text = empls.Where(e => !e.Free).Count() + " / " + empls.Count;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            SetBtnText(btnPause, "Pause");
            if (tabSimGraph.SelectedTab.Name == "tabGraphs") { }
            else
                try
                {
                    double days_S = Double.Parse(_tb_Day_Start.Text) * Const.HourToSecond;
                    double days_E = Double.Parse(_tb_Day_End.Text) * Const.HourToSecond;
                    double heatUp = Double.Parse(_tb_HeatUp.Text) * Const.HourToSecond;
                    int days_C = Int32.Parse(_tb_Days_Count.Text);
                    int repl_C = Int32.Parse(_tb_Repl_Count.Text);

                    int mini_C = Int32.Parse(_tb_Mini_Count.Text);
                    int empl_C = Int32.Parse(_tb_Empl_Count.Text);
                    InitInfoComponents(mini_C, empl_C);

                    int mini_T = _cb_TypeMinis.SelectedIndex;

                    bool Slow = simModeSlow.Checked;
                    double interval = simulationSpeed.Value; // <1;20>
                    double duration = SimSpeed[simulationSpeed.Value - 1];

                    Simulation.SetParams(mini_C, mini_T, empl_C, days_C, days_S, days_E, heatUp, repl_C, Slow, interval, duration);
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
            {
                this.Invoke(new System.Action(() =>
                {
                    SetLabelText(_l_Simulation_Time, ComputeHours(simulation, "", null));
                    RefreshReplicationStats((MySimulation)simulation);
                }));
            }
            else
            {
                SetLabelText(_l_Simulation_Time, ComputeHours(simulation, "", null));
                RefreshReplicationStats((MySimulation)simulation);
            }
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
                }));
            }
            else
            {
                SetLabelText(_l_Simulation_Time, ComputeHours(simulation, "", null));
                ShowActorsInfo(((MySimulation)simulation).AMinibus.Minis, ((MySimulation)simulation).AEmployee.Employees);
            }
        }

        private void RefreshReplicationStats(MySimulation sim)
        {
            //repl_cr_avg_count_in = sim.AEmployee.GetReplStats(); // stat front size to be served 
            //repl_cr_wait_time = sim.AEmployee.GetReplStats(); // stat waiting time for serve
            //repl_cr_avg_count_out = sim.AMinibus.GetReplStats(); // stat front size waiting for bus to T3
            repl_sim_avg_time_in.Text = ComputeHours(sim, "HH:mm:ss", sim.AEnv.GetReplStats(1));
            repl_sim_avg_time_out.Text = ComputeHours(sim, "HH:mm:ss", sim.AEnv.GetReplStats(2));
            
        }

        private string ComputeHours(Simulation simulation, string format, double? time)
        {
            double t = time.HasValue ? time.Value : simulation.CurrentTime;
            if (Double.IsNaN(t))
                return "0";
            TimeSpan ts = TimeSpan.FromSeconds(t);
            switch (format)
            {
                case "HH:mm:ss":
                    return ts.ToString(@"hh\:mm\:ss");
                default:
                    return String.Format("Replication: {0} Day: {1} Time: {2}", simulation.CurrentReplication + 1, ((MySimulation)simulation).AAirport.ActualDay, ts.ToString(@"hh\:mm\:ss"));
            }
        }


        private void btnStop_Click(object sender, EventArgs e)
        {
            SetBtnText(btnPause, "Pause");
            Simulation.StopSimulation();
            Simulation = new MySimulation();
        }

        private void btnPause_Click(object sender, EventArgs e)
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

        private void tb_valueChanged(object sender, EventArgs e)
        {
            SetLabelText(simSpeedLabel, SimSpeedString[simulationSpeed.Value - 1]);
            if (simModeSlow.Checked) {
                Simulation.SetSpeed(simulationSpeed.Value * 20, SimSpeed[simulationSpeed.Value - 1]);
            }
        }

        private void simModeChanged(object sender, EventArgs e)
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
