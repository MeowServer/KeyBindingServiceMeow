- [English](https://github.com/MeowServer/KeyBindingServiceMeow/blob/main/README.md)
- [中文](https://github.com/MeowServer/KeyBindingServiceMeow/blob/main/README_Zh.md)
- [Português](https://github.com/MeowServer/KeyBindingServiceMeow/blob/main/README_Br.md)
# Key Binding Service Meow
这是一个Exiled插件，允许您将自己插件的行为和玩家的按键绑定
# 安装
## 客户端：
1. 在Steam的启动选项中添加 -allow-syncbind
2. 在游戏中，进入服务器之前使用 "synccmd" 指令两次，直到 "SyncServerCommandBinding has been enabled" 出现。
## 服务器端:
1. 在config_gameplay.txt中打开enable_sync_command_binding option。
2. 将KeyBindingServiceMeow.dll放入Plugins文件夹
3. 将Newtonsoft.Json.dll放入Plugins.dependencies文件夹
# 文档
这是一个使用此插件的简单指引：
2种常用的方法
| 方法 | 优势 | 劣势 |
| ---- | ---- | ---- |
| HotKey | 玩家可以知道有哪些按键被绑定，并更改按键 | 你必须创建HotKey实例并将其添加给每一个玩家 |
| Event | 使用方式简单 | 玩家无法知晓或自定义这些按键 |
## 例子
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
使用KeyPressed事件时，您必须先使用RegisterKeyToEvent方法登记需要使用的按键
