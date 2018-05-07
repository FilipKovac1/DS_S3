using OSPABA;
using simulation;
using agents;
using Actors;
using System;

namespace managers
{
    //meta! id="3"
    public class AAirportManager : Manager
    {
        public AAirportManager(int id, Simulation mySim, Agent myAgent) : base(id, mySim, myAgent) => Init();

        public override void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication

            if (PetriNet != null)
            {
                PetriNet.Clear();
            }
        }

        private void ProcessInit(MessageForm message)
        {
            if (MyAgent.ActualDay != 0)
            {
                foreach (Minibus m in ((AMinibus)MySim.FindAgent(SimId.AMinibus)).Minis)
                {
                    MessageForm mess = message.CreateCopy();
                    mess.Code = Mc.Stop;
                    mess.Addressee = MySim.FindAgent(SimId.AMinibus);
                    ((MyMessage)mess).Minibus = m;
                    Request(mess);
                }
            }
            else
                ProcessStopBusses(message);
        }

        /// <summary>
        /// Next day or and of the replication
        /// </summary>
        /// <param name="message"></param>
        //meta! sender="ASim", id="18", type="Notice"
        public void ProcessStopBusses(MessageForm message)
        {
            if (message.MsgResult <= 0)
            {
                MyAgent.ActualDay++;
                if (MyAgent.ActualDay <= ((MySimulation)MySim).Repl_Days_C)
                {
                    MessageForm messResStatEmpl = message.CreateCopy();
                    messResStatEmpl.Code = Mc.ResetStat;
                    messResStatEmpl.MsgResult = 1;
                    messResStatEmpl.Addressee = MySim.FindAgent(SimId.AEmployee);
                    MessageForm messResStatMini = messResStatEmpl.CreateCopy();
                    messResStatMini.Addressee = MySim.FindAgent(SimId.AMinibus);
                    Notice(messResStatEmpl); Notice(messResStatMini);

                    ((MySimulation)MySim).AEnv.Generate = true;
                    double new_time = (MyAgent.ActualDay - 1) * Const.DayToSecond + MyAgent.DayStart - MyAgent.HeatUp.HeatUp;
                    if (MySim.CurrentTime < new_time)
                        MySim.CurrentTime = new_time;
                    else
                        MySim.StopSimulation();
                    if (MyAgent.HeatUp.HeatUp > 0)
                    { // end heat up
                        message.Addressee = MyAgent.FindAssistant(SimId.EndHeatUp);
                        StartContinualAssistant(message);
                    }

                    Random r = new Random(Generator.Seed.GetSeed());
                    foreach (Minibus m in ((AMinibus)MySim.FindAgent(SimId.AMinibus)).Minis)
                    {
                        MessageForm mess = message.CreateCopy();
                        mess.Code = Mc.Move;
                        mess.Addressee = MySim.FindAgent(SimId.AMinibus);
                        m.Reinit(r.Next(4) + 1);
                        ((MyMessage)mess).Minibus = m;
                        Request(mess);
                    }

                    MessageForm mess2 = message.CreateCopy();
                    mess2.Code = Mc.Init;
                    mess2.Addressee = MySim.FindAgent(SimId.ASim);
                    Response(mess2);
                }
                else // end of replication after all of days happened
                    MySim.StopReplication();
            }
        }

        //meta! sender="ASim", id="19", type="Request"
        public void ProcessServePassengerASim(MessageForm message)
        {
            MyAgent.ServedInc();
            if (((MyMessage)message).Passenger.ArrivedAt < 3)
            { // go to bus station wait for bus
                message.Code = Mc.ProcessPassenger;
                message.Addressee = MySim.FindAgent(SimId.AMinibus);
                Request(message);
            }
            else // go to employee to serve him
            {
                message.Code = Mc.ServePassenger;
                message.Addressee = MySim.FindAgent(SimId.AEmployee);
                Request(message);
            }
        }

        //meta! sender="AEmployee", id="23", type="Response"
        public void ProcessServePassengerAEmployee(MessageForm message)
        {
            if (((MyMessage)message).Passenger.ArrivedAt < 3)
            { // go to hell
                MyAgent.ServedDec();
                message.Code = Mc.ServePassenger;
                message.Addressee = MySim.FindAgent(SimId.ASim);
                Notice(message);
            }
            else
            { // go to wait bus on CR to go to T3
                message.Code = Mc.ProcessPassenger;
                message.Addressee = MySim.FindAgent(SimId.AMinibus);
                Request(message);
            }
        }

        /*!
		 * Request - Enter to front to wait for a bus
		 * Response - leave a bus
		 */
        //meta! sender="AMinibus", id="58", type="Response"
        public void ProcessProcessPassenger(MessageForm message)
        {
            if (((MyMessage)message).Passenger.ArrivedAt < 3)
            { // go to employee to serve him
                message.Code = Mc.ServePassenger;
                message.Addressee = MySim.FindAgent(SimId.AEmployee);
                Request(message);
            }
            else
            { // end in the simulation for this passenger
                MyAgent.ServedDec();
                message.Code = Mc.ServePassenger;
                message.Addressee = MySim.FindAgent(SimId.ASim);
                Notice(message);
            }
        }

        /*!
		 * move of bus
		 */
        //meta! sender="AMinibus", id="21", type="Response"
        public void ProcessMove(MessageForm message)
        { // go to next stop
            message.Code = Mc.Move;
            message.Addressee = MySim.FindAgent(SimId.AMinibus);
            Request(message);
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
                default:
                    break;
            }
        }

        //meta! sender="EndHeatUp", id="102", type="Finish"
        public void ProcessFinishEndHeatUp(MessageForm message)
        { // end of the heat so reset stats
            if (((MySimulation)MySim).Slow)
                MySim.SetSimSpeed(((MySimulation)MySim).Slow_interval, ((MySimulation)MySim).Slow_duration);

            message.Code = Mc.ResetStat;
            message.Addressee = MySim.FindAgent(SimId.ASim);
            MessageForm m1 = message.CreateCopy();
            MessageForm m2 = message.CreateCopy();
            m1.Addressee = MySim.FindAgent(SimId.AEmployee);
            m2.Addressee = MySim.FindAgent(SimId.AMinibus);
            Notice(message); Notice(m1); Notice(m2);
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        public void Init()
        {
        }

        public override void ProcessMessage(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.Move:
                    ProcessMove(message);
                    break;
                case Mc.Stop:
                    ProcessStopBusses(message);
                    break;

                case Mc.ServePassenger:
                    switch (message.Sender.Id)
                    {
                        case SimId.ASim:
                            ProcessServePassengerASim(message);
                            break;

                        case SimId.AEmployee:
                            ProcessServePassengerAEmployee(message);
                            break;
                    }
                    break;

                case Mc.Init:
                    ProcessInit(message);
                    break;

                case Mc.Finish:
                    switch (message.Sender.Id)
                    {
                        case SimId.EndHeatUp:
                            ProcessFinishEndHeatUp(message);
                            break;
                    }
                    break;

                case Mc.ProcessPassenger:
                    ProcessProcessPassenger(message);
                    break;

                default:
                    ProcessDefault(message);
                    break;
            }
        }
        //meta! tag="end"
        public new AAirport MyAgent
        {
            get
            {
                return (AAirport)base.MyAgent;
            }
        }
    }
}