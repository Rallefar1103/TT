using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Markup.LeftToRight;
using static Xamarin.Forms.Markup.GridRowsColumns;
using Xamarin.Forms.Shapes;

namespace TurfTankRegistrationApplication.Pages
{
    public class RobotRegistrationPage : ContentPage
    {
        /// <summary>
        /// Denne enum bruges til at navngive de specifikke rows
        /// Det bør gøre det nemmere at flytte rundt på layoutet
        /// </summary>
        enum Row
        {
            picture,
            Chassi,
            Controller,
            Rover,
            Base,
            Tablet,
            Spacer1,
            Save
        }


        private Button ChassiButton;
        private Button ControllerButton;
        private Button RoverButton;
        private Button BaseButton;
        private Button TabletButton;
        private Button Save;
        private Grid MainGrid;

        public RobotRegistrationPage()
        {
            Visual = VisualMarker.Material;
            Content = GetContent();
        }

        public Xamarin.Forms.View GetContent() => new Grid
        {
            RowDefinitions = Rows.Define(
                   (Row.picture, 50),
                   (Row.Chassi, Auto),
                   (Row.Controller, Auto),
                   (Row.Rover, Auto),
                   (Row.Base, Auto),
                   (Row.Tablet, Auto),
                   (Row.Spacer1, Auto),
                   (Row.Save, Auto)
                ),

            Children = {
                        new Image {Source = "RobotPic.png"}
                        .Row(Row.picture),

                        new Button{Text = "Chassi"}
                        .Row(Row.Chassi),
                        new Button{Text="Controller"}
                         .Row(Row.Controller),
                        new Button{Text="Rover"}
                         .Row(Row.Rover),
                        new Button{Text="Base"}
                         .Row(Row.Base),
                        new Button{Text="Tablet"}
                         .Row(Row.Tablet),
                        new BoxView{}
                         .Row(Row.Spacer1),

                        new Button{Text="Save Robot"}
                         .Row(Row.Save),
                }
        }
        .Margin(100)
        .Assign(out MainGrid);
    }
}



