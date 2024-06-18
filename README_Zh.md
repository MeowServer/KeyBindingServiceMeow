Meow服务器设计~
- [English](https://github.com/MeowServer/KeyBindingServiceMeow/blob/main/README.md)
- [中文](https://github.com/MeowServer/KeyBindingServiceMeow/blob/main/README_Zh.md)
# Key Binding Service Meow
这是一个Exiled插件，允许您将自己插件的行为和玩家的按键绑定
# 安装
## 客户端：
1. 在Steam的启动选项中添加 -allow-syncbind
2. 在游戏中，进入服务器之前使用 ".synccmd" 指令两次，直到 "SyncServerCommandBinding has been enabled" 出现。
3. 进入服务器后按两下RA键
## 服务器端:
1. 在config_gameplay.txt中打开enable_sync_command_binding option。
# 文档
这是一个使用此插件的简单指引：
1. 使用KeyRegister.RegisterKey方法来绑定一个键到您插件的Action上
2. 使用KeyRegister.RegisterKeyToEvent方法来注册一个或多个键，然后绑定Events.OnKeyPressed事件
