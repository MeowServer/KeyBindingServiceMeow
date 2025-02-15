## This repo has been permanently archived since an official key-binding system was released. The content will no longer be updated. Thank you for your understanding!

- [English](https://github.com/MeowServer/KeyBindingServiceMeow/blob/main/README.md)
- [中文](https://github.com/MeowServer/KeyBindingServiceMeow/blob/main/README_Zh.md)
- [Português](https://github.com/MeowServer/KeyBindingServiceMeow/blob/main/README_Br.md)

[Translation Contributers](TranslationContribution.md)
# Key Binding Service Meow
This Exiled plugin allows other plugins to customize the player's key binding.
# Installation
## Client-side:
1. Add -allow-syncbind in the launch option
2. Use the command "synccmd" twice in the client console until "SyncServerCommandBinding has been enabled" appears on the console.
## Server-side:
1. Enable enable_sync_command_binding option in config_gameplay.txt
2. Put KeyBindingServiceMeow.dll under Plugins folder
3. Put Newtonsoft.Json.dll under Plugins.dependencies folder
# Documentary
Here's a simple instruction to develop using this plugin:  
There 2 major way to bind a key to your action:
| Method | Advantage | Disadvantage |
| ---- | ---- | ---- |
| HotKey | Player can know which key the plugin is using and customize it using command | You have to create a HotKey instance and register it for every single player|
| Event | Easy to use.  | It is not customizable and not visible for players |
## Example
1. Hot Key
```CSharp
var hotKey = new HotKey(KeyCode.YourKeyCode, "ID", "Name", "Descriptoin(optional)", "Category(optional)");
hotKey.KeyPressed += YourMethod;
API.Features.HotKey.HotKeyBinder.RegisterKey(ev.Player, hotKeys);
```
2. Event
```CSharp
API.Event.Events.RegisterKeyToEvent(KeyCode.YourKeyCode);
API.Event.Events.KeyPressed += OnKeyPressed;
```
When using OnKeyPressed event, you must first register your key using RegisterKeyToEvent method.
