using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skclusive.Blazor.Dashboard.App.View.UserList
{
    public partial class UsersToolbar
    {
        [Parameter] public EventCallback OnAddUser { get; set; }
    }
}
