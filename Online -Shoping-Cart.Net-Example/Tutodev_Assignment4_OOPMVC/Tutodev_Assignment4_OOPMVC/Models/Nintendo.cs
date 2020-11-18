using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutodev_Assignment4_OOPMVC.Models
{
    public class Nintendo : ConsoleI
    {
        //Data Members - these are the data entities your class will act on 

        public string _switchID;
        public string _padSize;

        //Constructors
        public Nintendo(int ID, string Name, string Description, string Model, double Price, int Quantity, string ImagePath, DateTime ReleaseDate, string ShippingTime ,string switchID, string padSize) : base(ID,Name, Description, Model, Price, Quantity, ImagePath, ReleaseDate, ShippingTime)
        {
            _switchID = switchID;
            _padSize = padSize;
        }

        //Methods

        public override string DescribeItem()
        {
            return "The following Console uses Switch ID " + " " + this.switchID + " " + "and has a padsize of " + " " + this.padSize + "mm";
        }

        public override string ShippingDelay()
        {
            return "The" + " " + this.Name + " " + "was released in " + this.RealeaseDate + "\n" +
                   "This Item will take " + this.ShippingTime + " " + "to deliver";

        }

        //Properties

        public string switchID
        {
            get { return _switchID; }

            set { _switchID = value; }
        }

        public string padSize
        {
            get { return _padSize; }

            set { _padSize = value; }
        }

    }
}