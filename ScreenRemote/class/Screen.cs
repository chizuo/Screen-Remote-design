using System;

namespace ScreenRemote
{
    public class TU7000 : TM124A
    {
        protected bool power;
        protected bool mute;
        protected int channel;
        protected int volume;
        protected int lastChannel;
        protected Queue<string> source = new Queue<string>(new[] { "antenna", "HDMI1", "HDMI2", "HDMI3", "HDMI4" });
        protected long upcNumber;
        protected int serialNumber;
        protected string model;
        protected int maxChannel = 999;
        protected int maxVolume = 100;
        protected int minChannel = 1;
        protected int minVolume = 0;

        public TU7000(long upc, string name)
        {
            upcNumber = upc;
            serialNumber = new Random().Next(1000000, 9999999);
            model = name;
            /* default state of a new TU7000 series television */
            power = false;
            mute = false;
            channel = 3;
            volume = 50;
            lastChannel = channel;
        }

        public void Power(string command)
        {
            if (command.Equals("power"))
            {
                this.power = !this.power ? true : false;
                this.DisplayScreen(this.power ? "Power: On" : "Power: Off");
            }
        }

        public void Source(string command)
        {
            if (command.Equals("source"))
            {
                var sourceState = this.source.Dequeue();
                this.DisplayScreen("Source: " + sourceState.ToString() + " --> " + source.Peek());
                this.source.Enqueue(sourceState);
            }

        }

        public void Mute(string command)
        {
            if (command.Equals("mute"))
            {
                this.mute = !this.mute ? true : false;
                string info = this.mute ? "Volume: MUTE" : "Volume: " + this.volume.ToString();
                this.DisplayScreen(info);
            }
        }

        public void Volume(string command)
        {
            bool volumeUP = command.Equals("vol+");
            bool volumeDOWN = command.Equals("vol-");
            if (volumeUP || volumeDOWN)
            {
                if (volumeUP)
                    this.volume = this.volume < this.maxVolume ? ++this.volume : this.volume;

                if (volumeDOWN)
                    this.volume = this.volume > this.minVolume ? --this.volume : this.volume;

                this.mute = false;
                this.DisplayScreen("Volume: " + this.volume.ToString());
            }
        }

        public void Channel(string command)
        {
            if (int.TryParse(command, out int num))
            {
                if (num <= this.maxChannel && num >= this.minChannel)
                {
                    this.lastChannel = this.channel;
                    this.channel = num;
                    this.DisplayScreen("Channel: " + command);
                }
                else { this.DisplayScreen("Error: Beyond Channel Range of Model"); }
            }
            else if (command.Equals("ch+"))
            {
                if (this.channel < this.maxChannel)
                {
                    this.lastChannel = this.channel;
                    this.channel++;
                    this.DisplayScreen("Channel: " + this.channel.ToString());
                }
                else { this.DisplayScreen("Channel: " + this.maxChannel.ToString() + " is the max range"); }
            }
            else if (command.Equals("ch-"))
            {
                if (this.channel > this.minChannel)
                {
                    this.lastChannel = this.channel;
                    this.channel--;
                    this.DisplayScreen("Channel: " + this.channel.ToString());
                }
                else { this.DisplayScreen("Channel: " + this.minChannel.ToString() + " is the minumum range"); }
            }
        }

        public virtual void Menu(string command)
        {
            if (command.Equals("Menu"))
            {
                Console.WriteLine("Demo Menu for {0}", this.upcNumber);
            }
        }

        public virtual void Settings(string command)
        {
            if (command.Equals("settings"))
            {
                Console.WriteLine("Demo Settings for {0}", this.upcNumber);
            }
        }

        public void Info(string command)
        {
            if (command.Equals("info"))
            {
                Console.WriteLine("\n****** State of {0} ******", this.model);
                Console.WriteLine("UPC# {0} | Serial# {1}", this.upcNumber, this.serialNumber);
                Console.WriteLine(this.power ? "Power: On" : "Power: Off");
                Console.WriteLine("Source: {0}", this.source.Peek());
                Console.WriteLine("Channel: {0}", this.channel);
                Console.WriteLine("Volume: {0}", this.volume);
            }
        }


        public void DisplayScreen(string info)
        {
            Console.Clear();
            Console.WriteLine(".---..-----------------------------------------------..---.");
            Console.WriteLine("|   ||.---------------------------------------------.||   |");
            Console.WriteLine("| o |||                                             ||| o |");
            Console.WriteLine("| _ |||                                             ||| _ |");
            Console.WriteLine("|(_)|||                                             |||(_)|");
            Console.WriteLine("|   |||                                             |||   |");
            Console.Write("|   ||| ");
            Console.Write(info.PadRight(44));
            Console.WriteLine("|||   |");
            Console.WriteLine("|.-.|||                                             |||.-.|");
            Console.WriteLine("| o |||                                             ||| o |");
            Console.WriteLine("|`-'|||                                             |||`-'|");
            Console.WriteLine("|   |||                                             |||   |");
            Console.WriteLine("|.-.|||                                             |||.-.|");
            Console.WriteLine("| O |||                                             ||| O |");
            Console.WriteLine("|`-'||`---------------------------------------------'||`-'|");
            Console.WriteLine("`---'`-----------------------------------------------'`---'");
            Console.WriteLine("        _||_                                   _||_");
        }
    }

}
