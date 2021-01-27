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
                        ViewBag.Message = "\nNo Match Found.";                    
                    else 
                    {
                        if (allAddresses.Count > 0)
                            ViewBag.Message = "\nMultiple Match Found.";
                        else
                            ViewBag.Message = "\nSingle Address Found.";
                        if (addressRepo.AddAddress(allAddresses))
                        {
                            ViewBag.Message += "\nAddress added successfully.";
                            if (addressRepo.UpdateAddress())
                                ViewBag.Message += "\nSingle address status updated.";
                            else
                                ViewBag.Message += "\nStatus is not updated for any address.";
                        }
                        else
                            ViewBag.Message += "\nFailure when adding address.";

                    }
                    
                }

                return View();
            }
            catch
            {
                return View();
            }

        }

        
    }
}
