namespace ScreenRemote
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /********************************************
              Main is a representation of our hand & eyes 
             ********************************************/
            var TUbrochure = new Dictionary<string, long>() {
                {"UN75",887276400082},
                {"UN70",887276400075},
                {"UN65",887276400068},
                {"UN58",887276400051},
                {"UN55",887276400044},
                {"UN50",887276402147},
                {"UN43",887276400037}
            };

            var remoteCommands = new HashSet<string>() {
                "power","source","ch+","ch-","vol+","vol-",
                "mute","menu","info","last","settings",
                "remote","help","q"
                };

            string command;

            var ScreenRemotes = new Dictionary<Remote, TU7000>();

            //string key = ModelInput(TU7000);
            string key = "UN75";
            TU7000 screen = ModelScreen(key, TUbrochure);
            Remote remote = new Remote(Pairing(screen, key));

            ViewRemote();
            ViewManual();

            do
            {
                command = RemoteCommand(remoteCommands);
                //command = "power";
                if (command == "q") continue;
                else if (command == "help") ViewManual();
                else if (command == "remote") ViewRemote();
                else remote.Command(command);
            } while (command != "q");
        }

        static public void ViewRemote()
        {
            Console.WriteLine("\n|------------------------------|");
            Console.WriteLine("|[  power ]          [ source ]|");
            Console.WriteLine("|[   1    ][    2   ][    3   ]|");
            Console.WriteLine("|[   4    ][    5   ][    6   ]|");
            Console.WriteLine("|[   7    ][    8   ][    9   ]|");
            Console.WriteLine("|[  menu  ][    0   ][  info  ]|");
            Console.WriteLine("|[    +   ][  mute  ][    +   ]|");
            Console.WriteLine("|[   vol  ][  last  ][   ch   ]|");
            Console.WriteLine("|[    -   ][settings][    -   ]|");
            Console.WriteLine("|------------------------------|\n");
        }

        static public void ViewManual()
        {
            Console.WriteLine("\n--- Instructions for Remote ---");
            Console.WriteLine("Instructions: enter 'Power' to execute that button or 'Source' to execute that button");
            Console.WriteLine("Channel can be entered within range of 1-999 inclusive");
            Console.WriteLine("Increasing volume is 'vol+' & decreasing volume is 'vol-'");
            Console.WriteLine("Increase channel by 1 is 'ch+' & decrease channel by 1 is 'ch-'");
            Console.WriteLine("Entering 'mute', to set volume back to its pre-mute state, enter 'Mute' again");
            Console.WriteLine("Entering 'last' will change the channel to its last channel state prior to the current channel");
            Console.WriteLine("Entering 'menu' will launch the screen menu that is specific to your model");
            Console.WriteLine("--- Instructions to operate this program ---");
            Console.WriteLine("Entering 'remote' will display the remote");
            Console.WriteLine("Entering 'help' will show this operational manual");
            Console.WriteLine("Entering 'q' will quit this program\n");
        }

        static public SignalHandler Pairing(TU7000 screen, string model)
        {
            SignalHandler signal = screen.Power;
            signal += screen.Source;
            signal += screen.Mute;
            signal += screen.Volume;
            signal += screen.Channel;

            if (model.Equals("UN75"))
            {
                screen = (UN75)screen;
            }
            return signal;
        }

        static public TU7000 ModelScreen(string key, Dictionary<string, long> TUbrochure)
        {
            if (key.Equals("UN75")) { return new UN75(TUbrochure[key], key); }
            if (key.Equals("UN70")) { Console.WriteLine("UN70"); }
            if (key.Equals("UN65")) { Console.WriteLine("UN65"); }
            if (key.Equals("UN58")) { Console.WriteLine("UN58"); }
            if (key.Equals("UN55")) { Console.WriteLine("UN55"); }
            if (key.Equals("UN50")) { Console.WriteLine("UN50"); }
            if (key.Equals("UN43")) { Console.WriteLine("UN43"); }
            return new TU7000(0000000000, "Demo");
        }

        static public string ModelInput(Dictionary<string, long> TU7000)
        {
            string key;
            bool result = false;

            do
            {
                Console.WriteLine("TU7000 Models: [UN75][UN70][UN65][UN58[UN55][UN50][UN43]");
                Console.Write("Provide the model of screen for the TU7000 series: ");
                key = Console.ReadLine();
                key = key == null ? "" : key;
                result = TU7000.ContainsKey(key);
                if (!result) { Console.WriteLine("invalid model, try again."); }
            } while (!result);
            return key;
        }

        static public string RemoteCommand(HashSet<string> validCommands)
        {
            string command = "";
            bool valid;
            do
            {
                Console.Write("\nYou enter... ");
                command = Console.ReadLine();
                if (Int32.TryParse(command, out int val)) valid = true;
                else if (validCommands.Contains(command)) valid = true;
                else
                {
                    valid = false;
                    Console.WriteLine("{0} is not a valid remote command", command);
                }
            } while (!valid);
            return command;
        }
    }
}