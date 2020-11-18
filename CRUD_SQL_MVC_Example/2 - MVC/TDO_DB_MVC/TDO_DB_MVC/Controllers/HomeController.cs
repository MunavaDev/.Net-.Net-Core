using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
//with model binding
using TDO_DB_MVC.Models;

namespace TDO_DB_MVC.Controllers
{
    public class HomeController : Controller
    {
        //Connection Address
        public static string connnectionstring = "Data Source=localhost;Initial Catalog=Cars;User Id=sa; password=Calvin1605";

        //Convert to Sql Connection

        SqlConnection cnn = new SqlConnection(connnectionstring);


        //Example login form

        public ActionResult Login()
        {

            return View();
        }

        // Week 1 Open and close connection 
        public ActionResult Home()
        {
       
            //Attempt to connect to database
            try
            {
                cnn.Open();
                ViewBag.Message = "Connection was opened and then...";
                ViewBag.ResponseError = "Any Errors will be logged here";

            }
            /*Catch any errors*/
            catch (Exception ex)
            {
                /*load Response*/
                ViewBag.ResponseError = ex;
                ViewBag.Message = "Something Went Wrrong";
            }
            finally
            {
               cnn.Close();
            }

            //return view
            return View();
        }

        //Week 2 -> C Creating or adding a new onject to the db

        public ActionResult AddCars(string CarName, string CarModel, string CarPrice)
        {


            if (CarName == null || CarModel == null || CarPrice == null)
            {
                ViewBag.Message = "One or more input fields are empty...Please add data";
                return View("Home");

            }
            else
            {
                try
                {
                    //create command 
                    SqlCommand myInsertCommand = new SqlCommand("Insert into CarModels Values ('" + CarName + "','" + CarModel + "','" + CarPrice + "')",/*Define Adress*/ cnn);

                    cnn.Open();

                    //This has to go here myInsertCommand.ExecuteNonQuery(); 

                    //This is optional 
                    int RowsChanged = myInsertCommand.ExecuteNonQuery();

                    ViewBag.Message = "Success:" + RowsChanged + "rows added.";

                }
                catch (Exception ex)
                {
                    /*load Response*/
                    ViewBag.ResponseError = ex;
                }
                finally
                {
                    cnn.Close();
                }

                ReadCars();

                ViewBag.ResponseError = TempData["Message"] as string;

                return View("Home");
            }
        }

        //Week 3 -> R Reading from DB

        public ActionResult ReadCars()
        {
            //without Model Binding
            //create a list that  can hold the values you are reading
            //List<string> CarsRead = new List<string>();

            //with Model Binding
            //create a list of Model type
            List<CarsModel> cars = new List<CarsModel>();

            ViewBag.Message = TempData["Rows"] as string;

            //try etc

            try
            {
                //create our statment
                SqlCommand myReadCommand = new SqlCommand("SELECT * FROM CarModels", cnn);

                //open our connection
                cnn.Open();

                //read from db

               SqlDataReader dataThatIveRead =  myReadCommand.ExecuteReader();

                //-while we read load data into our list 
                while(dataThatIveRead.Read())
                {

                    //this is without Model Binding
                   // CarsRead.Add((string)dataThatIveRead["CarName"] + " " + (string)dataThatIveRead["CarModel"] + " " + (string)dataThatIveRead["CarPrice"]);

                    //with Model Binding 
                    //create anew instance of a car
                    CarsModel newcar = new CarsModel();
                    newcar.ID = (int)dataThatIveRead["CarId"];
                    newcar.Name = (string)dataThatIveRead["CarName"];
                    newcar.Model = (string)dataThatIveRead["CarModel"];
                    newcar.Price = (string)dataThatIveRead["CarPrice"];
                    cars.Add(newcar);


                }

            }
            catch(Exception ex)
            {
                TempData["Message"] = ex;
                return RedirectToAction("Home", "Home");
            }
            finally
            {
                //finally close our connection
                cnn.Close();
            }

            return View(cars);

        }

        //Week 4 -> U update DB

        public ActionResult UpdateCar(int Id)
        {
            //Instantiate object class to hold object 
            CarsModel newcar = new CarsModel();

            try
            {
                //create our statment
                SqlCommand myReadCommand = new SqlCommand("SELECT CarModels.CarId , CarModels.CarName , CarModels.CarModel , CarModels.CarPrice FROM CarModels WHERE CarId ="+ Id , cnn);

                //open our connection
                cnn.Open();

                //read from db

                SqlDataReader dataThatIveRead = myReadCommand.ExecuteReader();

                //-while we read load data into our list 
                while (dataThatIveRead.Read())
                {
                    newcar.ID = (int)dataThatIveRead["CarId"];
                    newcar.Name = (string)dataThatIveRead["CarName"];
                    newcar.Model = (string)dataThatIveRead["CarModel"];
                    newcar.Price = (string)dataThatIveRead["CarPrice"];

                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex;
            }
            finally
            {
                cnn.Close();
            }

            return View(newcar);
        }

        public ActionResult UpdateCarDb( string CarID ,string CarName, string CarModel, string CarPrice)
        {

            try
            {
                //create our statment
                SqlCommand myReadCommand = new SqlCommand("UPDATE CarModels SET CarModels.CarName = '" + CarName + "', CarModels.CarModel ='" + CarModel + "' , CarModels.CarPrice ='" + CarPrice + "'WHERE CarId = "+CarID,cnn);

                //open our connection
                cnn.Open();

                //read from db
                int RowsChanged = myReadCommand.ExecuteNonQuery();

                TempData["Rows"] = "Successfully updated" + " "+ RowsChanged + " " + "rows";

            }
            catch(Exception ex)
            {
                TempData["Message"] = ex;
                return RedirectToAction("ReadCars", "Home");
            }
            finally
            {
                cnn.Close();
            }
            return RedirectToAction("ReadCars","Home");
        }

        //Week 5 -> U update DB

        public ActionResult DeleteCar(int Id)
        {
            //Instantiate object class to hold object 
            CarsModel newcar = new CarsModel();

            try
            {
                //create our statment
                SqlCommand myReadCommand = new SqlCommand("SELECT CarModels.CarId , CarModels.CarName FROM CarModels WHERE CarId =" + Id, cnn);

                //open our connection
                cnn.Open();

                //read from db

                SqlDataReader dataThatIveRead = myReadCommand.ExecuteReader();

                //-while we read load data into our list 
                while (dataThatIveRead.Read())
                {
                    newcar.ID = (int)dataThatIveRead["CarId"];
                    newcar.Name = (string)dataThatIveRead["CarName"];
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex;
            }
            finally
            {
                cnn.Close();
            }

            return View(newcar);
        }

        public ActionResult DeleteCarDb(string CarID)
        {

            try
            {
                //create our statment
                SqlCommand myReadCommand = new SqlCommand("Delete FROM CarModels WHERE CarId="+CarID, cnn);

                //open our connection
                cnn.Open();

                //read from db
                int RowsChanged = myReadCommand.ExecuteNonQuery();

                TempData["Rows"] = "Succesfully Deleted" + " " + RowsChanged + " " + "rows";

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex;
                return RedirectToAction("ReadCars", "Home");
            }
            finally
            {
                cnn.Close();
            }
            return RedirectToAction("ReadCars", "Home");
        }

    }
}