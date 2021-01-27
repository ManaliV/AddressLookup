using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AddressLookup.Models;
using AddressLookup.Repository;

namespace AddressLookup.Controllers
{
    public class AddressController : Controller
    {
        
        public ActionResult AddAddress(AddressModel addressModel)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    AddressRepository addressRepo = new AddressRepository();
                    List<AddressModel> allAddresses= addressRepo.GetAllMatchingAddress(addressModel.Address, addressModel.ZipCode);
                    if(allAddresses.Count==0)
                        ViewBag.Message = "No Match Found.";                    
                    else 
                    {
                        if (allAddresses.Count > 0)
                            ViewBag.Message = "Multiple Match Found.";
                        else
                            ViewBag.Message = "Single Address Found.";
                        if (addressRepo.AddAddress(allAddresses))
                        {
                            ViewBag.Message += "\nAddress added successfully";
                            if (addressRepo.UpdateAddress())
                                ViewBag.Message += "\nStatus updated for single address.";
                            else
                                ViewBag.Message += "\nStatus is not updated for single address.";
                        }
                        else
                            ViewBag.Message += "Failure when adding address.";

                    }
                    
                }

                return View();
            }
            catch(Exception e)
            {
                return View();
            }




        }
        // GET: Employee/AddEmployee    
        public ActionResult AddEmployee()
        {
            return View();
        }

        // POST: Employee/AddEmployee    
        //[HttpPost]
        //public ActionResult AddAddress(AddressModel Emp)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            EmpRepository EmpRepo = new EmpRepository();

        //            if (EmpRepo.AddEmployee(Emp))
        //            {
        //                ViewBag.Message = "Employee details added successfully";
        //            }
        //        }

        //        return View();
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Employee/EditEmpDetails/5    
        //public ActionResult EditEmpDetails(int id)
        //{
        //    EmpRepository EmpRepo = new EmpRepository();



        //    return View(EmpRepo.GetAllEmployees().Find(Emp => Emp.Empid == id));

        //}

        //// POST: Employee/EditEmpDetails/5    
        //[HttpPost]

        //public ActionResult EditEmpDetails(int id, EmpModel obj)
        //{
        //    try
        //    {
        //        EmpRepository EmpRepo = new EmpRepository();

        //        EmpRepo.UpdateEmployee(obj);
        //        return RedirectToAction("GetAllEmpDetails");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        
    }
}
