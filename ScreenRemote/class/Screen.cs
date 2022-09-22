namespace ScreenRemote
{
    public abstract class TU7000 : TM124A
    { /* Abstract class TU7000 (the series of screen) implements interface TM124A (remote model). 
         The abstract methods represent the unique features for each model of TU7000 (inheritance/polymorphism).
         This approach leverages the Factory Design Pattern in the case Samsung wishes to expand the model line.*/
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
            if (command.Equals("source") && this.power == true)
            {
                var sourceState = this.source.Dequeue();
                this.DisplayScreen("Source: " + sourceState.ToString() + " --> " + source.Peek());
                this.source.Enqueue(sourceState);
            }
        }

        public void Mute(string command)
        {
            if (command.Equals("mute") && this.power == true)
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
            if ((volumeUP || volumeDOWN) && this.power == true)
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
            if (this.power == true)
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

        }

        public void Last(string command)
        {
            if (command.Equals("last") && this.power == true)
            {
                int temp = this.channel;
                this.channel = this.lastChannel;
                this.lastChannel = temp;
                this.DisplayScreen("Channel: " + this.channel.ToString());
            }
        }

        public void Info(string command)
        {
            if (command.Equals("info"))
            { /* Used to validate correctness of the state of the screen relative to remote commands pressed */
                Console.WriteLine("\n****** State of {0} ******", this.model);
                Console.WriteLine("UPC# {0} | Serial# {1}", this.upcNumber, this.serialNumber);
                Console.WriteLine(this.power ? "Power: On" : "Power: Off");
                Console.WriteLine("Source: {0}", this.source.Peek());
                Console.WriteLine("Channel: {0}", this.channel);
                Console.WriteLine("Volume: {0}", this.volume);
                Console.WriteLine("Mute: {0}", this.mute);
                Console.WriteLine("Previous Channel: {0}", this.lastChannel);
                Console.WriteLine("****************************\n");
                Console.WriteLine("Press enter to exit the info screen...");
                Console.ReadLine();
            }
        }

        public void DisplayScreen(string info)
        {
            Console.Clear();
            Console.WriteLine("\n.---..-----------------------------------------------..---.");
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

        public abstract void Menu(string command);

        public abstract void Settings(string command);
    }

}