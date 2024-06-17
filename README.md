Designed by MeowServer~
- [English](https://github.com/MeowServer/KeyBindingServiceMeow/blob/main/README.md)
- [中文](https://github.com/MeowServer/KeyBindingServiceMeow/blob/main/README_Zh.md)
# Key Binding Service Meow
This Exiled plugin allows other plugins to customize the player's key binding.
# Installation
## Client-side:
1. Add -allow-syncbind in the launch option
2. Use the command ".synccmd" twice in the client console until "SyncServerCommandBinding has been enabled" appears on the console.
3. Press the RA key (default m) twice every time you connect to a server
## Server-side:
1. Enable enable_sync_command_binding option in config_gameplay.txt

# Documentary
Here's a simple instruction to develop using this plugin:
1. Use KeyRegister.RegisterKey method to bind your key to your action. 
2. Use KeyRegister.RegisterKeyToEvent to register a (or multiple) key to events. And register your method to the Event.KeyPressed.

