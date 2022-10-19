// <copyright file="MaxCatalogController.cs" company="Lakstins Family, LLC">
// Copyright (c) Brian A. Lakstins (http://www.lakstins.com/brian/)
// </copyright>

#region License
// <license>
// This software is provided 'as-is', without any express or implied warranty. In no 
// event will the author be held liable for any damages arising from the use of this 
// software.
//  
// Permission is granted to anyone to use this software for any purpose, including 
// commercial applications, and to alter it and redistribute it freely, subject to the 
// following restrictions:
// 
// 1. The origin of this software must not be misrepresented; you must not claim that 
// you wrote the original software. If you use this software in a product, an 
// acknowledgment (see the following) in the product documentation is required.
// 
// Portions Copyright (c) Brian A. Lakstins (http://www.lakstins.com/brian/)
// 
// 2. Altered source versions must be plainly marked as such, and must not be 
// misrepresented as being the original software.
// 
// 3. This notice may not be removed or altered from any source distribution.
// </license>
#endregion

#region Change Log
// <changelog>
// <change date="6/3/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="6/23/2014" author="Brian A. Lakstins" description="Added Client functionality..">
// <change date="6/25/2014" author="Brian A. Lakstins" description="Added Catalog, Category, Vendor, Manufacturer, and Product functionality..">
// <change date="6/30/2014" author="Brian A. Lakstins" description="Add Manage to the management controllers.">
// <change date="7/9/2014" author="Brian A. Lakstins" description="Add Cart Add, Cart view, and Cart update methods.">
// <change date="9/16/2014" author="Brian A. Lakstins" description="Added partial method that caches output.">
// <change date="9/30/2014" author="Brian A. Lakstins" description="Added product search.">
// <change date="6/8/2015" author="Brian A. Lakstins" description="Updated to improve performance.">
// <change date="6/11/2015" author="Brian A. Lakstins" description="Show error if no successful authorizations or sales.">
// <change date="7/29/2015" author="Brian A. Lakstins" description="Fix error handling or order page.">
// <change date="10/17/2016" author="Brian A. Lakstins" description="Fix price calculation for WooCommerce products.">
// <change date="10/24/2017" author="Brian A. Lakstins" description="Update to pass cart id to order page.">
// <change date="10/26/2017" author="Brian A. Lakstins" description="Fix pass cart id to order page.">
// <change date="7/11/2018" author="Brian A. Lakstins" description="Fix order being updated after it was placed.">
// <change date="9/13/2018" author="Brian A. Lakstins" description="Add Catalog">
// <change date="9/14/2018" author="Brian A. Lakstins" description="Add redirect to home page when data cannot be loaded. Add HEAD method for Cart">
// <change date="9/18/2018" author="Brian A. Lakstins" description="Add back setting catalog.  Oops.">
// <change date="9/25/2018" author="Brian A. Lakstins" description="Only set catalog if it is active.">
// <change date="11/30/2018" author="Brian A. Lakstins" description="Updated for new special shipping type that is free.">
// <change date="11/30/2018" author="Brian A. Lakstins" description="Updated for allowing any payment type that is selected.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.Mvc4.PresentationLayer
{

    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using System.Web.Routing;
    using MaxFactry.Core;
    using MaxFactry.Base.PresentationLayer;
    using MaxFactry.Module.Catalog.BusinessLayer;
    using MaxFactry.Module.Catalog.PresentationLayer;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.General.AspNet.BusinessLayer;
    using MaxFactry.General.AspNet.PresentationLayer;
    using MaxFactry.General.AspNet.IIS.Mvc4.PresentationLayer;
    using MaxFactry.General.BusinessLayer;
    using MaxFactry.General.PresentationLayer;

    public class MaxCatalogController : MaxBaseController
    {
        /* 
         [ChildActionOnly]
         public virtual ActionResult CatalogCategoryNavigation()
         {
             MaxCategoryViewModel loModel = new MaxCategoryViewModel();
             return PartialView("_PartialCatalogCategoryNavigation", loModel);
         }

         [ChildActionOnly]
         [OutputCache(Duration = 600, VaryByParam = "msk;nocache", VaryByCustom = "msk;nocache")]
         public virtual ActionResult CatalogCategoryList()
         {
             MaxCategoryViewModel loModel = new MaxCategoryViewModel();
             return PartialView("_PartialCatalogCategoryList", loModel);
         }
         */

        public ActionResult Index(string id)
        {
            ViewBag.Message = "Hello World Template MVC";
            return View();
        }

        [AcceptVerbs(HttpVerbs.Head | HttpVerbs.Get)]
        public virtual ActionResult Catalog(string id)
        {
            MaxCatalogViewModel loModel = new MaxCatalogViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                if (!loModel.Load(id))
                {
                    return Redirect("/");
                }
                else if (loModel.IsActive)
                {
                    loModel.SetCurrent();
                }
            }

            return View(loModel);
        }

        [AcceptVerbs(HttpVerbs.Head | HttpVerbs.Get)]
        public virtual ActionResult Category(string id)
        {
            MaxCategoryViewModel loModel = new MaxCategoryViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                if (!loModel.Load(id))
                {
                    return Redirect("/");
                }
            }

            return View(loModel);
        }

        [AcceptVerbs(HttpVerbs.Head | HttpVerbs.Get)]
        public virtual ActionResult Product(string id)
        {
            MaxProductViewModel loModel = new MaxProductViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                if (!loModel.Load(id))
                {
                    return Redirect("/");
                }
            }

            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Product(MaxProductViewModel loModel, string uoProcess)
        {
            if (!string.IsNullOrEmpty(uoProcess))
            {
                if (uoProcess.Equals("cartadd", StringComparison.InvariantCultureIgnoreCase))
                {
                    //// Load the product being added
                    loModel.EntityLoad();
                    loModel.Load();
                    //// Load the current cart (which may be a new cart)cart
                    MaxCartViewModel loCartViewModel = new MaxCartViewModel(string.Empty);
                    if (loCartViewModel.CartAdd(loModel))
                    {
                        if (User.Identity.IsAuthenticated)
                        {
                            loCartViewModel.Username = User.Identity.Name;
                        }

                        loCartViewModel.Save();
                        loCartViewModel.SetCurrent();
                        return RedirectToAction("Cart");
                    }
                    else
                    {
                        ModelState.AddModelError("General", "There was an error adding the product to the cart");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("General", "No action was specified for the product.");
            }

            loModel.EntityLoad();
            loModel.Load();
            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult ProductFile(string id)
        {
            object loEmailAddress = MaxConfigurationLibrary.GetValue(MaxEnumGroup.ScopeProfile, "ProductFileEmailAddress");
            if (null != loEmailAddress)
            {
                ViewBag.EmailAddress = loEmailAddress.ToString();
            }

            MaxProductFileViewModel loModel = new MaxProductFileViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                if (!loModel.Load(id))
                {
                    return Redirect("/");
                }
            }

            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ProductFile(MaxProductFileViewModel loModel, string uoProcess)
        {
            //// Load the product file being sent being added
            loModel.EntityLoad();
            loModel.Load();
            if (!string.IsNullOrEmpty(uoProcess))
            {
                if (uoProcess.Equals("sendfile", StringComparison.InvariantCultureIgnoreCase))
                {
                    string lsEmail = Request.Form["uoEmail"];
                    ViewBag.EmailAddress = lsEmail;
                    MaxEmailEntity loEmail = MaxEmailEntity.Create();
                    if (!MaxEmailEntity.IsValidEmail(lsEmail))
                    {
                        ModelState.AddModelError("General", "The email address is not valid.");
                    }
                    else
                    {
                        string lsFromName = "Product Catalog";
                        if (!string.IsNullOrEmpty(Request.Form["uoFromName"]))
                        {
                            lsFromName = Request.Form["uoFromName"];
                        }

                        string lsFromAddress = "catalog@efactorysolutions.com";
                        if (!string.IsNullOrEmpty(Request.Form["uoFromAddress"]))
                        {
                            lsFromAddress = Request.Form["uoFromAddress"];
                        }

                        string lsSubject = "File " + loModel.Name;
                        if (!string.IsNullOrEmpty(Request.Form["uoSubject"]))
                        {
                            lsSubject = Request.Form["uoSubject"];
                        }


                        MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeProfile, "ProductFileEmailAddress", lsEmail);
                        string lsViewPath = "/Views/MaxCatalog/ProductFileEmail.cshtml";
                        string lsContent = MaxDesignLibrary.GetHtml(lsViewPath, loModel, new MaxIndex());
                        string lsContentInline = MaxDesignLibrary.ConvertToInlineCSS(lsContent, string.Empty);
                        if (!loModel.SendAsEmailAttachment(lsEmail, lsFromName, lsFromAddress, lsSubject, lsContentInline))
                        {
                            ModelState.AddModelError("General", "There was an error sending the email.");
                        }
                    }

                    if (ModelState.IsValid)
                    {
                        return RedirectToAction("ProductFileSent", new { id = loModel.Id, e = Request.Form["uoEmail"] });
                    }
                }
            }
            else
            {
                ModelState.AddModelError("General", "No action was specified.");
            }

            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult ProductFileSent(string id)
        {
            MaxProductFileViewModel loModel = new MaxProductFileViewModel(id);
            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult ProductFileEmail(string id)
        {
            MaxProductFileViewModel loModel = new MaxProductFileViewModel();
            if (!string.IsNullOrEmpty(id))
            {
                if (!loModel.Load(id))
                {
                    return Redirect("/");
                }
            }

            return View(loModel);
        }

        [HttpHead]
        [MaxEnableEmbed]
        public virtual ActionResult Cart()
        {
            MaxCartViewModel loModel = new MaxCartViewModel();
            return View(loModel);
        }

        [HttpGet]
        [MaxEnableEmbed]
        public virtual ActionResult Cart(string id)
        {
            MaxCartViewModel loModel = new MaxCartViewModel(id);
            if (null != id)
            {
                if (id.Equals("new", StringComparison.InvariantCultureIgnoreCase))
                {
                    MaxOrderViewModel loNew = new MaxOrderViewModel("new");
                    return RedirectToAction("Cart", routeValues: new { id = "" });
                }
                else if (Request.QueryString["TakeOver"] == id)
                {
                    loModel.SetCurrent();
                    MaxOrderViewModel loNew = new MaxOrderViewModel("new");
                    return RedirectToAction("Cart", routeValues: new { id = "" });
                }
            }

            if (!string.IsNullOrEmpty(Request.QueryString["uoRemoveDiscountCode"]))
            {
                loModel.CouponCode = "!" + Request.QueryString["uoRemoveDiscountCode"];
                loModel.Save();
                return RedirectToAction("Cart", routeValues: new { id = "" });
            }

            if (!string.IsNullOrEmpty(Request.QueryString["uoAddDiscountCode"]))
            {
                loModel.CouponCode = "+" + Request.QueryString["uoAddDiscountCode"];
                loModel.Save();
                return RedirectToAction("Cart", routeValues: new { id = "" });
            }

            return View(loModel);
        }

        [HttpPost]
        public virtual ActionResult Cart(MaxCartViewModel loModel, string uoProcess)
        {
            if (!string.IsNullOrEmpty(uoProcess))
            {

                if (uoProcess.Equals("update", StringComparison.InvariantCultureIgnoreCase) ||
                uoProcess.Equals("recalculate", StringComparison.InvariantCultureIgnoreCase) ||
                    uoProcess.Equals("PlaceOrder", StringComparison.InvariantCultureIgnoreCase) ||
                    uoProcess.Equals("AddCoupon", StringComparison.InvariantCultureIgnoreCase))
                {
                    string[] laCouponCodeList = this.Request.Form.GetValues("uoCoupon");
                    if (null != laCouponCodeList && laCouponCodeList.Length > 0)
                    {
                        loModel.CouponCodeList = new System.Collections.Generic.List<string>();
                        foreach (string lsValue in laCouponCodeList)
                        {
                            loModel.CouponCodeList.Add(lsValue);
                        }
                    }

                    bool lbUpdated = false;
                    foreach (MaxCartItemViewModel loItem in loModel.ItemList)
                    {
                        if (null == loItem.ManualDiscountReason)
                        {
                            loItem.ManualDiscountReason = string.Empty;
                        }

                        if (loItem.OriginalValues["Quantity"].ToString() != loItem.Quantity.ToString() ||
                            loItem.OriginalValues["ManualDiscountAmount"].ToString() != loItem.ManualDiscountAmount.ToString() ||
                            loItem.OriginalValues["ManualDiscountReason"].ToString() != loItem.ManualDiscountReason)
                        {
                            lbUpdated = true;
                            loItem.Save();
                        }
                    }

                    if (uoProcess.Equals("AddCoupon", StringComparison.InvariantCultureIgnoreCase))
                    {
                        lbUpdated = true;
                    }


                    if (null != loModel.ManualDiscount)
                    {
                        if (loModel.ManualDiscount.EndsWith("%"))
                        {
                            double lnDiscountPercent = MaxConvertLibrary.ConvertToDouble(typeof(object), loModel.ManualDiscount.Substring(0, loModel.ManualDiscount.Length - 1));
                            double lnDiscountAmount = (MaxConvertLibrary.ConvertToDouble(typeof(object), loModel.SubTotal) * lnDiscountPercent) / 100;

                            loModel.ManualDiscount = string.Format("{0:0.00}", lnDiscountAmount);
                        }

                        if (null == loModel.OriginalValues["ManualDiscount"])
                        {
                            lbUpdated = true;
                        }
                        else if (loModel.OriginalValues["ManualDiscount"].ToString() != loModel.ManualDiscount)
                        {
                            lbUpdated = true;
                        }
                    }

                    if (null != loModel.ManualDiscountExplanation)
                    {
                        if (null == loModel.OriginalValues["ManualDiscountExplanation"])
                        {
                            lbUpdated = true;
                        }
                        else if (loModel.OriginalValues["ManualDiscountExplanation"].ToString() != loModel.ManualDiscountExplanation)
                        {
                            lbUpdated = true;
                        }
                    }

                    if (MaxConvertLibrary.ConvertToInt(typeof(object), loModel.OriginalValues["ShippingType"]) != loModel.ShippingType)
                    {
                        lbUpdated = true;
                    }

                    string lsTaxLocation = Request.Form["uoTaxLocation"];
                    if (!string.IsNullOrEmpty(lsTaxLocation) && lsTaxLocation != loModel.TaxLocation)
                    {
                        loModel.TaxLocation = lsTaxLocation;
                        lbUpdated = true;
                    }

                    string lsTaxOverride = Request.Form["uoTaxOverride"];
                    if (!string.IsNullOrEmpty(lsTaxOverride))
                    {
                        int lnTaxOverride = MaxConvertLibrary.ConvertToInt(typeof(object), lsTaxOverride);
                        if (lnTaxOverride != loModel.TaxOverride)
                        {
                            loModel.TaxOverride = lnTaxOverride;
                            lbUpdated = true;
                        }
                    }

                    if (lbUpdated)
                    {
                        if (User.Identity.IsAuthenticated)
                        {
                            loModel.Username = User.Identity.Name;
                        }

                        loModel.Save();
                    }
                }

                if (uoProcess.Equals("PlaceOrder", StringComparison.InvariantCultureIgnoreCase))
                {
                    //// Start the order process sending the cart ID that is to be used to check out
                    string lsOrderUrl = this.Url.Action("Order") + "?id=" + loModel.Id;
                    return Redirect(lsOrderUrl);
                }

                if (uoProcess.StartsWith("delete", StringComparison.InvariantCultureIgnoreCase))
                {
                    bool lbDeleted = false;
                    string lsId = uoProcess.Substring("delete".Length);
                    foreach (MaxCartItemViewModel loItem in loModel.ItemList)
                    {
                        if (loItem.Id.Equals(lsId))
                        {
                            if (loItem.Delete())
                            {
                                lbDeleted = true;
                            }
                        }
                    }

                    if (lbDeleted)
                    {
                        loModel = new MaxCartViewModel(loModel.Id);
                    }
                }

                if (uoProcess.Equals("add", StringComparison.InvariantCultureIgnoreCase) && User.IsInRole("Admin - App"))
                {
                    ModelState.Clear();
                    if (string.IsNullOrWhiteSpace(loModel.CartItemNew.Name))
                    {
                        ModelState.AddModelError("CartItemNew.Name", "Name is required");
                    }

                    if (string.IsNullOrWhiteSpace(loModel.CartItemNew.Sku))
                    {
                        ModelState.AddModelError("CartItemNew.Sku", "Sku is required");
                    }

                    if (loModel.CartItemNew.Quantity <= 0)
                    {
                        ModelState.AddModelError("CartItemNew.Quantity", "Quantity is required");
                    }
                    else
                    {
                        int lnQuantity = MaxConvertLibrary.ConvertToInt(typeof(object), loModel.CartItemNew.Quantity);
                        if (lnQuantity <= 0)
                        {
                            ModelState.AddModelError("CartItemNew.Quantity", "Quantity must be an number greater than zero.");
                        }
                    }

                    if (loModel.CartItemNew.ItemPrice <= 0)
                    {
                        ModelState.AddModelError("CartItemNew.ItemPrice", "Price is required");
                    }
                    else
                    {
                        double lnPrice = MaxConvertLibrary.ConvertToDouble(typeof(object), loModel.CartItemNew.ItemPrice);
                        if (lnPrice < 0)
                        {
                            ModelState.AddModelError("CartItemNew.ItemPrice", "Price must be a positive number.");
                        }
                    }

                    if (loModel.CartItemNew.ItemShipping < 0)
                    {
                        ModelState.AddModelError("CartItemNew.ItemShipping", "Shipping is required");
                    }
                    else
                    {
                        double lnShipping = MaxConvertLibrary.ConvertToDouble(typeof(object), loModel.CartItemNew.ItemShipping);
                        if (lnShipping < 0)
                        {
                            ModelState.AddModelError("CartItemNew.ItemShipping", "Shipping must be a positive number.");
                        }
                    }

                    if (ModelState.IsValid)
                    {
                        if (loModel.CartAdd(loModel.CartItemNew))
                        {
                            if (User.Identity.IsAuthenticated)
                            {
                                loModel.Username = User.Identity.Name;
                            }

                            loModel.Save();
                            loModel.SetCurrent();
                        }
                    }
                    else
                    {
                        MaxCartViewModel loModelUpdate = new MaxCartViewModel(loModel.Id);
                        loModelUpdate.CartItemNew.ItemShipping = loModel.CartItemNew.ItemShipping;
                        loModelUpdate.CartItemNew.ItemPrice = loModel.CartItemNew.ItemPrice;
                        loModelUpdate.CartItemNew.Name = loModel.CartItemNew.Name;
                        loModelUpdate.CartItemNew.Note = loModel.CartItemNew.Note;
                        loModelUpdate.CartItemNew.Quantity = loModel.CartItemNew.Quantity;
                        loModelUpdate.CartItemNew.Sku = loModel.CartItemNew.Sku;
                        return View(loModelUpdate);
                    }
                }
            }

            string lsCartUrl = this.Url.Action("Cart") + "?cartid=" + loModel.Id;
            return Redirect(lsCartUrl);
        }

        [HttpGet]
        [MaxRequireHttps]
        [MaxEnableEmbed]
        public virtual ActionResult Order(string cartid)
        {
            MaxCartViewModel loCartModel = new MaxCartViewModel(cartid);
            if (loCartModel.ItemList.Count.Equals(0))
            {
                return RedirectToAction("Cart");
            }

            //// Make sure there is a current order and view that current order
            MaxOrderViewModel loModel = new MaxOrderViewModel();
            if (loModel.EntityLoad() && loModel.Load())
            {
                loModel.AddCart(loCartModel, true);
                loModel.Save();
                loModel.SetCurrent(false);
            }

            if (loModel.ItemList.Count.Equals(0))
            {
                return RedirectToAction("Cart");
            }

            return View(loModel);
        }

        [HttpPost]
        [MaxRequireHttps]
        [MaxEnableEmbed]
        [ValidateInput(false)]
        public virtual ActionResult Order(MaxOrderViewModel loModel, string uoProcess)
        {
            if (User.IsInRole("Admin - App") && !string.IsNullOrEmpty(uoProcess) && uoProcess.Equals("load", StringComparison.InvariantCultureIgnoreCase))
            {
                ModelState.Clear();
                loModel = new MaxOrderViewModel(loModel.Id);
                string lsPersonId = Request.Form["uoPersonId"];
                if (loModel.LoadFromPersonId(lsPersonId))
                {
                    return View(loModel);
                }
            }
            else
            {
                //// Set the status to order started by the current user (only if the status has not been started already).
                if (loModel.SetStartedStatus(User.Identity.Name))
                {
                    //// Save all data related to the order that may have been updated on the order form.
                    loModel.OrderContactPerson.Save(loModel.Id);
                    loModel.OrderContactAddress.Save(loModel.Id);
                    loModel.OrderShippingInfo.Save(loModel.Id);
                    foreach (MaxOrderPaymentViewModel loPayment in loModel.OrderPaymentList)
                    {
                        loPayment.Save(loModel.Id);
                    }
                }
            }

            if (!string.IsNullOrEmpty(uoProcess))
            {
                bool lbByPassValidation = User.IsInRole("Admin - App");

                //// Return the user to the cart if there are no items related to the order.
                if (uoProcess.Equals("EditCart", StringComparison.InvariantCultureIgnoreCase))
                {
                    return RedirectToAction("Cart");
                }

                //// Load the order data and check for consistency.
                loModel = new MaxOrderViewModel(loModel.Id);

                //// Return the user to the cart if there are no items related to the order.
                if (loModel.ItemList.Count.Equals(0))
                {
                    return RedirectToAction("Cart");
                }

                //// Remove all "ID" Keys from the ModelState so that it will load up any new ones from the loaded model.
                string[] laKey = new string[ModelState.Keys.Count];
                ModelState.Keys.CopyTo(laKey, 0);
                foreach (string lsKey in laKey)
                {
                    if (lbByPassValidation && String.IsNullOrEmpty(ModelState[lsKey].Value.AttemptedValue) && ModelState[lsKey].Errors.Count > 0)
                    {
                        //// Remove errors for most fields if the user can bypass validation (admin users)
                        if (lsKey != "OrderContactPerson.CurrentFirstName" &&
                            lsKey != "OrderContactPerson.CurrentLastName" &&
                            lsKey != "OrderContactPerson.Email")
                        {
                            ModelState.Remove(lsKey);
                        }
                    }
                    else if (lsKey.ToLower().Contains("id"))
                    {
                        ModelState.Remove(lsKey);
                    }
                }

                if (lbByPassValidation)
                {
                    if (string.IsNullOrEmpty(loModel.OrderContactPerson.CurrentFirstName))
                    {
                        ModelState.AddModelError("OrderContactPerson.CurrentFirstName", "First name is required.");
                    }

                    if (string.IsNullOrEmpty(loModel.OrderContactPerson.CurrentLastName))
                    {
                        ModelState.AddModelError("OrderContactPerson.CurrentLastName", "Last name is required.");
                    }

                    if (string.IsNullOrEmpty(loModel.OrderContactPerson.Email))
                    {
                        ModelState.AddModelError("OrderContactPerson.Email", "Email is required.");
                    }
                    else
                    {
                        MaxEmailEntity loEntity = MaxEmailEntity.Create();
                        if (!MaxEmailEntity.IsValidEmail(loModel.OrderContactPerson.Email))
                        {
                            ModelState.AddModelError("OrderContactPerson.Email", "Email must be a valid email address.");
                        }
                    }
                }

                //// Validate Shipping data related to the order.
                if (!lbByPassValidation)
                {
                    if (loModel.OrderShippingInfo.IsPickup)
                    {
                        //// Either notes need to be included, or pickup date and time need to be specified.
                        if (string.IsNullOrEmpty(loModel.OrderShippingInfo.Notes))
                        {
                            if (string.IsNullOrEmpty(loModel.OrderShippingInfo.PickupDate) ||
                                string.IsNullOrEmpty(loModel.OrderShippingInfo.PickupTime))
                            {
                                ModelState.AddModelError("OrderShippingInfo.PickupDate", "Either Pickup Date and time are needed, or a note about pickup is needed.");

                            }
                        }
                    }
                    else if (loModel.OrderShippingInfo.ShippingType == MaxShippingTypeEntity.ShippingTypeSpecial)
                    {
                        //// Nothing to check for errors
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(loModel.OrderShippingInfo.ShippingAddress.DeliveryAddress))
                        {
                            ModelState.AddModelError("OrderShippingInfo.ShippingAddress.DeliveryAddress", "The Shipping address field is needed.");
                        }

                        if (string.IsNullOrEmpty(loModel.OrderShippingInfo.ShippingAddress.PostalCode))
                        {
                            ModelState.AddModelError("OrderShippingInfo.ShippingAddress.PostalCode", "The Shipping zip code field is needed.");
                        }

                        if (string.IsNullOrEmpty(loModel.OrderShippingInfo.ShippingAddress.StateCode))
                        {
                            ModelState.AddModelError("OrderShippingInfo.ShippingAddress.StateCode", "The Shipping state field is needed.");
                        }

                        if (string.IsNullOrEmpty(loModel.OrderShippingInfo.ShippingAddress.City))
                        {
                            ModelState.AddModelError("OrderShippingInfo.ShippingAddress.City", "The Shipping city field is needed.");
                        }
                    }
                }

                //// Validate all selected order payment methods related to the order.
                for (int lnM = 0; lnM < loModel.OrderPaymentList.Count; lnM++)
                {
                    if (loModel.OrderPaymentList[lnM].IsSelected == "true")
                    {
                        if (loModel.OrderPaymentList[lnM].OrderPaymentDetail.DetailType == MaxOrderPaymentDetailEntity.PaymentDetailTypeCard)
                        {
                            MaxOrderPaymentDetailViewModel loCard = loModel.OrderPaymentList[lnM].OrderPaymentDetail;

                            //// Validate the credit card data
                            if (string.IsNullOrWhiteSpace(loCard.CardName))
                            {
                                //// Check to make sure the name on the card is not blank.
                                ModelState.AddModelError("OrderPaymentList[" + lnM.ToString() + "].OrderPaymentDetail.CardName", "The name on the credit card is needed.");
                            }

                            int lnYear = MaxConvertLibrary.ConvertToInt(typeof(object), loCard.CardExpireYear);
                            int lnMonth = MaxConvertLibrary.ConvertToInt(typeof(object), loCard.CardExpireMonth);
                            bool lbCheckDate = true;
                            if (lnMonth == int.MinValue)
                            {
                                //// Check to make sure the year is valid is not in the past.
                                ModelState.AddModelError("OrderPaymentList[" + lnM.ToString() + "].OrderPaymentDetail.CardExpireMonth", "Expiration month is needed.");
                                lbCheckDate = false;
                            }
                            else if (lnMonth < 1 || lnMonth > 12)
                            {
                                //// Check to make sure the month is valid.
                                ModelState.AddModelError("OrderPaymentList[" + lnM.ToString() + "].OrderPaymentDetail.CardExpireMonth", "Expiration month is not a valid month.");
                                lbCheckDate = false;
                            }

                            if (lnYear == int.MinValue)
                            {
                                //// Check to make sure the year is valid is not in the past.
                                ModelState.AddModelError("OrderPaymentList[" + lnM.ToString() + "].OrderPaymentDetail.CardExpireYear", "Expiration year is needed.");
                                lbCheckDate = false;
                            }
                            else if (lnYear < DateTime.UtcNow.Year)
                            {
                                //// Check to make sure the year is valid is not in the past.
                                ModelState.AddModelError("OrderPaymentList[" + lnM.ToString() + "].OrderPaymentDetail.CardExpireYear", "Expiration year is in the past.");
                                lbCheckDate = false;
                            }
                            else if (lnYear > DateTime.UtcNow.Year + 30)
                            {
                                //// Check to make sure the year is not too far in the future.
                                ModelState.AddModelError("OrderPaymentList[" + lnM.ToString() + "].OrderPaymentDetail.CardExpireYear", "Expiration year is too far in the future.");
                                lbCheckDate = false;
                            }

                            if (lbCheckDate)
                            {
                                //// Check to make sure the expiration date is in the future.
                                DateTime loTest = new DateTime(lnYear, lnMonth, 1).AddMonths(1);
                                if (loTest < DateTime.Now)
                                {
                                    ModelState.AddModelError("OrderPaymentList[" + lnM.ToString() + "].OrderPaymentDetail.CardExpireYear", "Expiration date is in the past.");
                                }
                            }

                            if (string.IsNullOrWhiteSpace(loCard.CardNumber))
                            {
                                ModelState.AddModelError("OrderPaymentList[" + lnM.ToString() + "].OrderPaymentDetail.CardNumber", "Credit card number is needed.");
                            }
                            else
                            {
                                MaxCreditCardValidationAttribute loCreditCardValidation = new MaxCreditCardValidationAttribute();
                                if (!loCreditCardValidation.IsValid(loCard.CardNumber))
                                {
                                    ModelState.AddModelError("OrderPaymentList[" + lnM.ToString() + "].OrderPaymentDetail.CardNumber", "Credit card number is not valid.");
                                }
                            }

                            if (string.IsNullOrWhiteSpace(loCard.CardVerification))
                            {
                                ModelState.AddModelError("OrderPaymentList[" + lnM.ToString() + "].OrderPaymentDetail.CardVerification", "Credit card verification code is needed.");
                            }
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    bool lbIsPaymentAuthorized = false;
                    //// Authorize payments
                    for (int lnM = 0; lnM < loModel.OrderPaymentList.Count; lnM++)
                    {
                        //// Only process selected payments (or only payment).
                        if (loModel.OrderPaymentList[lnM].IsSelected == "true")
                        {
                            loModel.OrderPaymentList[lnM].Amount = loModel.OrderPaymentList[lnM].CurrentAmount;
                            if (string.IsNullOrEmpty(loModel.OrderPaymentList[lnM].Amount))
                            {
                                loModel.OrderPaymentList[lnM].Amount = loModel.Total.ToString();
                            }

                            string lsError = loModel.OrderPaymentList[lnM].TryVerify();
                            if (lsError.Length > 0)
                            {
                                string lsKey = "Payment" + lnM.ToString();
                                if (loModel.OrderPaymentList[lnM].OrderPaymentDetail.DetailType == MaxFactry.Module.Catalog.BusinessLayer.MaxOrderPaymentDetailEntity.PaymentDetailTypeCard)
                                {
                                    lsKey = "OrderPaymentList[" + lnM + "].OrderPaymentDetail.CardNumber";
                                }

                                loModel.LogMessage("Error Processing Payment Authorization: " + lsError, User.Identity.Name);
                                if (lsError.Contains("\r\n"))
                                {
                                    string[] laError = lsError.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                                    for (int lnE = 0; lnE < laError.Length; lnE++)
                                    {
                                        lsKey = "Payment" + lnM.ToString() + "|" + lnE.ToString();
                                        if (loModel.OrderPaymentList[lnM].PaymentDetailType == MaxOrderPaymentDetailEntity.PaymentDetailTypeCard)
                                        {
                                            lsKey = "OrderPaymentList[" + lnM + "].OrderPaymentDetail.CardNumber";
                                        }

                                        string lsErrorDetail = laError[lnE];
                                        if (lsErrorDetail.Contains("\t"))
                                        {
                                            string[] laErrorDetail = lsErrorDetail.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                                            string lsField = laErrorDetail[0];
                                            string lsMessage = laErrorDetail[1];
                                            if (lsField.EndsWith(".cvv2"))
                                            {
                                                lsKey = "OrderPaymentList[" + lnM + "].OrderPaymentDetail.CardVerification";
                                            }
                                            else if (!lsField.EndsWith(".credit_card"))
                                            {
                                                lsMessage = "Error Processing Payment Authorization: " + lsMessage;
                                            }

                                            ModelState.AddModelError(lsKey, lsMessage);
                                        }
                                        else
                                        {
                                            ModelState.AddModelError(lsKey, "Error Processing Payment Authorization: " + lsErrorDetail);
                                        }
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError(lsKey, "Error Processing Payment Authorization: " + lsError);
                                }

                                //// Send an email about any payment authorization that fails.
                                MaxEmailEntity loEmail = MaxEmailEntity.Create();
                                loEmail.RelationType = "MaxErrorNotification";
                                loEmail.RelationId = MaxConvertLibrary.ConvertToGuid(typeof(object), loModel.OrderPaymentList[lnM].Id);
                                loEmail.Subject = "Error during payment transaction";
                                loEmail.Content = "Error Processing Payment: " + lsError + "\r\n" +
                                    "\r\nOrder Id: " + loModel.Id +
                                    "\r\nStarted Order Detail Link: " + Request.Url.GetLeftPart(UriPartial.Authority) + "/MaxCatalogManage/OrderEdit/" + loModel.Id +
                                    "\r\nContact Name: " + loModel.OrderContactPerson.CurrentFirstName + " " + loModel.OrderContactPerson.CurrentLastName +
                                    "\r\nContact Email: " + loModel.OrderContactPerson.Email +
                                    "\r\nContact Phone: " + loModel.OrderContactPerson.Phone;
                                MaxCartViewModel loCart = new MaxCartViewModel(string.Empty);
                                if (loCart.GetCurrent())
                                {
                                    loEmail.Content += "\r\nCart Takeover Link: " + Request.Url.GetLeftPart(UriPartial.Authority) + "/MaxCatalog/Cart/" + loCart.Id + "?TakeOver=" + loCart.Id;
                                }

                                loEmail.Send();
                            }
                            else
                            {
                                lbIsPaymentAuthorized = true;
                                loModel.LogMessage("Successfully processed payment authorization for type " + loModel.OrderPaymentList[lnM].OrderPaymentDetail.DetailType, User.Identity.Name);
                            }
                        }
                    }

                    if (ModelState.IsValid && !lbIsPaymentAuthorized)
                    {
                        ModelState.AddModelError("PaymentAuthorization", "No payments were successfully authorized. Please double check selected payment methods.");
                    }

                    if (ModelState.IsValid)
                    {
                        if (loModel.ProcessCheckOrder())
                        {
                            string lsUrl = this.Url.Action("OrderConfirm") + "?Id=" + loModel.Id;
                            return Redirect(lsUrl);
                        }
                    }
                }
            }

            return View(loModel);
        }

        [HttpGet]
        [MaxRequireHttps]
        [MaxEnableEmbed]
        public virtual ActionResult OrderConfirm(string id)
        {
            MaxOrderViewModel loModel = new MaxOrderViewModel(id);
            return View(loModel);
        }

        [HttpPost]
        [MaxRequireHttps]
        [MaxEnableEmbed]
        public virtual ActionResult OrderConfirm(MaxOrderViewModel loModel, string uoProcess)
        {
            if (loModel.EntityLoad())
            {
                loModel.Load();
                ModelState.Clear();
                loModel.LogMessage("Started order confirmation process.", User.Identity.Name);
                bool lbSaleSuccess = false;

                if (loModel.ProcessOrderConfirmStart())
                {
                    for (int lnM = 0; lnM < loModel.OrderPaymentList.Count; lnM++)
                    {
                        if (loModel.OrderPaymentList.Count.Equals(1) || loModel.OrderPaymentList[lnM].IsActive)
                        {
                            loModel.OrderPaymentList[lnM].Amount = loModel.OrderPaymentList[lnM].CurrentAmount;
                            if (String.IsNullOrEmpty(loModel.OrderPaymentList[lnM].Amount))
                            {
                                //// If the current amount was not set, then there must be only one payment type that is active and it should be the full amount.
                                loModel.OrderPaymentList[lnM].Amount = loModel.Total.ToString();
                            }

                            loModel.LogMessage("Processing Payment for " + loModel.OrderPaymentList[lnM].Amount + " of Total " + loModel.Total.ToString(), User.Identity.Name);
                            string lsError = loModel.OrderPaymentList[lnM].TrySale();
                            if (lsError.Length > 0)
                            {
                                loModel.LogMessage("Error Processing Payment: " + lsError, User.Identity.Name);
                                string lsKey = "Payment" + lnM.ToString();
                                if (loModel.OrderPaymentList[lnM].OrderPaymentDetail.DetailType == MaxFactry.Module.Catalog.BusinessLayer.MaxOrderPaymentDetailEntity.PaymentDetailTypeCard)
                                {
                                    lsKey = "OrderPaymentList[" + lnM + "].OrderPaymentDetail.CardNumber";
                                }

                                if (lsError.Contains("\r\n"))
                                {
                                    string[] laError = lsError.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                                    for (int lnE = 0; lnE < laError.Length; lnE++)
                                    {
                                        lsKey = "Payment" + lnM.ToString() + "|" + lnE.ToString();
                                        if (loModel.OrderPaymentList[lnM].PaymentDetailType == MaxOrderPaymentDetailEntity.PaymentDetailTypeCard)
                                        {
                                            lsKey = "OrderPaymentList[" + lnM + "].OrderPaymentDetail.CardNumber";
                                        }

                                        string lsErrorDetail = laError[lnE];
                                        if (lsErrorDetail.Contains("\t"))
                                        {
                                            string[] laErrorDetail = lsErrorDetail.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                                            string lsField = laErrorDetail[0];
                                            string lsMessage = laErrorDetail[1];
                                            if (lsField.EndsWith(".cvv2"))
                                            {
                                                lsKey = "OrderPaymentList[" + lnM + "].OrderPaymentDetail.CardVerification";
                                            }
                                            else if (!lsField.EndsWith(".credit_card"))
                                            {
                                                lsMessage = "Error Processing Payment: " + lsMessage;
                                            }

                                            ModelState.AddModelError(lsKey, lsMessage);
                                        }
                                        else
                                        {
                                            ModelState.AddModelError(lsKey, "Error Processing Payment: " + lsErrorDetail);
                                        }
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError(lsKey, "Error Processing Payment: " + lsError);
                                }

                                MaxEmailEntity loEmail = MaxEmailEntity.Create();
                                loEmail.RelationType = "MaxErrorNotification";
                                loEmail.RelationId = MaxConvertLibrary.ConvertToGuid(typeof(object), loModel.OrderPaymentList[lnM].Id);
                                loEmail.Subject = "Error during payment transaction";
                                loEmail.Content = "Error Processing Payment: " + lsError + "\r\n" +
                                    "\r\nOrder Id: " + loModel.Id +
                                    "\r\nStarted Order Detail Link: " + Request.Url.GetLeftPart(UriPartial.Authority) + "/MaxCatalogManage/OrderEdit/" + loModel.Id +
                                    "\r\nContact Name: " + loModel.OrderContactPerson.CurrentFirstName + " " + loModel.OrderContactPerson.CurrentLastName +
                                    "\r\nContact Email: " + loModel.OrderContactPerson.Email +
                                    "\r\nContact Phone: " + loModel.OrderContactPerson.Phone;
                                MaxCartViewModel loCart = new MaxCartViewModel(string.Empty);
                                if (loCart.GetCurrent())
                                {
                                    loEmail.Content += "\r\nCart Takeover Link: " + Request.Url.GetLeftPart(UriPartial.Authority) + "/MaxCatalog/Cart/" + loCart.Id + "?TakeOver=" + loCart.Id;
                                }

                                loEmail.Send();
                            }
                            else
                            {
                                lbSaleSuccess = true;
                                loModel.LogMessage("Successfully processed payment for type " + loModel.OrderPaymentList[lnM].OrderPaymentDetail.DetailType + " for " + loModel.OrderPaymentList[lnM].CurrentAmount, User.Identity.Name);
                            }
                        }
                    }

                    if (ModelState.IsValid && !lbSaleSuccess)
                    {
                        ModelState.AddModelError("TrySaleError", "Error procesing any payment successfully.  Please go back to the Order page and check payment information.");
                    }

                    if (ModelState.IsValid)
                    {
                        if (User.IsInRole("Admin - App"))
                        {
                            System.Web.Security.MembershipUser loUser = Membership.GetUser();
                            loModel.UserName = loUser.UserName;
                            loModel.UserId = loUser.ProviderUserKey.ToString();
                        }

                        if (loModel.ProcessOrderConfirm())
                        {
                            string lsViewPath = "/Views/MaxCatalog/OrderEmail.cshtml";
                            string lsOrderEmail = MaxHtmlHelperLibrary.GetHtml(lsViewPath, loModel, new MaxIndex());
                            string lsOrderEmailInline = MaxDesignLibrary.ConvertToInlineCSS(lsOrderEmail, string.Empty);
                            loModel.SendConfirmationEmail(null, null, null, lsOrderEmailInline);

                            string lsManagementHtml = "<div><a  class='btn-primary' href='https://" + Request.Url.DnsSafeHost + "/MaxCatalogManage/OrderEdit/" + loModel.Id + "'>Manage this Order</a></div>";
                            if (User.IsInRole("Admin - App"))
                            {
                                lsManagementHtml += "<div>Order placed by " + loModel.UserName + "</div>";
                            }

                            string lsOrderManagementEmail = lsOrderEmail.Replace("<!--Order Management Info-->", lsManagementHtml);
                            string lsOrderManagementEmailInline = MaxDesignLibrary.ConvertToInlineCSS(lsOrderManagementEmail, string.Empty);
                            loModel.SendNotificationEmail(null, null, null, lsOrderManagementEmailInline, null, null);

                            loModel.LogMessage("Ended order confirmation process.  Directing user to success page.", User.Identity.Name);
                            string lsUrl = this.Url.Action("OrderConfirmComplete") + "?id=" + loModel.Id;
                            return Redirect(lsUrl);
                        }
                        else
                        {
                            loModel.LogMessage("Ended order confirmation process.  Showing error message.", User.Identity.Name);
                            ModelState.AddModelError("OrderConfirm", "The order was not able to be confirmed.  It may already be confirmed.");
                        }
                    }
                }
                else
                {
                    loModel.LogMessage("Ended order start confirmation process.  Showing error message.", User.Identity.Name);
                    ModelState.AddModelError("OrderConfirmStart", "The order was not able to be confirmed.  It may already be confirmed.");
                }
            }

            return View(loModel);
        }

        [HttpGet]
        [MaxRequireHttps]
        [MaxEnableEmbed]
        public virtual ActionResult OrderConfirmComplete(string id)
        {
            MaxOrderViewModel loModel = new MaxOrderViewModel(id);
            return View(loModel);
        }

        [HttpGet]
        [MaxRequireHttps]
        [MaxEnableEmbed]
        public virtual ActionResult OrderEmail(string id)
        {
            MaxOrderViewModel loOrderModel = new MaxOrderViewModel(id);
            return View(loOrderModel);
        }

        [HttpGet]
        public virtual ActionResult ProductSearch(string s)
        {
            MaxProductViewModel loModel = new MaxProductViewModel();
            loModel.SearchText = s;
            return View(loModel);
        }
    }
}
