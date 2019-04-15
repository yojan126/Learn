using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelegateTest
{
    class Test2
    {
        public static void DoThis()
        {
            Car car = new Car(15);
            new Alerter(car);
            car.Run(120);
        }
    }

    class Car
    {
        public delegate void NotifyEventHandler(int p);
        public event NotifyEventHandler Notifier;

        private int petrol = 0;
        public int Petrol
        {
            get { return petrol; }
            set
            {
                petrol = value;
                if (petrol < 10)
                {
                    if (Notifier != null)
                    {
                        Notifier.Invoke(Petrol);
                        // Notifier(Petrol);    与event.invoke等效
                    }
                }
            }
        }

        public Car(int p)
        {
            Petrol = p;
        }

        public void Run(int speed)
        {
            int Distance = 0;
            while (Petrol > 0)
            {
                Thread.Sleep(500);
                Petrol--;
                Distance += speed;
                Console.WriteLine("Car is running...... Distance is " + Distance.ToString());
            }
        }
    }

    class Alerter
    {
        public Alerter(Car car)
        {
            car.Notifier += new Car.NotifyEventHandler(NotEnoughPetrol);
            car.Notifier += new Car.NotifyEventHandler(SoundNotify);
        }

        public void NotEnoughPetrol(int p)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You only have " + p.ToString() + " gallon petrol left");
            Console.ResetColor();
        }

        public void SoundNotify(int p)
        {
            if (p < 5)
            {
                Console.WriteLine("BIBIBIBI");
            }
        }
    }
}
