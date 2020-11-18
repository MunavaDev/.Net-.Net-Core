using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tutodev_Assignment4_OOPMVC.Models;

namespace Tutodev_Assignment4_OOPMVC.Models
{
    public abstract class ConsoleI
    {
        //Create List

        //Data Members
        public int _ID;
        public string _Name;
        public string _Description;
        public string _Model;
        public double _Price;
        public int _Quantity;
        public string _Imagepath;
        public DateTime _RealeaseDate;
        public string _ShippingTime;


        //Constructors -This is the default constructors that instatiate/initialise your class

        public ConsoleI(int ID,string Name, string Description , string Model ,double Price, int Quantity, string ImagePath, DateTime ReleaseDate, string ShippingTime)
        {
            _ID = ID;
            _Name = Name;
            _Description = Description;
            _Model = Model;
            _Price = Price;
            _Quantity = Quantity;
            _Imagepath = ImagePath;
            _RealeaseDate = ReleaseDate;
            _ShippingTime = ShippingTime;
        }

        //Methods - here you define Methods and functions for your class

        public abstract string DescribeItem();

        public abstract string ShippingDelay();

        //These are properties that your data members will inherit

        public int ID
        {
            get { return _ID; }

            set { _ID = value; }
        }

        public string Name
        {
            get { return _Name; }

            set { _Name = value; }
        }

        public string Description
        {
            get { return _Description; }

            set { _Description= value; }
        }

        public string Model
        {
            get { return _Model; }

            set { _Model = value; }
        }

        public double Price
        {
            get { return _Price; }

            set { _Price = value; }
        }

        public int Quantity
        {
            get { return _Quantity; }

            set { _Quantity = value; }
        }

        public string ImagePath
        {
            get { return _Imagepath; }

            set { _Imagepath = value; }
        }

        public DateTime RealeaseDate
        {
            get { return _RealeaseDate; }

            set { _RealeaseDate = value; }
        }

        public string ShippingTime
        {
            get { return _ShippingTime; }

            set { _ShippingTime = value; }
        }

        //End Properties


    }
}