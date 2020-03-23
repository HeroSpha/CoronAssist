using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Skclusive.Blazor.Dashboard.App.View.Common;
using Skclusive.Material.Script;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Skclusive.Blazor.Dashboard.App.View.Authentication
{

    public partial class SignInPage
    {
        //[Inject]
        //public HttpClient http { get; set; }
        [Inject]
        public IApplicationUserRepository ApplicationUserRepository { get; set; }
        [CascadingParameter]
        public UserStateProvider State { get; set; }

        [Inject]
        public ScriptHelpers ScriptHelpers { get; set; }
        private string Email { set; get; }

        private string Password { set; get; }

        private void HandleEmailChange(ChangeEventArgs arg)
        {
            Email = arg.Value.ToString();

            StateHasChanged();
        }

        private void HandlePasswordChange(ChangeEventArgs arg)
        {
            Password = arg.Value.ToString();
            StateHasChanged();
        }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        private async void HandleSignIn()
        {
            //var login = await http
            //    .PostJsonAsync<LoginView>("/api/accounts/authenticate", new Coronassist.Web.Shared.Models.UserDto
            //    {
            //        Email = Email,
            //        Password = Password
            //    });
            var login = await ApplicationUserRepository.AuthenticateAsync(new Coronassist.Web.Shared.Models.UserDto
            {
                Email = Email,
                Password = Password
            });

            if (login.IsSuccess && login.Role == "Doctor")
            {
                State.User = login.ApplicationUser;
                StateHasChanged();
                NavigationManager.NavigateTo("dashboard");
            }
            // System.Console.WriteLine("HandleSignInDone");
        }
    }
}
