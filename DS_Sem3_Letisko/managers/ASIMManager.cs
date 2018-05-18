using OSPABA;
using simulation;
using agents;
namespace managers
{
    //meta! id="1"
    public class ASimManager : Manager
    {
        public ASimManager(int id, Simulation mySim, Agent myAgent) : base(id, mySim, myAgent) => Init();

        public override void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication

            if (PetriNet != null)
            {
                PetriNet.Clear();
            }
        }

        private void ProcessEnter(MessageForm message)
        {
            message.Code = Mc.ServePassenger;
            message.Addressee = MySim.FindAgent(SimId.AAirport);
            Request(message);
        }

        //meta! sender="AEnv", id="13", type="Notice"
        public void ProcessEnterT1(MessageForm message)
        {
            ProcessEnter(message);
        }

        //meta! sender="AEnv", id="12", type="Notice"
        public void ProcessEnterCR(MessageForm message)
        {
            ProcessEnter(message);
        }

        //meta! sender="AEnv", id="11", type="Notice"
        public void ProcessEnterT2(MessageForm message)
        {
            ProcessEnter(message);
        }

        //meta! sender="AAirport", id="19", type="Response"
        public void ProcessServePassenger(MessageForm message)
        {
            message.Code = ((MyMessage)message).Passenger.ArrivedAt < 3 ? Mc.LeaveCR : Mc.LeaveT3;
            message.Addressee = MySim.FindAgent(SimId.AEnv);
            Notice(message);
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.Init:
                    // start planning enters
                    message.Addressee = MySim.FindAgent(SimId.AEnv);
                    Notice(message);
                    break;
            }
        }

        //meta! sender="AAirport", id="17", type="Notice"
        public void ProcessResetStat(MessageForm message)
        {
            message.Addressee = MySim.FindAgent(SimId.AEnv);
            Notice(message);
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        public void Init()
        {
        }

        public override void ProcessMessage(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.EnterT1:
                    ProcessEnterT1(message);
                    break;

                case Mc.EnterT2:
                    ProcessEnterT2(message);
                    break;

                case Mc.ResetStat:
                    ProcessResetStat(message);
                    break;

                case Mc.EnterCR:
                    ProcessEnterCR(message);
                    break;

                case Mc.ServePassenger:
                    ProcessServePassenger(message);
                    break;

                default:
                    ProcessDefault(message);
                    break;
            }
        }
        //meta! tag="end"
        public new ASim MyAgent
        {
            get
            {
                return (ASim)base.MyAgent;
            }
        }
    }
}