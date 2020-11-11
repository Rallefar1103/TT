
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Markup.LeftToRight;
using static Xamarin.Forms.Markup.GridRowsColumns;
using Xamarin.Forms.Shapes;

namespace TurfTankRegistrationApplication.Pages
{
    public class PreregistrationPage : ContentPage
    {
        /// <summary>
        /// Denne enum bruges til at navngive de specifikke rows
        /// Det bør gøre det nemmere at flytte rundt på layoutet
        /// </summary>
        enum Row
        {
            picture,
            QRSticker,
            Controller,
            Spacer1,
            Simcard,
            Rover,
            Base,
            Tablet,
            Spacer2,
            Save
        }


        private Button QRSticker;
        private Button Controller;
        private Button Simcard;
        private Button Rover;
        private Button Base;
        private Button Tablet;
        private Button Save;
        private Grid MainGrid;

        public PreregistrationPage()
        {
            
            Visual = VisualMarker.Material;
            Content = GetContent();
        }

        public Xamarin.Forms.View GetContent() => new Grid
        {
            RowDefinitions = Rows.Define(
                   (Row.picture, 50),
                   (Row.QRSticker, Auto),
                   (Row.Controller, Auto),
                   (Row.Spacer1, Auto),

                   (Row.Simcard, Auto),
                   (Row.Rover, Auto),
                   (Row.Base, Auto),
                   (Row.Tablet, Auto),
                   (Row.Spacer2, Auto),

                   (Row.Save, Auto)
                ),

            Children = {
                        new Image {Source = "RobotPic.png"}
                        .Row(Row.picture),
                        new Button{Text = "QR-Sticker"}
                        .Row(Row.QRSticker),
                        new Button{Text="Controller"}
                         .Row(Row.Controller),
                        new BoxView{}
                         .Row(Row.Spacer1),

                        new Button{Text="Simcard"}
                         .Row(Row.Simcard),
                        new Button{Text="Rover"}
                         .Row(Row.Rover),
                        new Button{Text="Base"}
                         .Row(Row.Base),
                        new Button{Text="Tablet"}
                         .Row(Row.Tablet),
                        new BoxView{}
                         .Row(Row.Spacer2),

                        new Button{Text="Save"}
                         .Row(Row.Save),
                }
        }
        .Margin(100)
        .Assign(out MainGrid);
    }
}



