using System;

namespace ChainedEvents
{
    public delegate void myEventHandler(string value);

    class EventPublisher
    {
        private string theVal;

        public event myEventHandler valueChanged;
        public event EventHandler<ObjChangeEventArgs> objChanged;

        public string Val
        {
            set
            {
                this.theVal = value;
                this.valueChanged(theVal);
                this.objChanged(this, new ObjChangeEventArgs() { propChanged = "Val" });
            }
        }
    }

    class ObjChangeEventArgs : EventArgs
    {
        public string propChanged;
    }

    class Program
    {
        static void Main(string[] args)
        {
            EventPublisher obj = new EventPublisher();

            obj.valueChanged += changeListener1;
            obj.valueChanged += changeListener2;

            obj.valueChanged += delegate (string s)
            {
                Console.WriteLine($"This came from the anonymous handler! {s}");
            };

            obj.objChanged += delegate (object sender, ObjChangeEventArgs e)
            {
                Console.WriteLine($"{sender.GetType()} had the '{e.propChanged}' property changed");
            };

            string str;
            do
            {
                Console.WriteLine("Enter a value: ");
                str = Console.ReadLine();
                if (!str.Equals("exit"))
                {
                    obj.Val = str;
                }
            }
            while (!str.Equals("exit"));

            Console.WriteLine("Goodbye!");
        }

        static void changeListener1(string value)
        {
            Console.WriteLine($"The value changed to {value}");
        }

        static void changeListener2(string value)
        {
            Console.WriteLine($"I also listen to the event, and got {value}");
        }
    }
}
