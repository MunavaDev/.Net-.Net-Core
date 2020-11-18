using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutodev_Assignment4_OOPMVC.Models
{
    public class Playstation : ConsoleI
    {
        //Data Members - these are the data entities your class will act on 

        public string _playstationNetworkID;
        public string _dualShockRating;


        //Constructors
        public Playstation(int ID, string Name, string Description, string Model, double Price, int Quantity, string ImagePath, DateTime ReleaseDate, string ShippingTime, string playstationNetworkID, string dualShockRating) : base(ID, Name, Description, Model, Price, Quantity, ImagePath, ReleaseDate, ShippingTime)
        {
            _playstationNetworkID = playstationNetworkID;
            _dualShockRating = dualShockRating;
        }

        //Methods

        public override string DescribeItem()
        {
            return "The following Console uses NetworkID " + " " + this.playstationNetworkID + " " + "and has a dualshock rating of " + " " + this.dualShockRating + "pts";
        }

        public override string ShippingDelay()
        {
            return "The" + " " + this.Name + " " + "was released in " + this.RealeaseDate + "\n" +
                   "This Item will take " + this.ShippingTime + " " + "to deliver";

        }

        //Properties

        public string playstationNetworkID
        {
            get { return _playstationNetworkID; }

            set { _playstationNetworkID = value; }
        }

        public string dualShockRating
        {
            get { return _dualShockRating; }

            set { _dualShockRating = value; }
        }
    }
}