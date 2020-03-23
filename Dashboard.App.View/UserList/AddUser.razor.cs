using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Coronassist.Web.Shared.DAL.Core.Interfaces;
using Coronassist.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Skclusive.Blazor.Dashboard.App.View.AlertService;

namespace Skclusive.Blazor.Dashboard.App.View.UserList
{
    public partial class AddUser
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public ISweetAlertMessage SweetAlertMessage { get; set; }
        [Inject]
        public IApplicationUserRepository ApplicationUserRepository { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullAddress { get; set; }
        public string Province { get; set; }
        protected async override Task OnInitializedAsync()
        {
            SweetAlertMessage.Prepare(JSRuntime);
        }

        private void HandleFirstNameChange(ChangeEventArgs arg)
        {
            FirstName = arg.Value.ToString();

            StateHasChanged();
        }
        
        private void HandleLastNameChange(ChangeEventArgs arg)
        {
            LastName = arg.Value.ToString();

            StateHasChanged();
        }
        private void HandlePhoneNumberChange(ChangeEventArgs arg)
        {
            PhoneNumber = arg.Value.ToString();

            StateHasChanged();
        }
        private void HandleEmailChange(ChangeEventArgs arg)
        {
            Email = arg.Value.ToString();

            StateHasChanged();
        }
        private void HandleFullAddressChange(ChangeEventArgs arg)
        {
            FullAddress = arg.Value.ToString();

            StateHasChanged();
        }
        private void HandleProvinceChange(ChangeEventArgs arg)
        {
            Province = arg.Value.ToString();

            StateHasChanged();
        }
        private async void AddUserAsync()
        {
           if(!string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(Province) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(PhoneNumber))
            {
                var user = await ApplicationUserRepository.CreateAsync(
               new Usermodel
               {
                   Fullname = $"{FirstName} {LastName}",
                   Email = Email,
                   PhoneNumber = PhoneNumber,
                   ConfirmPassword = "User@123",
                   Password = "User@123",
                   Province = Province,
                   FullAddress = FullAddress,
                   Role = "Doctor"
               });
                if (user.identityResult.Succeeded)
                {
                    FirstName = string.Empty;
                    Email = string.Empty;
                    PhoneNumber = string.Empty;
                    FullAddress = string.Empty;
                    LastName = string.Empty;
                    Province = string.Empty;
                    StateHasChanged();
                    await SweetAlertMessage.SuccessMessage();
                }
            }
        }

    }
}