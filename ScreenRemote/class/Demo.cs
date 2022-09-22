namespace ScreenRemote
{
    public class Demo : TU7000
    {
        public Demo(long upc, string name) : base(upc, name)
        {
            upcNumber = upc;
            serialNumber = 0000000000;
            model = name;
            /* default state of a new TU7000 series television */
            power = false;
            mute = false;
            channel = 3;
            volume = 50;
            lastChannel = channel;
        }

        public override void Menu(string command)
        {
            if (command.Equals("menu"))
            {
                this.DisplayScreen("Menu: disabled for demo models");
            }
        }

        public override void Settings(string command)
        {
            if (command.Equals("settings"))
            {
                this.DisplayScreen("Settings: disabled for demo models");
            }
        }

        public void RunDemo(string command)
        {
            if (command.Equals("run demo"))
            {
                this.Power("power");
                Console.WriteLine("Pressed [power]");
                Thread.Sleep(1500);
                this.Channel("586");
                Console.WriteLine("Pressed [5][8][6]");
                Thread.Sleep(1500);
                this.Channel("ch+");
                Console.WriteLine("Pressed [ch+]");
                Thread.Sleep(1500);
                this.Channel("ch-");
                Console.WriteLine("Pressed [ch-]");
                Thread.Sleep(1500);
                this.Volume("vol+");
                Console.WriteLine("Pressed [vol+]");
                Thread.Sleep(1500);
                this.Mute("mute");
                Console.WriteLine("Pressed [mute]");
                Thread.Sleep(1500);
                this.Mute("mute");
                Console.WriteLine("Pressed [mute]");
                Thread.Sleep(1500);
                this.Mute("mute");
                Console.WriteLine("Pressed [mute]");
                Thread.Sleep(1500);
                this.Volume("vol-");
                Console.WriteLine("Pressed [vol-]");
                Thread.Sleep(1500);
                this.Power("power");
                Console.WriteLine("Pressed [power]");
                Thread.Sleep(1500);
                this.Power("power");
                Console.WriteLine("Pressed [power]");
                Thread.Sleep(1500);
                this.Source("source");
                Console.WriteLine("Pressed [source]");
                Thread.Sleep(1500);
                this.Source("source");
                Console.WriteLine("Pressed [source]");
                Thread.Sleep(1500);
                this.Source("source");
                Console.WriteLine("Pressed [source]");
                Thread.Sleep(1500);
                this.Source("source");
                Console.WriteLine("Pressed [source]");
                Thread.Sleep(1500);
                this.Source("source");
                Console.WriteLine("Pressed [source]");
                Thread.Sleep(1500);
                this.Power("power");
                Console.WriteLine("Pressed [power]");
                Thread.Sleep(1500);
                this.Source("source");
                Console.WriteLine("Pressed [source] ... source state shouldn't change since screen is off.");
                Thread.Sleep(1500);
                this.Info("info");
            }
        }
    }
}