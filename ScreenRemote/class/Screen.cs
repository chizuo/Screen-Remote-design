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
        protected string source;
        protected Queue<string> sourceControl = new Queue<string>(new[] { "antenna", "HDMI1", "HDMI2", "HDMI3", "HDMI4" });
        protected long upcNumber;
        protected int serialNumber;
        protected string model;
        protected static int maxChannel;
        protected static int maxVolume;
        protected static int minChannel;
        protected static int minVolume;

        public TU7000(long upc, string name)
        {
            upcNumber = upc;
            serialNumber = new Random().Next(1000000, 9999999);
            model = name;
            source = "antenna";
            /* default state of a new TU7000 series television */
            power = false;
            mute = false;
            channel = 3;
            volume = 50;
            lastChannel = channel;
        }

        static TU7000()
        {
            maxChannel = 999;
            maxVolume = 100;
            minChannel = 1;
            minVolume = 0;
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
                var prevState = this.sourceControl.Dequeue();
                this.DisplayScreen("Source: " + this.source + " --> " + this.sourceControl.Peek());
                this.sourceControl.Enqueue(prevState);
                this.source = this.sourceControl.Peek();
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
                    this.volume = this.volume < maxVolume ? ++this.volume : this.volume;

                if (volumeDOWN)
                    this.volume = this.volume > minVolume ? --this.volume : this.volume;

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
                    if (num <= maxChannel && num >= minChannel)
                    {
                        this.lastChannel = this.channel;
                        this.channel = num;
                        this.source = "antenna";
                        this.DisplayScreen("Channel: " + command);
                    }
                    else { this.DisplayScreen("Error: Beyond Channel Range of Model"); }
                }
                else if (command.Equals("ch+"))
                {
                    if (this.channel < maxChannel)
                    {
                        this.lastChannel = this.channel;
                        this.channel++;
                        this.source = "antenna";
                        this.DisplayScreen("Channel: " + this.channel.ToString());
                    }
                    else { this.DisplayScreen("Channel: " + maxChannel.ToString() + " is the max range"); }
                }
                else if (command.Equals("ch-"))
                {
                    if (this.channel > minChannel)
                    {
                        this.lastChannel = this.channel;
                        this.channel--;
                        this.source = "antenna";
                        this.DisplayScreen("Channel: " + this.channel.ToString());
                    }
                    else { this.DisplayScreen("Channel: " + minChannel.ToString() + " is the minumum range"); }
                }
            }
            else { if (!command.Equals("power")) this.DisplayScreen("Power: Off, turn on the TV."); }
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
                Console.WriteLine("Source: {0}", this.source);
                Console.WriteLine("Channel: {0}", this.channel);
                Console.WriteLine("Volume: {0}", this.volume);
                Console.WriteLine("Mute: {0}", this.mute);
                Console.WriteLine("Previous Channel: {0}", this.lastChannel);
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
            Console.WriteLine("        _||_                                   _||_\n");
        }

        public abstract void Menu(string command);

        public abstract void Settings(string command);

        public abstract void ModelInfo(string command);
    }
}