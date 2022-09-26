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
                {"UN43",887276400037},
                {"DEMO",000000000000}
            };

            var remoteCommands = new HashSet<string>() {
                "power","source","ch+","ch-","vol+","vol-",
                "mute","menu","info","last","settings",
                "remote","help","q","run demo"
            };

            string command;

            string key = ModelInput(TUbrochure);
            TU7000 screen = Factory(key, TUbrochure);
            Remote remote = new Remote(Publisher(screen, key));

            ViewManual();

            do
            {
                Thread.Sleep(2000);
                ViewRemote();
                command = RemoteCommand(remoteCommands);
                if (command == "q") continue;
                else if (command == "help") ViewManual();
                else if (command == "remote") ViewRemote();
                else remote.Command(command);
            } while (command != "q");
        }

        static public void ViewRemote()
        {
            Console.WriteLine("|------------------------------|");
            Console.WriteLine("|[  power ]          [ source ]|");
            Console.WriteLine("|[   1    ][    2   ][    3   ]|");
            Console.WriteLine("|[   4    ][    5   ][    6   ]|");
            Console.WriteLine("|[   7    ][    8   ][    9   ]|");
            Console.WriteLine("|[  menu  ][    0   ][  info  ]|");
            Console.WriteLine("|[    +   ][  mute  ][    +   ]|");
            Console.WriteLine("|[   vol  ][  last  ][   ch   ]|");
            Console.WriteLine("|[    -   ][settings][    -   ]|");
            Console.WriteLine("|------------------------------|");
            Console.WriteLine("|[  help  ]          [    q   ]|");
        }

        static public void ViewManual()
        {
            Console.WriteLine("\n--- Instructions for Remote ---");
            Console.WriteLine("Enter...");
            Console.WriteLine("...'power' to execute that button");
            Console.WriteLine("...'source' to execute that button");
            Console.WriteLine("... a number within 1-999 inclusive to switch to that channel");
            Console.WriteLine("...'vol+' to increase volume by 1 & 'vol-' to decrease volume by 1");
            Console.WriteLine("...'ch+' to increase channel by 1 & 'ch-' to decrease channel by 1");
            Console.WriteLine("...'mute' to mute the volume & unmute if the volume is already muted");
            Console.WriteLine("...'last' will change the channel to its last channel state prior to the current channel");
            Console.WriteLine("...'menu' will launch the screen menu that is specific to your model");
            Console.WriteLine("...'help' will show this operational manual");
            Console.WriteLine("...'q' will quit this program\n");
            Console.Write("....Press the enter button to continue");
            Console.ReadLine();
        }

        static public SignalHandler Publisher(TU7000 screen, string model)
        { /* Subscribers: instantiated TU7000 models & its functional features implemented by its methods
             Broadcaster: delegate (signal) for the instantiated remote */
            SignalHandler signal = screen.Power;
            signal += screen.Source;
            signal += screen.Mute;
            signal += screen.Volume;
            signal += screen.Channel;
            signal += screen.Last;
            signal += screen.Info;

            switch (model)
            {
                case "DEMO": /* Subscribers unique to Demo Model */
                    Demo demo = (Demo)screen;
                    signal += demo.RunDemo;
                    signal += demo.Menu;
                    signal += demo.Settings;
                    signal += demo.ModelInfo;
                    break;
                case "UN75": /* Subscribers unique to UN75 Model */
                    UN75 un75 = (UN75)screen;
                    signal += un75.Menu;
                    signal += un75.Settings;
                    signal += un75.ModelInfo;
                    break;
                case "UN70": /* Subscribers unique to UN70 Model */
                    UN70 un70 = (UN70)screen;
                    signal += un70.Menu;
                    signal += un70.Settings;
                    signal += un70.ModelInfo;
                    break;
                case "UN65": /* Subscribers unique to UN65 Model */
                    UN65 un65 = (UN65)screen;
                    signal += un65.Menu;
                    signal += un65.Settings;
                    signal += un65.ModelInfo;
                    break;
                case "UN58": /* Subscribers unique to UN58 Model */
                    UN58 un58 = (UN58)screen;
                    signal += un58.Menu;
                    signal += un58.Settings;
                    signal += un58.ModelInfo;
                    break;
                case "UN55": /* Subscribers unique to UN55 Model */
                    UN55 un55 = (UN55)screen;
                    signal += un55.Menu;
                    signal += un55.Settings;
                    signal += un55.ModelInfo;
                    break;
                case "UN50": /* Subscribers unique to UN50 Model */
                    UN50 un50 = (UN50)screen;
                    signal += un50.Menu;
                    signal += un50.Settings;
                    signal += un50.ModelInfo;
                    break;
                case "UN43": /* Subscribers unique to UN43 Model */
                    UN43 un43 = (UN43)screen;
                    signal += un43.Menu;
                    signal += un43.Settings;
                    signal += un43.ModelInfo;
                    break;
            }

            return signal;
        }

        static public TU7000 Factory(string key, Dictionary<string, long> TUbrochure)
        {
            switch (key)
            {
                case "UN75":
                    return new UN75(TUbrochure[key], key);
                case "UN70":
                    return new UN70(TUbrochure[key], key);
                case "UN65":
                    return new UN65(TUbrochure[key], key);
                case "UN58":
                    return new UN58(TUbrochure[key], key);
                case "UN55":
                    return new UN55(TUbrochure[key], key);
                case "UN50":
                    return new UN50(TUbrochure[key], key);
                case "UN43":
                    return new UN43(TUbrochure[key], key);
                default:
                    return new Demo(0000000000, "Demo");
            }
        }

        static public string ModelInput(Dictionary<string, long> TU7000)
        {
            string key;
            bool result = false;

            do
            {
                Console.WriteLine("TU7000 Models: [UN75],[UN70],[UN65],[UN58],[UN55],[UN50],[UN43]");
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