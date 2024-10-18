Projetado por MeowServer~
- [English](https://github.com/MeowServer/KeyBindingServiceMeow/blob/main/README.md)
- [中文](https://github.com/MeowServer/KeyBindingServiceMeow/blob/main/README_Zh.md)
- [Português](https://github.com/MeowServer/KeyBindingServiceMeow/blob/main/README_Br.md)
# Key Binding Service Meow
Esse plug-in do EXILED permite que outros plug-ins personalizem a vinculação de tecla do jogador.
# Instalação
## Client-side:
1. Adicione -allow-syncbind na opção de inicialização
2. Use o comando "synccmd" duas vezes no console do cliente até que "SyncServerCommandBinding has been enabled" apareça no console.
## Server-side:
1. Ative a opção enable_sync_command_binding no config_gameplay.txt
2. Ponha KeyBindingServiceMeow.dll na pasta Plugins
3. Ponha Newtonsoft.Json.dll na pasta Plugins.dependencies
# Documentário
Aqui vai uma simples instrução para desenvolver usando esse plug-in:  
Há duas formas principais para vincular uma tecla à sua ação:
| Método | Vantagem | Desvantagem |
| ---- | ---- | ---- |
| HotKey | O jogador pode saber qual tecla o plug-in está usando e personalizá-la usando esse comando | Você precisa criar uma instância de HotKey e registrá-la para todos jogadores|
| Event | Fácil de usar.  | Não é personalizável e nem visível para jogadores. |
## Exemplo
1. Hot Key
```CSharp
var hotKey = new HotKey(KeyCode.YourKeyCode, "ID", "Nome", "Descrição(opcional)", "Categoria(opcional)");
hotKey.KeyPressed += YourMethod;
API.Features.HotKey.HotKeyBinder.RegisterKey(ev.Player, hotKeys);
```
2. Event
```CSharp
API.Event.Events.RegisterKeyToEvent(KeyCode.YourKeyCode);
API.Event.Events.KeyPressed += OnKeyPressed;
```
Ao usar o evento OnKeyPressed, você primeiro deve registrar sua tecla usando o método RegisterKeyToEvent.
