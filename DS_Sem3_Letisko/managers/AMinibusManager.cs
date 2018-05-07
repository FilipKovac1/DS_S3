using OSPABA;
using simulation;
using agents;
using Actors;
using System;
using System.Linq;

namespace managers
{
    //meta! id="4"
    public class AMinibusManager : Manager
    {
        private bool Stop = false;
        public AMinibusManager(int id, Simulation mySim, Agent myAgent) : base(id, mySim, myAgent) => Init();

        public override void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication

            if (PetriNet != null)
            {
                PetriNet.Clear();
            }

            Stop = false;
        }

        //meta! sender="AAirport", id="25", type="Notice"
        public void ProcessResetStat(MessageForm message)
        { // reset after heat up 
            foreach (Minibus m in MyAgent.Minis) // just reset mileage to compute costs
                m.MileAge = 0;

            MyAgent.ResetStats();
        }

        /*!
		 * move of bus
		 */
        //meta! sender="AAirport", id="21", type="Request"
        public void ProcessMove(MessageForm message)
        {
            Stop = false;
            message.Addressee = MyAgent.FindAssistant(SimId.Transport);
            StartContinualAssistant(message);
        }

        private void ProcessEndMove(MessageForm message)
        {
            message.Code = Mc.Move;
            message.Addressee = MySim.FindAgent(SimId.AAirport);
            Response(message); // response of move
        }

        /*!
		 * Request - Enter to front to wait for a bus
		 * Response - leave a bus
		 */
        //meta! sender="AAirport", id="58", type="Request"
        public void ProcessProcessPassenger(MessageForm message)
        {
            message.Addressee = MyAgent.FindAssistant(SimId.AddToFront);
            Execute(message);
        }

        //meta! sender="Transport", id="109", type="Finish"
        public void ProcessFinishTransport(MessageForm message)
        {
            // minibus arrived
            ((MyMessage)message).Minibus.OnWay = false;
            if (!Stop)
            {
                switch (((MyMessage)message).Minibus.State)
                {
                    case 1: // get in
                    case 2:
                        if (MyAgent.IsEmpty(((MyMessage)message).Minibus.State) || ((MyMessage)message).Minibus.IsFull())
                            ProcessEndMove(message);
                        else
                            StartProcessGetIn(message);
                        break;
                    case 3: // here is stil someone
                        StartProcessGetOut(message);
                        break;
                    case 4:
                        if (((MyMessage)message).Minibus.IsEmpty())
                            if (MyAgent.IsEmpty(4))
                                ProcessEndMove(message);
                            else
                                StartProcessGetIn(message);
                        else
                            StartProcessGetOut(message);
                        break;
                }
            }
            else
            {
                message.Code = Mc.Stop;
                message.Addressee = MySim.FindAgent(SimId.AAirport);
                message.MsgResult = MyAgent.Minis.Where(b => b.OnWay && b.IsEmpty()).Count(); // send the number of busses in move to indicate that all of them stopped
                Response(message);
            }
        }

        //meta! sender="GetIn", id="39", type="Finish"
        public void ProcessFinishGetIn(MessageForm message)
        {
            ProcessEndMove(message);
        }

        private void StartProcessGetOut(MessageForm message)
        {
            message.Addressee = MyAgent.FindAssistant(SimId.GetOut);
            StartContinualAssistant(message);
        }

        private void StartProcessGetIn(MessageForm message)
        {
            ((MyMessage)message).Passenger = null;
            message.Addressee = MyAgent.FindAssistant(SimId.GetIn);
            StartContinualAssistant(message);
        }

        //meta! sender="GetOut", id="41", type="Finish"
        public void ProcessFinishGetOut(MessageForm message)
        {
            if (((MyMessage)message).Passenger != null)
            {
                message.Code = Mc.ProcessPassenger;
                message.Addressee = MySim.FindAgent(SimId.AAirport);
                Response(message);
            }
            else
            {
                switch (((MyMessage)message).Minibus.State)
                {
                    case 3: // go to next station
                        ProcessEndMove(message);
                        break;
                    case 4:
                        if (MyAgent.IsEmpty(4))
                            ProcessEndMove(message); // move to another stop
                        else 
                            StartProcessGetIn(message); // start get in
                        break;
                }
            }
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

        private void ProcessStop(MessageForm message) => Stop = true;

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
                    ProcessStop(message);
                    break;

                case Mc.Finish:
                    switch (message.Sender.Id)
                    {
                        case SimId.GetOut:
                            ProcessFinishGetOut(message);
                            break;

                        case SimId.Transport:
                            ProcessFinishTransport(message);
                            break;

                        case SimId.GetIn:
                            ProcessFinishGetIn(message);
                            break;
                    }
                    break;

                case Mc.ResetStat:
                    ProcessResetStat(message);
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
        public new AMinibus MyAgent
        {
            get
            {
                return (AMinibus)base.MyAgent;
            }
        }
    }
}
