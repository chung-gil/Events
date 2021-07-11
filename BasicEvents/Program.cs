using System;

namespace BasicEvents
{
    public delegate void myEventHandler(string value);

    class EventPublisher
    {
        private string theVal;

        public event myEventHandler valueChanged;

        public string Val
        {
            set
            {
                this.theVal = value;
                this.valueChanged(theVal);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EventPublisher obj = new EventPublisher();
            //obj.valueChanged += obj_valueChanged;
            obj.valueChanged += delegate (string val)
            {
                Console.WriteLine($"The value changed to {val}");
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

        static void obj_valueChanged(string value)
        {
            Console.WriteLine($"The value changed to {value}");
        }

    }
}
