using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TurfTankRegistrationApplication.Connection;

namespace TestUnit.Connection
{
    /// <summary>
    /// To make proper testing with NSwagger, it is needed that all of its objects can be initialised.
    /// This class presents a Static function for each swagger class.
    /// This also helps to throw early errors, in case that the Swagger schema changes in the future.
    /// </summary>
    //    public class Initializer
    //    {
    //        public static RobotItemModel ValidRobotItem()
    //        {
    //            RobotItemModel robot = new RobotItemModel();

    //            robot.Id = "";
    //            robot.Type = "";
    //            robot.Serialnumber = "";
    //            robot.MacEth = "";            
    //            robot.Controller = ValidRobotController();
    //            robot.CreatedOn = ValidDateTimeOffset();
    //            robot.Routeplans = ValidRoutePlanItems();
    //            robot.Subscription = ValidRobotSubscription();
    //            robot.Users = ValidRobotUsers();
    //            robot.Validation = ValidRobotValidation();
    //            return robot;
    //        }

    //        public static RobotController ValidRobotController()
    //        {
    //            RobotController controller = new RobotController();
    //            controller.SerialNumber = "";
    //            controller.Ssid = "";
    //            controller.SsidPassword = "";
    //            controller.MacEth = "";
    //            controller.MacWifi = "";
    //            return controller;
    //        }

    //        public static DateTimeOffset ValidDateTimeOffset()
    //        {
    //            DateTimeOffset date = new DateTimeOffset();

    //            return date;
    //        }

    //        public static List<RouteplanItem> ValidRoutePlanItems()
    //        {
    //            List<RouteplanItem> routes = new List<RouteplanItem>();


    //            return routes;
    //        }

    //        public static RobotSubscription ValidRobotSubscription()
    //        {
    //            RobotSubscription subscription = new RobotSubscription();

    //            return subscription;
    //        }

    //        public static List<RobotUser> ValidRobotUsers()
    //        {
    //            List<RobotUser> users = new List<RobotUser>();

    //            return users;
    //        }

    //        public static RobotValidation ValidRobotValidation()
    //        {
    //            RobotValidation validation = new RobotValidation();

    //            return validation;
    //        }
    //    }
}
