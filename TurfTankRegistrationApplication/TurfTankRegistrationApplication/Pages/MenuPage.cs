using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Markup.LeftToRight;
using static Xamarin.Forms.Markup.GridRowsColumns;
using Xamarin.Forms.Shapes;

namespace TurfTankRegistrationApplication.Pages
{
    public class MenuPage : ContentPage
    {

        /// <summary>
        /// Denne enum bruges til at navngive de specifikke rows
        /// Det bør gøre det nemmere at flytte rundt på layoutet
        /// </summary>
        enum Row
        {
            picture,
            Prereg,
            RobotReg
        }

        private Button PreRegistrationButton;
        private Button RobotRegistrationButton;
        private Grid MainGrid;

        public MenuPage()
        {
            
            Visual = VisualMarker.Material;
            Content = GetContent();
        }

        public Xamarin.Forms.View GetContent() => new Grid
        {
            RowDefinitions = Rows.Define(
                   (Row.picture, 50),
                   (Row.Prereg, Auto),
                   (Row.RobotReg, Auto)
               
               ),

            Children = {
                        new Image {Source = "RobotPic.png"}
                        .Row(Row.picture),
   
                        new Button{Text = "Preregistration"}
                        .Row(Row.Prereg),

                        new Button{Text="Robot Registration"}
                         .Row(Row.RobotReg),
                }
        }
        .Margin(100)
        .Assign(out MainGrid);
    }
}

