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
            if (command.Equals("Menu"))
            {
                this.DisplayScreen("Menu: disabled for demo models");
            }
        }

        public override void Settings(string command)
        {
            if (command.Equals("Settings"))
            {
                this.DisplayScreen("Settings: disabled for demo models");
            }
        }

        public void RunDemo(string command)
        {
            if (command.Equals("run demo"))
            {
                this.Power("power");
                Thread.Sleep(1500);
                this.Channel("586");
                Thread.Sleep(2000);
                this.Channel("ch+");
                Thread.Sleep(2000);
                this.Channel("ch-");
                Thread.Sleep(2000);
                this.Volume("vol+");
                Thread.Sleep(2000);
                this.Mute("mute");
                Thread.Sleep(2000);
                this.Mute("mute");
                Thread.Sleep(2000);
                this.Mute("mute");
                Thread.Sleep(2000);
                this.Volume("vol-");
                Thread.Sleep(2000);
                this.Power("power");
                Thread.Sleep(2000);
                this.Power("power");
                Thread.Sleep(2000);
                this.Source("source");
                Thread.Sleep(1500);
                this.Source("source");
                Thread.Sleep(1500);
                this.Source("source");
                Thread.Sleep(1500);
                this.Source("source");
                Thread.Sleep(1500);
                this.Source("source");
                Thread.Sleep(1500);
                this.Source("source");
                Thread.Sleep(1500);
                this.Info("info");
            }
        }
    }
}