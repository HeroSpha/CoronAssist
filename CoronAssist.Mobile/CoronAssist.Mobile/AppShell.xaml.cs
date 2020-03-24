﻿using CoronAssist.Mobile.Views;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CoronAssist.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("registration", typeof(RegisterPage));
            Routing.RegisterRoute("addbooking", typeof(AddBookingPage));

        }
    }
}
