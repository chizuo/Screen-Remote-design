# COMP586 Objected Oriented Software Development
In fulfillment of project 1 during Fall 2022
<br><br>
## Description
This appliciation is a system design implementation of a screen & remote. Using the TU7000 series of Samsung Televisions as a model of a screen product line, the system was designed using the Factory design pattern to address this commonly occurring problem.
<br><br>
## Class
- `class Program` in Program.cs
  - `void static Main(string[] args)` : driver of the application.
  - `static void ViewRemote()` : provides the user a visualization of a remote and possible remote commands.
  - `static void ViewManual()` : provides the user documentation what each command accomplishes.
  - `static SignalHandler Publisher(TU7000 screen, string model)` : facilitates the pairing for the broadcaster | subscriber model.
  - `static TU7000 Factory(string key, Dictionary TUbrochure)` : facilitates the production of the TU7000 series of screens.
  - `static ModelInput(Dictionary TUbrochure)` : User prompt regarding which series model of TU7000 to produce.
  - `static RemoteCommand(HashSet validCommands)` : User prompt regarding which remote command to execute towards the TU7000 model screen.
- `class Remote` in Remote.cs
  - `void Command(string command` : calls the delegate to broadcast the command to the remote's paired screen.
- `abstract class TU7000` in Screen.cs inherits `interface TM124A` in IRemote.cs
  - `void DisplayScreen(string info)` : visualization of the screen responding to the User's remote commands.
  - `abstract void Menu(string command)` : should implement the Smart Menu of a particular TU7000 model.
  - `abstract void Settings(string command)` : should implement the T.V. Settings of a particular TU7000 model.
  - `abstract void ModelInfo(string command)` : should implement the Model Information of a particular TU7000 model.
- `class UN43` in UN43.cs inherits `TU7000` in Screen.cs
- `class UN50` in UN50.cs inherits `TU7000` in Screen.cs
- `class UN55` in UN55.cs inherits `TU7000` in Screen.cs
- `class UN58` in UN58.cs inherits `TU7000` in Screen.cs
- `class UN65` in UN65.cs inherits `TU7000` in Screen.cs
- `class UN70` in UN70.cs inherits `TU7000` in Screen.cs
- `class UN75` in UN75.cs inherits `TU7000` in Screen.cs
<br><br>
## Interface
- TM124A : interface that TU7000 screens must implement, as they are the common fundamental remote commands that all series model share.
  - `void Power(string command)` : should implement the Power function of the TU7000 series.
  - `void Mute(string command)` : should implement the volume Mute function of the TU7000 series.
  - `void Source(string command)` : should implement the screen signal Source function of the TU7000 series.
  - `void Volume(string command)` : should implement the change Volume function of the TU7000 series.
  - `void Channel(string command)` : should implement the change Channel function of the TU7000 series.
  - `void Last(string command)` : should implement the Last channel function of the TU7000 series.
  - `void Info(string command)` : should implement the screen state Info of the TU7000 series.

<br><br>
## Delegate
- `void SignalHandler(string command)` : Multicasted delegate that acts as the remote signal to the TU7000 screen.
<br><br>
