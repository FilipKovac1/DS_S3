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

        public Form1()
        {
            InitializeComponent();
            // gui events

            for (int i = 0; i < Const.CapacityOptions.Length; i++)
            {
                _cb_TypeMinis.Items.Insert(i, Const.CapacityOptions[i]);
            }
            _cb_TypeMinis.SelectedIndex = 0;

            Simulation = new MySimulation();

            //Simulation.SetParams();
        }

        /// <summary>
        /// Init table (labels) for employees and minibuses
        /// </summary>
        /// <param name="minis"></param>
        /// <param name="empls"></param>
        private void InitInfoComponents(int minis, int empls)
        {
            tables.Controls.Clear();
            int y = 0, x = 0;
            for (int i = 0; i < minis; i++)
            {
                tables.Controls.Add(
                    new Label
                    {
                        Location = new Point(x, y),
                        Text = (i + 1) + ". minibus: ",
                        AutoSize = true
                    });
                tables.Controls.Add(
                    new Label
                    {
                        Location = new Point(x + 80, y),
                        Text = "",
                        Name = i + "_mini",
                        AutoSize = true
                    });

                y += 15;
            }
            y += 15;
            for (int i = 0; i < empls; i++)
            {
                tables.Controls.Add(
                    new Label
                    {
                        Location = new Point(x, y),
                        Text = (i + 1) + ". employee: ",
                        //Text = "Employees (Working / All): ",
                        AutoSize = true
                    });
                tables.Controls.Add(
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
                tables.Controls.Find(m.Index + "_mini", true)[0].Text = m.ToString(Simulation.CurrentTime);

            foreach (Employee e in empls)
                tables.Controls.Find(e.Index + "_empl", true)[0].Text = e.ToString();

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

                    Simulation.SetParams(mini_C, mini_T, empl_C, days_C, days_S, days_E, heatUp, repl_C);
                    //Simulation.OnReplicationDidFinish(sim => RefreshUI(sim));
                    Simulation.SetSimSpeed(100, 0.00001);
                    Simulation.OnRefreshUI(new Action<Simulation>((sim) => RefreshUI(sim)));

                    Simulation.Start();
                }
                catch (Exception err)
                {
                    MessageBox.Show("Inputs were of wrong type. All inputs have to be of type integer.");
                }
        }

        private void RefreshUI(Simulation simulation)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new System.Action(() =>
                {
                    SetLabelText(_l_Simulation_Time, ComputeHours(simulation, ""));
                    ShowActorsInfo(((MySimulation)simulation).AMinibus.Minis, ((MySimulation)simulation).AEmployee.Employees);
                }));
            }
            else
            {
                SetLabelText(_l_Simulation_Time, ComputeHours(simulation, ""));
                ShowActorsInfo(((MySimulation)simulation).AMinibus.Minis, ((MySimulation)simulation).AEmployee.Employees);
            }
        }

        private string ComputeHours(Simulation simulation, string format)
        {
            if (Double.IsNaN(simulation.CurrentTime))
                return "0";
            TimeSpan t = TimeSpan.FromSeconds(simulation.CurrentTime);
            switch (format)
            {
                case "HH:mm:ss":
                    return t.ToString(@"hh\:mm\:ss");
                default:
                    return String.Format("Replication: {0} Day: {1} Time: {2}", simulation.CurrentReplication + 1, ((MySimulation)simulation).AAirport.ActualDay, t.ToString(@"hh\:mm\:ss"));
            }
        }


        private void btnStop_Click(object sender, EventArgs e)
        {

        }

        private void btnPause_Click(object sender, EventArgs e)
        {

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
    }
}
