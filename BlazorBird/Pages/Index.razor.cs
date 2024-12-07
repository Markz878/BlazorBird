using BlazorBird.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorBird.Pages
{
  public partial class Index(IJSRuntime js) : IAsyncDisposable
  {
    public GameModel Game { get; } = new GameModel();
    private IJSObjectReference? module;
    private static Func<ScreenSize, Task>? updateScreenInfoFunc;

    protected override void OnInitialized()
    {
      Game.PropertyChanged += Game_PropertyChanged;
    }

    [JSInvokable]
    public static void GetScreenSize(ScreenSize screenSize)
    {
      updateScreenInfoFunc?.Invoke(screenSize);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
      if (firstRender)
      {
        updateScreenInfoFunc = ScreenSizeChanged;
        module = await js.InvokeAsync<IJSObjectReference>("import", "./Pages/Index.razor.js");
      }
    }

    private async Task ScreenSizeChanged(ScreenSize screenSize)
    {
      await InvokeAsync(() =>
      {
        Game.ScreenSizeChanged(screenSize);
        StateHasChanged();
      });
    }

    public async Task Action(MouseEventArgs e)
    {
      if (e.Button == 0)
      {
        if (!Game.IsRunning)
        {
          Game.Restart();
          await Game.MainLoop();
        }
        else
        {
          Game.Bird.Jump();
        }
      }
    }

    private void Game_PropertyChanged()
    {
      StateHasChanged();
    }

    public async ValueTask DisposeAsync()
    {
      Game.PropertyChanged -= Game_PropertyChanged;
      if (module is not null)
      {
        await module.DisposeAsync();
      }
      GC.SuppressFinalize(this);
    }
  }
}
