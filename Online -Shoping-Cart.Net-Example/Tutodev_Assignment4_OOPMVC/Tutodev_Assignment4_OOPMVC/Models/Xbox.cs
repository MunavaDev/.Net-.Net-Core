using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutodev_Assignment4_OOPMVC.Models
{
    public class Xbox : ConsoleI
    {
        //Data Members - these are the data entities your class will act on 

        public string _xboxliveID;
        public string _batteryType;

        //Constructors
        public Xbox(int ID, string Name, string Description, string Model, double Price, int Quantity, string ImagePath, DateTime ReleaseDate, string ShippingTime, string xboxliveID, string batteryType) : base(ID, Name, Description, Model, Price, Quantity, ImagePath, ReleaseDate, ShippingTime)
        {
            _xboxliveID = xboxliveID;
            _batteryType = batteryType;
        }

        //Methods

        public override string DescribeItem()
        {
            return "The following Console uses the Live ID " + " " + this.xboxliveID + " " + "and is battery based using " + " " + this.batteryType;
        }

        public override string ShippingDelay()
        {
            return "The" + " " + this.Name + " " + "was released in " + this.RealeaseDate + "\n" +
                   "This Item will take " + this.ShippingTime + " " + "to deliver";

        }

        //Properties

        public string xboxliveID
        {
            get { return _xboxliveID; }

            set { _xboxliveID = value; }
        }

        public string batteryType
        {
            get { return _batteryType; }

            set { _batteryType = value; }
        }

    }
}