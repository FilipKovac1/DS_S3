using OSPABA;
using Actors;

namespace simulation
{
	public class MyMessage : MessageForm
	{
        private Passenger Passenger { get; set; }
        private Minibus Minibus { get; set; }
        private Employee Employee { get; set; }

		public MyMessage(Simulation sim) :
			base(sim)
		{
		}

		public MyMessage(MyMessage original) :
			base(original)
		{
			// copy() is called in superclass
		}

        public override MessageForm CreateCopy()
		{
			return new MyMessage(this);
		}

        protected override void Copy(MessageForm message)
		{
			base.Copy(message);
			MyMessage original = (MyMessage)message;
			// Copy attributes
		}
	}
}