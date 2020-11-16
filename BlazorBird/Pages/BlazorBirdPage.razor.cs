using BlazorBird.Models;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BlazorBird.Pages
{
    public partial class BlazorBirdPage : IDisposable
    {
        public GameModel Game { get; } = new GameModel();

        protected override void OnInitialized()
        {
            Game.PropertyChanged += Game_PropertyChanged;
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

        private void Game_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            StateHasChanged();
        }

        public void Dispose()
        {
            Game.PropertyChanged -= Game_PropertyChanged;
        }
    }
}
