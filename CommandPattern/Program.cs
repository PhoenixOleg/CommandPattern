namespace CommandPattern
{
    internal class Program
    {
        static void Main()
        {
            Driver driver = new Driver();

            Auto auto = new Auto();
            Transmission transmission = new Transmission();

            driver.SetCommandEngine(auto);

            driver.SetCommandTransmission (transmission);

            driver.TurnOn();

            driver.GearUp();
            driver.GearUp();
            driver.GearUp();
            driver.GearDown ();
            driver.GearDown();
            driver.GearUp();
            driver.GearDown(); 
            driver.GearDown(); //Нейтраль
            //Еще раз воткнули нейтраль)))

            driver.TurnOff();

            Console.ReadKey();
        }
    }

    public interface ICommand
    {
        public void Execute();

        public void Undo();
    }

    public class Auto //Авто. Он же Receiver - Получатель
    {
        //private int _gear = 0;

        public void TurnOn()
        {
            Console.WriteLine("Машина заведена");
        }

        public void TurnOff()
        {
            Console.WriteLine("Зажигание выключено");
        }

        /*
         * Не понимаб как релизовать много команд в одном классе. Поэтому завел класс Transmission
         * Но что делать если  объект реально поддерживает более двух команд???
        public void GearUp()
        {
            _gear += 1;
            Console.WriteLine($"Включена передача {_gear}");
        }

        public void GearDown()
        {
            if (_gear > 0)
            {
                _gear -= 1;
                Console.WriteLine($"Включена передача {_gear}");
            }
        }
        */
    }

    public class Transmission //Ручная КПП. Еще один Reciever
    {
        private int _gear = 0;

        public void GearUp()
        {
            _gear += 1;
            Console.WriteLine($"Включена передача {_gear}");
        }

        public void GearDown()
        {
            if (_gear > 0)
            {
                _gear -= 1;
                Console.WriteLine($"Включена передача {_gear}");
            }
        }
    }

    public class AutoCommand : ICommand //Класс машины - включение и выключение зажигания
    {
        private Auto _auto;

        public AutoCommand( Auto auto )
        {
            _auto = auto;
        }

        public void Execute()
        {
            _auto.TurnOn();
        }

        public void Undo() 
        { 
            _auto.TurnOff();
        }
    }

    public class TransmissionCommand : ICommand
    {
        private Transmission _transmission;

        public void Execute()
        {
            _transmission.GearUp();
        }

        public void Undo()
        {
            _transmission.GearDown();
        }
    }

    public class Driver //Водитель. Он же Sender - Отправитель
    {
        Auto _auto;
        Transmission _transmission;

        public void SetCommandEngine (Auto auto)
        {
            _auto = auto;
        }

        public void SetCommandTransmission(Transmission transmission)
        {
            _transmission = transmission;
        }

        public void TurnOn()
        {
            _auto.TurnOn();
        }

        public void TurnOff()
        {
            _auto.TurnOff ();
        }

        public void GearUp()
        {
            _transmission.GearUp ();
        }

        public void GearDown()
        {
            _transmission.GearDown ();  
        }
    }
}
