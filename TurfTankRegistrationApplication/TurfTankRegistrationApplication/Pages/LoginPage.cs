using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Markup.LeftToRight;
using static Xamarin.Forms.Markup.GridRowsColumns;
using Xamarin.Forms.Shapes;
using TurfTankRegistrationApplication.View;

namespace TurfTankRegistrationApplication.Pages
{
    

    /*En forklaring af hvad der sker med Markup findes her:  //forklaret: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/csharp-markup */

    public class LoginPage : ContentPage
    {
        /// <summary>
        /// Denne enum bruges til at navngive de specifikke rows
        /// Det bør gøre det nemmere at flytte rundt på layoutet
        /// </summary>
        enum Row 
        {
            picture,
            username,
            password,
            Spacer1,
            login
        }

        private Entry UsernameEntry;
        private Entry PasswordEntry;
        private Grid MainGrid;

        public LoginPage()
        {
            /*https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/visual/material-visual */
            Visual = VisualMarker.Material;
            Content = GetContent();
        }

        public Xamarin.Forms.View GetContent() => new Grid
        {
            RowDefinitions = Rows.Define(
               (Row.picture, 300 ),
               (Row.username, Auto),
               (Row.password, Auto),
               (Row.Spacer1, Auto),
               (Row.login, Auto)
               ),

            Children = {
                new LoginView{ }
                .Row(Row.picture),

                new Button{Text ="test"}.Row(Row.Spacer1)

                //.Center()
                    //new Image{Source = "RobotPic.png"}
                    // .Row(Row.picture),

                    //new Entry { Placeholder = "Username" }
                    // .Assign(out UsernameEntry)
                    ////.Size(100)
                    // .Center()
                    // .Row(Row.username),
                    //new Entry { Placeholder = "Password" }
                    // .Assign(out PasswordEntry)
                    ////.Size(200)
                    // .Center()
                    // .Row(Row.password),
                    //new BoxView{}
                    //     .Row(Row.Spacer1),

                    //new Button{ Text = "Login"}
                    // .Row(Row.login)
                    // //.Assign(),
                     

                }
        }
        .Margin(100)
        .Assign(out MainGrid);
    }
}

