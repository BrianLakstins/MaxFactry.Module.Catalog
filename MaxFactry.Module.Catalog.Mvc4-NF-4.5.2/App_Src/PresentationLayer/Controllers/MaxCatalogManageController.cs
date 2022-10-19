// <copyright file="MaxCatalogManageController.cs" company="Lakstins Family, LLC">
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
// <change date="6/30/2014" author="Brian A. Lakstins" description="Add  to the ment controllers.">
// <change date="7/9/2014" author="Brian A. Lakstins" description="Add Cart Add, Cart view, and Cart update methods.">
// <change date="9/16/2014" author="Brian A. Lakstins" description="Added Product Import.">
// <change date="6/22/2017" author="Brian A. Lakstins" description="Fix errors when attempting to upload a product or category image when there is not one actually being uploaded.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.Mvc4.PresentationLayer
{

    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Module.Catalog.BusinessLayer;
    using MaxFactry.Module.Catalog.PresentationLayer;
    using MaxFactry.General.AspNet.IIS.Mvc4.PresentationLayer;
    using MaxFactry.General.AspNet.BusinessLayer;
    using MaxFactry.General.PresentationLayer;

    public class MaxCatalogManageController : MaxManageController
    {
        [AllowAnonymous]
        public ActionResult Index(string id)
        {
            ViewBag.Message = "Hello World Template MVC";
            return View();
        }

        [HttpGet]
        public virtual ActionResult Client(string m)
        {
            return this.Show(new MaxClientViewModel(), m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Client(MaxClientViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "Client";
            string lsSuccessAction = "Client";
            string lsSuccessMessage = loModel.Name + " successfully created.";
            object loResult = this.Create(loModel, uoProcess, lsCancelAction, lsSuccessAction, lsSuccessMessage);
            if (loResult is ActionResult)
            {
                return (ActionResult)loResult;
            }

            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult ClientEdit(string id)
        {
            MaxClientViewModel loModel = new MaxClientViewModel(id);
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ClientEdit(MaxClientViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "Client";
            ActionResult loResult = this.Edit(loModel, uoProcess, lsCancelAction);
            if (loResult is ViewResult)
            {
                ViewBag.Message = "Successfully saved.";
            }

            return loResult;
        }

        [HttpGet]
        public virtual ActionResult DiscountCode(string m)
        {
            return this.Show(new MaxDiscountCodeViewModel(), m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DiscountCode(MaxDiscountCodeViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "DiscountCode";
            string lsSuccessAction = "DiscountCode";
            string lsSuccessMessage = loModel.Name + " successfully created.";
            object loResult = this.Create(loModel, uoProcess, lsCancelAction, lsSuccessAction, lsSuccessMessage);
            if (loResult is ActionResult)
            {
                return (ActionResult)loResult;
            }

            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult DiscountCodeEdit(string id)
        {
            MaxDiscountCodeViewModel loModel = new MaxDiscountCodeViewModel(id);
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DiscountCodeEdit(MaxDiscountCodeViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "DiscountCode";
            ActionResult loResult = this.Edit(loModel, uoProcess, lsCancelAction);
            if (loResult is ViewResult)
            {
                ViewBag.Message = "Successfully saved.";
            }

            return loResult;
        }

        [HttpGet]
        public virtual ActionResult Manufacturer(string m)
        {
            return this.Show(new MaxManufacturerViewModel(), m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Manufacturer(MaxManufacturerViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "Manufacturer";
            string lsSuccessAction = "Manufacturer";
            string lsSuccessMessage = loModel.Name + " successfully created.";
            object loResult = this.Create(loModel, uoProcess, lsCancelAction, lsSuccessAction, lsSuccessMessage);
            if (loResult is ActionResult)
            {
                return (ActionResult)loResult;
            }

            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult ManufacturerEdit(string id)
        {
            MaxManufacturerViewModel loModel = new MaxManufacturerViewModel(id);
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ManufacturerEdit(MaxManufacturerViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "Manufacturer";
            ActionResult loResult = this.Edit(loModel, uoProcess, lsCancelAction);
            if (loResult is ViewResult)
            {
                ViewBag.Message = "Successfully saved.";
            }

            return loResult;
        }

        [HttpGet]
        public virtual ActionResult Vendor(string m)
        {
            return this.Show(new MaxVendorViewModel(), m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Vendor(MaxVendorViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "Vendor";
            string lsSuccessAction = "Vendor";
            string lsSuccessMessage = loModel.Name + " successfully created.";
            object loResult = this.Create(loModel, uoProcess, lsCancelAction, lsSuccessAction, lsSuccessMessage);
            if (loResult is ActionResult)
            {
                return (ActionResult)loResult;
            }

            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult VendorEdit(string id)
        {
            MaxVendorViewModel loModel = new MaxVendorViewModel(id);
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult VendorEdit(MaxVendorViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "Vendor";
            ActionResult loResult = this.Edit(loModel, uoProcess, lsCancelAction);
            if (loResult is ViewResult)
            {
                ViewBag.Message = "Successfully saved.";
            }

            return loResult;
        }

        [HttpGet]
        public virtual ActionResult Catalog(string m)
        {
            return this.Show(new MaxCatalogViewModel(), m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Catalog(MaxCatalogViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "Catalog";
            string lsSuccessAction = "Catalog";
            string lsSuccessMessage = loModel.Name + " successfully created.";
            object loResult = this.Create(loModel, uoProcess, lsCancelAction, lsSuccessAction, lsSuccessMessage);
            if (loResult is ActionResult)
            {
                return (ActionResult)loResult;
            }

            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult CatalogEdit(string id)
        {
            MaxCatalogViewModel loModel = new MaxCatalogViewModel(id);
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CatalogEdit(MaxCatalogViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "Catalog";
            ActionResult loResult = this.Edit(loModel, uoProcess, lsCancelAction);
            if (loResult is ViewResult)
            {
                ViewBag.Message = "Successfully saved.";
            }

            return loResult;
        }

        [HttpGet]
        public virtual ActionResult Category(string m)
        {
            return this.Show(new MaxCategoryViewModel(), m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Category(MaxCategoryViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "Category";
            string lsSuccessAction = "Category";
            string lsSuccessMessage = loModel.Name + " successfully created.";
            object loResult = this.Create(loModel, uoProcess, lsCancelAction, lsSuccessAction, lsSuccessMessage);
            if (loResult is ActionResult)
            {
                return (ActionResult)loResult;
            }

            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult CategoryEdit(string id)
        {
            MaxCategoryViewModel loModel = new MaxCategoryViewModel(id);
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CategoryEdit(MaxCategoryViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "Category";
            ActionResult loResult = this.Edit(loModel, uoProcess, lsCancelAction);
            if (loResult is ViewResult)
            {
                ViewBag.Message = "Successfully saved.";
            }

            return loResult;
        }

        [HttpGet]
        public virtual ActionResult CategoryImage(string m)
        {
            return this.Show(new MaxCategoryImageViewModel(), m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryImage(MaxCategoryViewModel loModel, string uoProcess, HttpPostedFileBase[] laFile)
        {
            if (uoProcess == MaxManageController.ProcessCancel)
            {
                return RedirectToAction("CategoryEdit", new RouteValueDictionary { { "id", loModel.Id } });
            }

            string lsMessage = "Upload Failed";

            if (uoProcess == MaxManageController.ProcessCreate)
            {
                if (laFile != null && laFile.Length > 0 && laFile[0] != null)
                {
                    MaxCategoryImageViewModel loFile = new MaxCategoryImageViewModel();
                    loFile.CategoryId = loModel.Id;
                    if (this.Save(loFile, laFile))
                    {
                        lsMessage = "Upload Succeeded.";
                    }
                }
            }

            return RedirectToAction("CategoryEdit", new RouteValueDictionary { { "id", loModel.Id }, { "m", lsMessage } });
        }

        [HttpGet]
        public virtual ActionResult CategoryImageEdit(string id)
        {
            MaxCategoryImageViewModel loModel = new MaxCategoryImageViewModel(id);
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CategoryImageEdit(MaxCategoryImageViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "Category";
            ActionResult loResult = this.Edit(loModel, uoProcess, lsCancelAction);
            if (loResult is ViewResult)
            {
                ViewBag.Message = "Successfully saved.";
            }

            if (uoProcess == MaxManageController.ProcessCancel || uoProcess == MaxManageController.ProcessDone)
            {
                return RedirectToAction("CategoryEdit", new RouteValueDictionary { { "id", loModel.CategoryId } });
            }

            return loResult;
        }


        [HttpGet]
        public virtual ActionResult Product(string m)
        {
            return this.Show(new MaxProductViewModel(), m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ProductSearch(string SearchText)
        {
            MaxProductViewModel loModel = new MaxProductViewModel();
            loModel.SearchText = SearchText;

            return View("Product", loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public virtual ActionResult Product(MaxProductViewModel loModel, string uoProcess)
        {
            if (!string.IsNullOrEmpty(uoProcess))
            {
                if (uoProcess == "Search")
                {
                    ModelState.Clear();
                    return View(loModel);
                }
                else if (uoProcess == "UpdateSearch")
                {
                    //// Load all products and update the search index for them.
                    ModelState.Clear();
                    MaxEntityList loList = MaxProductEntity.Create().LoadAllCache();
                    for (int lnE = 0; lnE < loList.Count; lnE++)
                    {
                        MaxProductEntity loEntity = loList[lnE] as MaxProductEntity;
                        //// Inactivate all products for display connection 2/23/17
                        /*
                        if (loEntity.IsActive)
                        {
                            loEntity.IsActive = false;
                            loEntity.Update();
                        }
                        */
                        loEntity.UpdateSearch();
                    }

                    //// Deactive all categories for display connection 2/23/2017
                    /*
                    MaxEntityList loCategoryList = MaxCategoryEntity.Create().LoadAllCache();
                    for (int lnE = 0; lnE < loCategoryList.Count; lnE++)
                    {
                        MaxCategoryEntity loEntity = loCategoryList[lnE] as MaxCategoryEntity;
                        //// Inactivate all products for display connection 2/23/17
                        if (loEntity.IsActive)
                        {
                            loEntity.IsActive = false;
                            loEntity.Update();
                        }
                    }
                    */

                    return RedirectToAction("Product", routeValues: new { m = "Updated search for " + loList.Count.ToString() + " products" });
                }
                else
                {
                    string lsCancelAction = "Product";
                    string lsSuccessAction = "ProductEdit";
                    string lsSuccessMessage = loModel.Name + " successfully created.";
                    object loResult = this.Create(loModel, uoProcess, lsCancelAction, lsSuccessAction, lsSuccessMessage);
                    if (loResult is ActionResult)
                    {
                        return (ActionResult)loResult;
                    }
                }
            }

            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult ProductEdit(string id)
        {
            MaxProductViewModel loModel = new MaxProductViewModel(id);
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public virtual ActionResult ProductEdit(MaxProductViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "Product";
            ActionResult loResult = this.Edit(loModel, uoProcess, lsCancelAction);
            if (loResult is ViewResult)
            {
                ViewBag.Message = "Successfully saved.";
            }

            return loResult;
        }

        [HttpGet]
        public virtual ActionResult ProductBranch(string id)
        {
            MaxProductViewModel loModel = new MaxProductViewModel(id);
            Guid loId = loModel.CreateBranch();
            RouteValueDictionary loRoute = new RouteValueDictionary();
            loRoute.Add("id", loId);
            return RedirectToAction("ProductEdit", loRoute);
        }

        [HttpGet]
        public virtual ActionResult ProductChild(string id)
        {
            MaxProductViewModel loModel = new MaxProductViewModel(id);
            Guid loId = loModel.CreateChild();
            RouteValueDictionary loRoute = new RouteValueDictionary();
            loRoute.Add("id", loId);
            return RedirectToAction("ProductEdit", loRoute);
        }

        public ActionResult ProductFile()
        {
            MaxProductFileViewModel loModel = new MaxProductFileViewModel();
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductFile(MaxProductFileViewModel loModel, string uoProcess, HttpPostedFileBase[] laFile)
        {
            string lsCancelAction = "ProductFile";
            string lsSuccessAction = "ProductFile";
            string lsSuccessMessage = "Successfully uploaded.";
            object loResult = this.Create(loModel, uoProcess, lsCancelAction, lsSuccessAction, lsSuccessMessage, laFile);

            if (loResult is ActionResult)
            {
                return (ActionResult)loResult;
            }

            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult ProductFileEdit(string id)
        {
            MaxProductFileViewModel loModel = new MaxProductFileViewModel(id);
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ProductFileEdit(MaxProductFileViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "ProductFile";
            ActionResult loResult = this.Edit(loModel, uoProcess, lsCancelAction);
            if (loResult is ViewResult)
            {
                ViewBag.Message = "Successfully saved.";
            }

            return loResult;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductImage(MaxProductViewModel loModel, string uoProcess, HttpPostedFileBase[] laFile)
        {
            if (uoProcess == MaxManageController.ProcessCancel)
            {
                return RedirectToAction("ProductEdit", new RouteValueDictionary { { "id", loModel.Id } });
            }

            string lsMessage = "Upload Failed";

            if (uoProcess == MaxManageController.ProcessCreate)
            {
                if (laFile != null && laFile.Length > 0 && laFile[0] != null)
                {
                    MaxProductImageViewModel loFile = new MaxProductImageViewModel();
                    loFile.ProductId = loModel.Id;
                    if (this.Save(loFile, laFile))
                    {
                        lsMessage = "Upload Succeeded.";
                    }
                }
            }

            return RedirectToAction("ProductEdit", new RouteValueDictionary { { "id", loModel.Id }, { "m", lsMessage } });
        }


        [HttpGet]
        public virtual ActionResult ProductImage(string m)
        {
            return this.Show(new MaxProductImageViewModel(), m);
        }

        [HttpGet]
        public virtual ActionResult ProductImageEdit(string id)
        {
            MaxProductImageViewModel loModel = new MaxProductImageViewModel(id);
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ProductImageEdit(MaxProductImageViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "Product";
            ActionResult loResult = this.Edit(loModel, uoProcess, lsCancelAction);
            if (loResult is ViewResult)
            {
                ViewBag.Message = "Successfully saved.";
            }

            if (uoProcess == MaxManageController.ProcessCancel || uoProcess == MaxManageController.ProcessDone)
            {
                return RedirectToAction("ProductEdit", new RouteValueDictionary { { "id", loModel.ProductId } });
            }

            return loResult;
        }

        [HttpGet]
        public virtual ActionResult ProductImport()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ProductImport(MaxProductImportViewModel loModel, string uoProcess)
        {
            if (!string.IsNullOrEmpty(uoProcess))
            {
                loModel.Command = uoProcess;
            }

            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult ProductImportStoreSupply()
        {
            MaxProductImportStoreSupplyViewModel loModel = new MaxProductImportStoreSupplyViewModel();
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public virtual ActionResult ProductImportStoreSupply(MaxProductImportStoreSupplyViewModel loModel, string uoProcess)
        {
            if (!string.IsNullOrEmpty(uoProcess))
            {
                loModel.Command = uoProcess;
                ViewBag.Output = loModel.ProcessCommand();
                if (loModel.Redirect)
                {
                    return RedirectToAction("ProductImportStoreSupply");
                }
            }

            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult Order(string Filter)
        {
            MaxOrderViewModel loModel = new MaxOrderViewModel();

            if (null != Filter)
            {
                if (Filter == "UpdateCrm")
                {
                    MaxEntityList loList = MaxOrderEntity.Create().LoadAllCache();
                    for (int lnE = 0; lnE < loList.Count; lnE++)
                    {
                        MaxOrderEntity loOrder = loList[lnE] as MaxOrderEntity;
                        if (loOrder.ProcessingStatus >= MaxOrderEntity.ProcessingStatusPending)
                        {
                            MaxCatalogLibrary.UpdateCrm(loOrder);
                        }
                    }
                }
                else
                {
                    loModel.Filter = Filter;
                }
            }

            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult OrderEdit(string id)
        {
            MaxOrderViewModel loModel = new MaxOrderViewModel(id);
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult OrderEdit(MaxOrderViewModel loModel, string uoProcess)
        {
            if (uoProcess.StartsWith("Status", StringComparison.InvariantCultureIgnoreCase))
            {
                if (loModel.ProcessStatusUpdate(uoProcess, User.Identity.Name))
                {
                    ModelState.Clear();
                }
            }
            else if (uoProcess.StartsWith("Action", StringComparison.InvariantCultureIgnoreCase))
            {
                if (loModel.ProcessActionUpdate(uoProcess, User.Identity.Name))
                {
                    ModelState.Clear();
                }
            }
            else if (uoProcess.Equals("AddTransaction", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!string.IsNullOrEmpty(loModel.StatusChangeNote))
                {
                    loModel.ProcessActionUpdate(uoProcess, User.Identity.Name);
                }

                loModel.EntityLoad();
                loModel.Load();
                foreach (MaxOrderPaymentViewModel loOrderPayment in loModel.OrderPaymentList)
                {
                    string lsAmount = Request.Form["uo" + loOrderPayment.Id + "Amount"];
                    if (!string.IsNullOrEmpty(lsAmount))
                    {
                        string lsProcess = uoProcess;
                        loOrderPayment.CurrentAmount = lsAmount;
                        string lsError = loOrderPayment.TrySale();
                        if (string.IsNullOrEmpty(lsError))
                        {
                            ModelState.Clear();
                            loModel.StatusChangeNote = "Processed transaction for " + lsAmount;
                            lsProcess += "Success";
                        }
                        else
                        {
                            ModelState.AddModelError("AddTransaction", new MaxException(lsError));
                            loModel.StatusChangeNote = "Failed to process transaction for " + lsAmount + " [" + lsError + "]";
                            lsProcess += "Fail";
                        }

                        loModel.ProcessActionUpdate(lsProcess, User.Identity.Name);
                    }

                    loOrderPayment.CurrentAmount = string.Empty;
                }
            }
            else if (uoProcess.StartsWith("TakeOver", StringComparison.InvariantCultureIgnoreCase))
            {
                loModel.EntityLoad();
                if (uoProcess.EndsWith("Cart"))
                {
                    loModel.LogMessage("Order and cart taken over", this.User.Identity.Name);
                    loModel.SetCurrent(true);
                }
                else
                {
                    loModel.LogMessage("Order taken over", this.User.Identity.Name);
                    loModel.SetCurrent(false);
                }

                return Redirect("/MaxCatalog/Order/");
            }
            else if (uoProcess.StartsWith("ResendEmail", StringComparison.InvariantCultureIgnoreCase))
            {
                if (loModel.EntityLoad() && loModel.Load())
                {

                    loModel.LogMessage("Email resent", this.User.Identity.Name);
                    string lsViewPath = "/Views/MaxCatalog/OrderEmail.cshtml";
                    string lsOrderEmail = MaxHtmlHelperLibrary.GetHtml(lsViewPath, loModel, new MaxIndex());
                    string lsOrderEmailInline = MaxDesignLibrary.ConvertToInlineCSS(lsOrderEmail, string.Empty);
                    loModel.SendConfirmationEmail(null, null, null, lsOrderEmailInline);
                }
            }
            else if (uoProcess.StartsWith("Update", StringComparison.InvariantCultureIgnoreCase))
            {
                if (loModel.ProcessUpdate(uoProcess, User.Identity.Name))
                {
                    ModelState.Clear();
                }
            }

            return RedirectToAction("OrderEdit", routeValues: new { id = loModel.Id });
            //return View(loModel);
        }

        public virtual ActionResult OrderChangeStatus(string id, string status)
        {
            MaxOrderViewModel loModel = new MaxOrderViewModel(id);
            string lsMessage = string.Empty;
            if (loModel.ProcessStatusUpdate(status.ToString(), User.Identity.Name))
            {
                lsMessage = "Success";
            }

            return RedirectToAction("Order", routeValues: new { m = lsMessage });
        }

        [HttpGet]
        public virtual ActionResult Cart()
        {
            MaxCartViewModel loModel = new MaxCartViewModel();
            return View(loModel);
        }



        [HttpGet]
        public virtual ActionResult InventorySite(string m)
        {
            return this.Show(new MaxInventorySiteViewModel(), m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult InventorySite(MaxInventorySiteViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "InventorySite";
            string lsSuccessAction = "InventorySite";
            string lsSuccessMessage = loModel.Name + " successfully created.";
            object loResult = this.Create(loModel, uoProcess, lsCancelAction, lsSuccessAction, lsSuccessMessage);
            if (loResult is ActionResult)
            {
                return (ActionResult)loResult;
            }

            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult InventorySiteEdit(string id)
        {
            MaxInventorySiteViewModel loModel = new MaxInventorySiteViewModel(id);
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult InventorySiteEdit(MaxInventorySiteViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "InventorySite";
            ActionResult loResult = this.Edit(loModel, uoProcess, lsCancelAction);
            if (loResult is ViewResult)
            {
                ViewBag.Message = "Successfully saved.";
            }

            return loResult;
        }




        [HttpGet]
        public virtual ActionResult InventorySupply(string m)
        {
            return this.Show(new MaxInventorySupplyViewModel(), m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult InventorySupply(MaxInventorySupplyViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "InventorySupply";
            string lsSuccessAction = "InventorySupply";
            string lsSuccessMessage = loModel.Name + " successfully created.";
            object loResult = this.Create(loModel, uoProcess, lsCancelAction, lsSuccessAction, lsSuccessMessage);
            if (loResult is ActionResult)
            {
                return (ActionResult)loResult;
            }

            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult InventorySupplyEdit(string id)
        {
            MaxInventorySupplyViewModel loModel = new MaxInventorySupplyViewModel(id);
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult InventorySupplyEdit(MaxInventorySupplyViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "InventorySupply";
            ActionResult loResult = this.Edit(loModel, uoProcess, lsCancelAction);
            if (loResult is ViewResult)
            {
                ViewBag.Message = "Successfully saved.";
            }

            return loResult;
        }

        [HttpGet]
        public virtual ActionResult InventoryProduct(string m)
        {
            return this.Show(new MaxInventoryProductViewModel(), m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult InventoryProduct(MaxInventoryProductViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "InventoryProduct";
            string lsSuccessAction = "InventoryProduct";
            string lsSuccessMessage = loModel.Name + " successfully created.";
            object loResult = this.Create(loModel, uoProcess, lsCancelAction, lsSuccessAction, lsSuccessMessage);
            if (loResult is ActionResult)
            {
                return (ActionResult)loResult;
            }

            return View(loModel);
        }

        [HttpGet]
        public virtual ActionResult InventoryProductEdit(string id)
        {
            MaxInventoryProductViewModel loModel = new MaxInventoryProductViewModel(id);
            return View(loModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult InventoryProductEdit(MaxInventoryProductViewModel loModel, string uoProcess)
        {
            string lsCancelAction = "InventoryProduct";
            ActionResult loResult = this.Edit(loModel, uoProcess, lsCancelAction);
            if (loResult is ViewResult)
            {
                ViewBag.Message = "Successfully saved.";
            }

            return loResult;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult InventorySupplyChange(MaxInventorySupplyViewModel loModel, string uoProcess, string uoAmountCurrent, string uoAmountReplenish, string uoReason)
        {
            int lnAmountCurrent = MaxConvertLibrary.ConvertToInt(typeof(object), uoAmountCurrent);
            int lnAmountReplenish = MaxConvertLibrary.ConvertToInt(typeof(object), uoAmountReplenish);
            string lsMessage = "Amount current change is [" + lnAmountCurrent.ToString() + "]\r\n";
            lsMessage = "Amount incoming change is [" + lnAmountReplenish.ToString() + "]\r\n";
            if (lnAmountCurrent > int.MinValue)
            {
                MaxInventorySupplyEntity.ChangeAmountCurrent(MaxConvertLibrary.ConvertToGuid(typeof(object), loModel.Id), lnAmountCurrent, uoReason, User.Identity.Name);
            }

            if (lnAmountReplenish > int.MinValue)
            {
                MaxInventorySupplyEntity.ChangeAmountReplenish(MaxConvertLibrary.ConvertToGuid(typeof(object), loModel.Id), lnAmountReplenish, uoReason, User.Identity.Name);
            }

            return RedirectToAction("InventorySupplyEdit", new RouteValueDictionary { { "id", loModel.Id }, { "m", lsMessage } });
        }

    }
}
