// <copyright file="MaxOrderViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="4/4/2015" author="Brian A. Lakstins" description="Initial creation.">
// <change date="4/14/2016" author="Brian A. Lakstins" description="Include product manual discount information when moving items from cart to order.">
// <change date="10/24/2017" author="Brian A. Lakstins" description="Update so changes are not detected when none are made.">
// <change date="11/8/2017" author="Brian A. Lakstins" description="Update to load orders based on status instead of loading them all.">
// <change date="11/8/2017" author="Brian A. Lakstins" description="Sort orders.">
// <change date="3/7/2018" author="Brian A. Lakstins" description="Update usage of cart item mapping.">
// <change date="7/11/2018" author="Brian A. Lakstins" description="Make sure order is not set to started after it has been placed.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.PresentationLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Module.Catalog.BusinessLayer;
    using MaxFactry.Module.Crm.BusinessLayer;
    using MaxFactry.Module.Crm.PresentationLayer;

    /// <summary>
    /// View model for content.
    /// </summary>
    public class MaxOrderViewModel : MaxFactry.Base.PresentationLayer.MaxBaseIdViewModel
    {
        private List<MaxOrderItemViewModel> _oItemList = null;

        private MaxOrderContactPersonViewModel _oOrderContactPerson = null;

        private MaxOrderContactAddressViewModel _oOrderContactAddress = null;

        private MaxOrderShippingInfoViewModel _oOrderShippingInfo = null;

        private List<MaxOrderProcessLogViewModel> _oLogList = null;

        private MaxShippingTypeEntity _oShippingTypeEntity = null;

        private List<MaxOrderPaymentViewModel> _oOrderPaymentList = null;

        private const string _sIdKey = "MaxCatalogOrderIdCurrent";

        private const string _sEntityKey = "MaxCatalogOrderEntityCurrent";

        private List<MaxCartViewModel> _oCartList = null;

        public const string FilterStarted = "Started";

        public const string FilterArchived = "Archived";

        public const string FilterPending = "Pending";

        public const string FilterCancelled = "Cancelled";

        public const string FilterCurrent = "Current";

        /// <summary>
        /// Initializes a new instance of the MaxOrderViewModel class
        /// </summary>
        public MaxOrderViewModel()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderViewModel class
        /// </summary>
        /// <param name="loEntity">Entity to use as data.</param>
        public MaxOrderViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the MaxOrderViewModel class
        /// </summary>
        /// <param name="lsId">Id to load</param>
        public MaxOrderViewModel(string lsId) : base(lsId)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxOrderEntity.Create();
        }

        public override bool EntityLoad()
        {
            if (!base.EntityLoad())
            {
                if (!this.GetCurrent())
                {
                    this.Entity = MaxOrderEntity.Create();
                }
            }

            MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
            if (loEntity.ProcessingStatus < MaxOrderEntity.ProcessingStatusPending)
            {
                if (loEntity.Recalculate())
                {
                    if (Guid.Empty.Equals(loEntity.Id))
                    {
                        loEntity.Insert();
                    }
                    else
                    {
                        loEntity.Update();
                    }
                }
            }

            this._oItemList = null;
            this._oOrderContactAddress = null;
            this._oOrderContactPerson = null;
            this._oOrderShippingInfo = null;
            this._oLogList = null;
            this._oOrderPaymentList = null;
            return true;
        }

        public int ProcessingStatus
        {
            get;
            set;
        }

        [Display(Name = "Order Status")]
        public string ProcessingStatusText
        {
            get;
            set;
        }

        public string ItemTotal
        {
            get; set;
        }

        public double Total
        {
            get;
            set;
        }

        public string ItemCount
        {
            get;
            set;
        }

        public double ShippingTotal
        {
            get;
            set;
        }

        public int ShippingType
        {
            get; set;
        }

        public string OrderPlacedDate
        {
            get;
            set;
        }

        [Display(Name = "Note")]
        public string StatusChangeNote
        {
            get;
            set;
        }

        [Display(Name = "Visible to Customer")]
        public bool IsStatusChangeNoteVisibleToCustomer
        {
            get;
            set;
        }

        [Display(Name = "Sales Tax")]
        public string UpdateSalesTax
        {
            get;
            set;
        }

        [Display(Name = "Shipping")]
        public string UpdateShipping
        {
            get;
            set;
        }

        public string DiscountTotal
        {
            get;
            set;
        }

        public string DiscountTotalExplanation
        {
            get;
            set;
        }

        public string SubTotal
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string UserId
        {
            get;
            set;
        }

        public string ProcessingUserId
        {
            get;
            set;
        }

        public string ConfirmationNoticeContent
        {
            get;
            set;
        }

        public string ManagementNoticeContent
        {
            get;
            set;
        }

        public string Filter
        {
            get;
            set;

        }

        public double TaxTotal
        {
            get;
            set;

        }

        public string ManualDiscount
        {
            get;
            set;
        }

        public string ManualDiscountExplanation
        {
            get;
            set;
        }

        public string ExternalOrderId
        {
            get;
            set;
        }

        public string ExternalOrderType
        {
            get;
            set;
        }

        public double TaxableAmount
        {
            get;
            set;
        }

        public bool HasBeenDone(string lsProcessingStep)
        {
            return false;


        }

        public MaxShippingTypeEntity ShippingTypeEntity
        {
            get
            {
                if (null == this._oShippingTypeEntity)
                {
                    this._oShippingTypeEntity = MaxShippingTypeEntity.Create();
                    this._oShippingTypeEntity.LoadByShippingType(this.ShippingType);
                }

                return this._oShippingTypeEntity;
            }

        }

        /*
        public List<MaxShippingTypeEntity> ShippingTypeList
        {
            get
            {
                return MaxShippingTypeEntity.Create().GetShippingTypeList();
            }
        }
        */

        public List<MaxOrderItemViewModel> ItemList
        {
            get
            {
                if (null == this._oItemList)
                {
                    this._oItemList = new List<MaxOrderItemViewModel>();
                    MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
                    foreach (MaxOrderItemEntity loItemEntity in loEntity.ItemList)
                    {
                        MaxOrderItemViewModel loModel = new MaxOrderItemViewModel(loItemEntity);
                        loModel.Load();
                        this._oItemList.Add(loModel);
                    }
                }

                return this._oItemList;
            }

            set
            {
                this._oItemList = value;
            }
        }

        public List<MaxOrderProcessLogViewModel> LogList
        {
            get
            {
                if (null == this._oLogList)
                {
                    this._oLogList = new List<MaxOrderProcessLogViewModel>();
                    MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
                    foreach (MaxOrderProcessLogEntity loLog in loEntity.LogList)
                    {
                        MaxOrderProcessLogViewModel loModel = new MaxOrderProcessLogViewModel(loLog);
                        loModel.Load();
                        this._oLogList.Add(loModel);
                    }
                }

                return this._oLogList;
            }
        }

        public MaxOrderContactPersonViewModel OrderContactPerson
        {
            get
            {
                if (null == this._oOrderContactPerson)
                {
                    MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
                    this._oOrderContactPerson = new MaxOrderContactPersonViewModel(loEntity.OrderContactPerson);
                    this._oOrderContactPerson.Load();
                }

                return this._oOrderContactPerson;
            }

            set
            {
                this._oOrderContactPerson = value;
            }
        }

        public MaxOrderContactAddressViewModel OrderContactAddress
        {
            get
            {
                if (null == this._oOrderContactAddress)
                {
                    MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
                    this._oOrderContactAddress = new MaxOrderContactAddressViewModel(loEntity.OrderContactAddress);
                    this._oOrderContactAddress.Load();
                }

                return this._oOrderContactAddress;
            }

            set
            {
                this._oOrderContactAddress = value;
            }
        }

        public MaxOrderShippingInfoViewModel OrderShippingInfo
        {
            get
            {
                if (null == this._oOrderShippingInfo)
                {
                    MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
                    this._oOrderShippingInfo = new MaxOrderShippingInfoViewModel(loEntity.OrderShippingInfo);
                    this._oOrderShippingInfo.Load();
                    this._oOrderShippingInfo.ShippingType = this.ShippingType;
                }

                return this._oOrderShippingInfo;
            }

            set
            {
                this._oOrderShippingInfo = value;
            }
        }

        /// <summary>
        /// Order payment list
        /// </summary>
        public List<MaxOrderPaymentViewModel> OrderPaymentList
        {
            get
            {
                if (null == this._oOrderPaymentList)
                {
                    this._oOrderPaymentList = new List<MaxOrderPaymentViewModel>();
                    MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
                    foreach (MaxOrderPaymentEntity loOrderPayment in loEntity.OrderPaymentList)
                    {
                        MaxOrderPaymentViewModel loModel = new MaxOrderPaymentViewModel(loOrderPayment);
                        loModel.Load();
                        this._oOrderPaymentList.Add(loModel);
                    }
                }

                return this._oOrderPaymentList;
            }

            set
            {
                this._oOrderPaymentList = value;
            }
        }

        public Dictionary<int, string> ProcessingStatusIndex
        {
            get
            {
                MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
                return loEntity.ProcessingStatusIndex;
            }
        }

        public virtual Dictionary<short, string> ProcessingActionIndex
        {
            get
            {
                MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
                return loEntity.ProcessingActionIndex;
            }
        }

        public bool GetProcessingAction(short lnFlag)
        {
            MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
            return loEntity.GetProcessingAction(lnFlag);
        }

        public void LogMessage(string lsMessage, string lsUsername)
        {
            MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
            loEntity.LogMessage(lsMessage, lsUsername);
        }

        public bool SetStartedStatus(string lsUserName)
        {
            if (this.ProcessingStatus <= MaxOrderEntity.ProcessingStatusStarted)
            {
                MaxOrderEntity loEntity = MaxOrderEntity.Create();
                if (loEntity.LoadByIdCache(MaxConvertLibrary.ConvertToGuid(typeof(object), this.Id)) && loEntity.ProcessingStatus <= MaxOrderEntity.ProcessingStatusStarted)
                {
                    int lnProcessingStatusCurrent = loEntity.ProcessingStatus;
                    this.ProcessingStatus = MaxOrderEntity.ProcessingStatusStarted;
                    this.Save();
                    loEntity = this.Entity as MaxOrderEntity;
                    loEntity.LogMessage("Changed status from [" + this.GetStatusText(lnProcessingStatusCurrent) + "]  to [" + this.GetStatusText(MaxOrderEntity.ProcessingStatusStarted) + "]", lsUserName);
                    return true;
                }
            }

            return false;
        }

        public int GetCount(string lsFilter)
        {
            int lnR = 0;
            if (lsFilter.Equals(FilterArchived))
            {
                lnR = MaxOrderEntity.Create().LoadAllByProcessingStatusCache(new int[] { MaxOrderEntity.ProcessingStatusArchived }).Count;
            }
            else if (lsFilter.Equals(FilterPending))
            {
                lnR = MaxOrderEntity.Create().LoadAllByProcessingStatusCache(new int[] { MaxOrderEntity.ProcessingStatusPending }).Count;
            }
            else if (lsFilter.Equals(FilterStarted))
            {
                MaxEntityList loStartedList = MaxOrderEntity.Create().LoadAllByProcessingStatusCache(new int[] { MaxOrderEntity.ProcessingStatusStarted });
                for (int lnE = 0; lnE < loStartedList.Count; lnE++)
                {
                    MaxOrderEntity loEntity = loStartedList[lnE] as MaxOrderEntity;
                    if (loEntity.ProcessingStatus == MaxOrderEntity.ProcessingStatusStarted && loEntity.LastUpdateDate.AddMonths(1) > DateTime.UtcNow)
                    {
                        lnR++;
                    }
                }
            }
            else if (lsFilter.Equals(FilterCancelled))
            {
                lnR = MaxOrderEntity.Create().LoadAllByProcessingStatusCache(new int[] { MaxOrderEntity.ProcessingStatusCancelled }).Count;
            }
            else
            {
                lnR = MaxOrderEntity.Create().LoadAllByProcessingStatusCache(new int[] { MaxOrderEntity.ProcessingStatusConfirmed,
                    MaxOrderEntity.ProcessingStatus2000,
                    MaxOrderEntity.ProcessingStatus3000,
                    MaxOrderEntity.ProcessingStatus4000,
                    MaxOrderEntity.ProcessingStatus5000,
                    MaxOrderEntity.ProcessingStatus6000,
                    MaxOrderEntity.ProcessingStatus7000,
                    MaxOrderEntity.ProcessingStatus8000,
                    MaxOrderEntity.ProcessingStatus9000 }).Count;
            }


            return lnR;
        }

        /// <summary>
        /// Gets a sorted list of all 
        /// Can use Generic List if supported in the framework.
        /// </summary>
        /// <returns>List of ViewModels</returns>
        public virtual List<MaxOrderViewModel> GetSortedListByStatusFilter()
        {
            List<MaxOrderViewModel> loR = new List<MaxOrderViewModel>();
            MaxEntityList loList = null;
            string lsFilter = this.Filter;
            if (null == lsFilter)
            {
                lsFilter = string.Empty;
            }

            if (lsFilter.Equals(FilterArchived))
            {
                loList = MaxOrderEntity.Create().LoadAllByProcessingStatusCache(new int[] { MaxOrderEntity.ProcessingStatusArchived });
            }
            else if (lsFilter.Equals(FilterPending))
            {
                loList = MaxOrderEntity.Create().LoadAllByProcessingStatusCache(new int[] { MaxOrderEntity.ProcessingStatusPending });
            }
            else if (lsFilter.Equals(FilterStarted))
            {
                loList = MaxOrderEntity.Create().LoadAllByProcessingStatusCache(new int[] { MaxOrderEntity.ProcessingStatusStarted });
            }
            else if (lsFilter.Equals(FilterCancelled))
            {
                loList = MaxOrderEntity.Create().LoadAllByProcessingStatusCache(new int[] { MaxOrderEntity.ProcessingStatusCancelled });
            }
            else
            {
                loList = MaxOrderEntity.Create().LoadAllByProcessingStatusCache(new int[] { MaxOrderEntity.ProcessingStatusConfirmed,
                    MaxOrderEntity.ProcessingStatus2000,
                    MaxOrderEntity.ProcessingStatus3000,
                    MaxOrderEntity.ProcessingStatus4000,
                    MaxOrderEntity.ProcessingStatus5000,
                    MaxOrderEntity.ProcessingStatus6000,
                    MaxOrderEntity.ProcessingStatus7000,
                    MaxOrderEntity.ProcessingStatus8000,
                    MaxOrderEntity.ProcessingStatus9000 });
            }

            SortedList<string, MaxOrderViewModel> loSortedList = new SortedList<string, MaxOrderViewModel>();
            for (int lnE = 0; lnE < loList.Count; lnE++)
            {
                MaxOrderEntity loEntity = loList[lnE] as MaxOrderEntity;
                if ((loEntity.ProcessingStatus == MaxOrderEntity.ProcessingStatusStarted && loEntity.LastUpdateDate.AddMonths(1) > DateTime.UtcNow) ||
                    loEntity.ProcessingStatus > MaxOrderEntity.ProcessingStatusStarted)
                {
                    MaxOrderViewModel loModel = new MaxOrderViewModel(loEntity);
                    if (loModel.Load())
                    {
                        loSortedList.Add(MaxConvertLibrary.ConvertToSortString(typeof(object), DateTime.Parse(loModel.OrderPlacedDate)) + Guid.NewGuid().ToString(), loModel);
                    }
                }
            }

            loR.AddRange(loSortedList.Values);
            if (FilterArchived.Equals(this.Filter) ||
                FilterStarted.Equals(this.Filter))
            {
                loR.Reverse();
            }

            return loR;
        }

        public bool ProcessCheckOrder()
        {
            return true;
        }

        public bool ProcessOrderConfirmStart()
        {
            bool lbR = false;
            if (this.EntityLoad())
            {
                MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
                if (loEntity.ConfirmStart())
                {
                    lbR = true;
                }

                this.MapFromEntity();
            }

            return lbR;
        }

        public bool ProcessOrderConfirm()
        {
            bool lbR = false;
            if (this.EntityLoad())
            {
                MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
                loEntity.Username = this.UserName;
                loEntity.UserId = this.UserId;
                if (loEntity.ConfirmEnd())
                {
                    MaxCatalogLibrary.UpdateCrm(loEntity);
                    lbR = true;
                }

                this.MapFromEntity();
            }

            return lbR;
        }

        public bool SendConfirmationEmail(string lsFromAddress, string lsFromName, string lsSubject, string lsContent)
        {
            bool lbR = false;
            if (this.EntityLoad())
            {
                MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
                loEntity.SendConfirmationEmail(lsFromAddress, lsFromName, lsSubject, lsContent);
                this.LogMessage("Sent confirmation email", string.Empty);
                lbR = true;
            }

            return lbR;
        }

        public bool SendNotificationEmail(string lsFromAddress, string lsFromName, string lsSubject, string lsContent, string lsToAddress, string lsToName)
        {
            bool lbR = false;
            if (this.EntityLoad())
            {
                MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
                loEntity.SendNotificationEmail(lsFromAddress, lsFromName, lsSubject, lsContent, lsToAddress, lsToName);
                this.LogMessage("Sent notification email", string.Empty);
                lbR = true;
            }

            return lbR;
        }

        public bool ProcessStatusUpdate(string lsStatus, string lsUsername)
        {
            if (this.EntityLoad())
            {
                MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
                if (lsStatus != "StatusNote")
                {
                    int lnStatus = MaxConvertLibrary.ConvertToInt(typeof(object), lsStatus.Replace("Status", string.Empty));
                    if (lnStatus > int.MinValue)
                    {
                        if (lnStatus == MaxOrderEntity.ProcessingStatusCancelled ||
                            (loEntity.ProcessingStatus == MaxOrderEntity.ProcessingStatusCancelled && lnStatus == MaxOrderEntity.ProcessingStatusConfirmed))
                        {
                            int lnCount = 0;
                            foreach (MaxOrderPaymentEntity loPayment in loEntity.OrderPaymentList)
                            {
                                foreach (MaxOrderPaymentTransactionEntity loTransaction in loPayment.TransactionList)
                                {
                                    if (loTransaction.IsCollected)
                                    {
                                        lnCount++;
                                    }
                                }
                            }

                            if (lnCount > 0)
                            {
                                if (lnStatus == MaxOrderEntity.ProcessingStatusCancelled)
                                {
                                    //// If it was shipped, then unship it.
                                    if (loEntity.GetProcessingAction(4))
                                    {
                                        loEntity.SetProcessingAction(4, false);
                                    }

                                    if (MaxInventoryProductEntity.UpdateInventoryAmount(loEntity, MaxInventoryProductEntity.OnCancel))
                                    {
                                        loEntity.LogMessage("Updated product inventory when cancelled.", null);
                                    }
                                }
                                else if (loEntity.ProcessingStatus == MaxOrderEntity.ProcessingStatusCancelled)
                                {
                                    if (!loEntity.GetProcessingAction(4))
                                    {
                                        if (MaxInventoryProductEntity.UpdateInventoryAmount(loEntity, MaxInventoryProductEntity.OnUnCancel))
                                        {
                                            loEntity.LogMessage("Updated product inventory when uncancelled.", null);
                                        }
                                    }
                                }
                            }
                        }

                        int lnProcessingStatusCurrent = loEntity.ProcessingStatus;
                        loEntity.ProcessingStatus = lnStatus;
                        loEntity.Update();
                        loEntity.LogMessage("Changed status from " + this.GetStatusText(lnProcessingStatusCurrent) + "] to [" + this.GetStatusText(lnStatus) + "]", lsUsername);
                    }
                }

                if (!string.IsNullOrEmpty(this.StatusChangeNote))
                {
                    loEntity.LogMessage(this.StatusChangeNote, lsUsername, this.IsStatusChangeNoteVisibleToCustomer);
                    this.StatusChangeNote = string.Empty;
                    this.IsStatusChangeNoteVisibleToCustomer = false;
                }

                if (this.EntityLoad())
                {
                    return this.Load();
                }
            }

            return false;
        }

        public bool ProcessActionUpdate(string lsAction, string lsUsername)
        {
            if (this.EntityLoad())
            {
                MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;

                if (lsAction.Contains("Action"))
                {
                    short lnActionFlag = 0;
                    bool lbValue = true;
                    if (lsAction.Contains("Undo"))
                    {
                        lnActionFlag = Convert.ToInt16(MaxConvertLibrary.ConvertToInt(typeof(object), lsAction.Replace("ActionUndo", string.Empty)));
                        lbValue = false;
                    }
                    else
                    {
                        lnActionFlag = Convert.ToInt16(MaxConvertLibrary.ConvertToInt(typeof(object), lsAction.Replace("Action", string.Empty)));
                    }

                    if (lnActionFlag > 0)
                    {
                        loEntity.SetProcessingAction(lnActionFlag, lbValue);
                        loEntity.Update();
                        if (lbValue)
                        {
                            loEntity.LogMessage("Processed [" + loEntity.ProcessingActionIndex[lnActionFlag] + "]", lsUsername);
                        }
                        else
                        {
                            loEntity.LogMessage("Processed Undo for [" + loEntity.ProcessingActionIndex[lnActionFlag] + "]", lsUsername);
                        }
                    }
                }

                if (lsAction.Equals("AddTransactionSuccess", StringComparison.InvariantCultureIgnoreCase))
                {
                    //// If this is the first transaction, then update the inventory on order
                    if (loEntity.Total > 0)
                    {
                        int lnCount = 0;
                        foreach (MaxOrderPaymentEntity loPayment in loEntity.OrderPaymentList)
                        {
                            foreach (MaxOrderPaymentTransactionEntity loTransaction in loPayment.TransactionList)
                            {
                                if (loTransaction.IsCollected)
                                {
                                    lnCount++;
                                }
                            }
                        }

                        if (lnCount == 1)
                        {
                            if (MaxInventoryProductEntity.UpdateInventoryAmount(loEntity, MaxInventoryProductEntity.OnPayment))
                            {
                                loEntity.LogMessage("Updated product inventory on first payment.", null);
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(this.StatusChangeNote))
                {
                    loEntity.LogMessage(this.StatusChangeNote, lsUsername, this.IsStatusChangeNoteVisibleToCustomer);
                    this.StatusChangeNote = string.Empty;
                    this.IsStatusChangeNoteVisibleToCustomer = false;
                }

                if (this.EntityLoad())
                {
                    return this.Load();
                }
            }

            return false;
        }

        public bool ProcessUpdate(string lsUpdate, string lsUsername)
        {
            if (this.EntityLoad())
            {
                MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
                if (lsUpdate.EndsWith("SalesTax", StringComparison.InvariantCultureIgnoreCase))
                {
                    double lnSalesTax = MaxConvertLibrary.ConvertToDouble(typeof(object), this.UpdateSalesTax);
                    if (lnSalesTax >= 0)
                    {
                        loEntity.LogMessage("Sales Tax updated from " + string.Format("{0:C}", loEntity.TaxTotal) + " to " + string.Format("{0:C}", lnSalesTax), lsUsername, false);
                        loEntity.TaxLocation = "Override";
                        loEntity.TaxTotal = lnSalesTax;
                        loEntity.Recalculate();
                        loEntity.Update();
                        if (lnSalesTax == 0)
                        {
                            loEntity.LogMessage("Customer claims to be tax exempt", lsUsername, false);
                            loEntity.LogMessage("Farm Tax Exempt Status", lsUsername, true);
                        }
                    }
                }
                else if (lsUpdate.EndsWith("Shipping", StringComparison.InvariantCultureIgnoreCase))
                {
                    double lnShipping = MaxConvertLibrary.ConvertToDouble(typeof(object), this.UpdateShipping);
                    if (lnShipping >= 0)
                    {
                        loEntity.LogMessage("Shipping updated from " + string.Format("{0:C}", loEntity.ShippingTotal) + " to " + string.Format("{0:C}", lnShipping), lsUsername, false);
                        loEntity.IsShippingTotalOverride = true;
                        loEntity.ShippingTotal = lnShipping;
                        loEntity.Recalculate();
                        loEntity.Update();
                    }
                }
            }

            return false;
        }

        private MaxEntityList _oOrderLast2MonthList = null;

        protected MaxEntityList GetAllLast2Months()
        {
            if (null != this._oOrderLast2MonthList)
            {
                return this._oOrderLast2MonthList;
            }

            MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
            DateTime ldStart = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(-1);
            DateTime ldEnd = ldStart.AddMonths(2);
            MaxEntityList loR = loEntity.LoadAllByOrderPlacedDate(ldStart, ldEnd);
            this._oOrderLast2MonthList = loR;
            return loR;
        }

        public double GetMonthTotalLastInternal()
        {
            MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
            MaxEntityList loList = this.GetAllLast2Months();
            DateTime ldStart = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(-1);
            DateTime ldEnd = ldStart.AddMonths(1);
            double lnR = 0;
            for (int lnE = 0; lnE < loList.Count; lnE++)
            {
                MaxOrderEntity loOrder = loList[lnE] as MaxOrderEntity;
                if (loOrder.OrderPlacedDate >= ldStart && loOrder.OrderPlacedDate < ldEnd &&
                    loOrder.ProcessingStatus >= MaxOrderEntity.ProcessingStatusPending)
                {
                    if (loOrder.Username != string.Empty)
                    {
                        lnR += loOrder.ItemTotal;
                    }
                }
            }

            return lnR;
        }

        public List<MaxCrmPersonViewModel> ContactPersonList
        {
            get
            {
                List<MaxCrmPersonViewModel> loR = new List<MaxCrmPersonViewModel>();
                MaxEntityList loList = MaxCrmPersonEntity.Create().LoadAllCache();
                SortedList<string, MaxCrmPersonEntity> loSortedList = new SortedList<string, MaxCrmPersonEntity>();

                for (int lnE = 0; lnE < loList.Count; lnE++)
                {
                    MaxCrmPersonEntity loEntity = loList[lnE] as MaxCrmPersonEntity;
                    if (null != loEntity)
                    {
                        string lsKey = loEntity.Name;
                        if (!string.IsNullOrEmpty(loEntity.MainEmail))
                        {
                            lsKey += " (" + loEntity.MainEmail + ")";
                        }

                        if (loSortedList.ContainsKey(lsKey))
                        {
                            if (loEntity.SourceDate > loSortedList[lsKey].SourceDate)
                            {
                                loSortedList[lsKey] = loEntity;
                            }
                        }
                        else
                        {
                            loSortedList.Add(lsKey, loEntity);
                        }
                    }
                }

                foreach (MaxFactry.Base.BusinessLayer.MaxBasePersonEntity loEntity in loSortedList.Values)
                {
                    MaxCrmPersonViewModel loModel = new MaxCrmPersonViewModel(loEntity);
                    loModel.Load();
                    loR.Add(loModel);
                }

                return loR;
            }
        }


        public double GetMonthTotalLastExternal()
        {
            MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
            MaxEntityList loList = this.GetAllLast2Months();
            DateTime ldStart = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(-1);
            DateTime ldEnd = ldStart.AddMonths(1);
            double lnR = 0;
            for (int lnE = 0; lnE < loList.Count; lnE++)
            {
                MaxOrderEntity loOrder = loList[lnE] as MaxOrderEntity;
                if (loOrder.OrderPlacedDate >= ldStart && loOrder.OrderPlacedDate < ldEnd &&
                    loOrder.ProcessingStatus >= MaxOrderEntity.ProcessingStatusPending)
                {
                    if (loOrder.Username == string.Empty)
                    {
                        lnR += loOrder.ItemTotal;
                    }
                }
            }

            return lnR;
        }

        public double GetMonthTotalThis()
        {
            MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
            MaxEntityList loList = this.GetAllLast2Months();
            DateTime ldStart = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            DateTime ldEnd = ldStart.AddMonths(1);
            double lnR = 0;
            for (int lnE = 0; lnE < loList.Count; lnE++)
            {
                MaxOrderEntity loOrder = loList[lnE] as MaxOrderEntity;
                if (loOrder.OrderPlacedDate >= ldStart && loOrder.OrderPlacedDate < ldEnd &&
                    loOrder.ProcessingStatus >= MaxOrderEntity.ProcessingStatusPending)
                {
                    lnR += loOrder.ItemTotal;
                }
            }

            return lnR;
        }

        public virtual string GetStatusText(int lnProcessingStatus)
        {
            string lsR = "Unknown";
            MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
            if (MaxOrderEntity.ProcessingStatusConfirmed <= lnProcessingStatus && lnProcessingStatus < MaxOrderEntity.ProcessingStatusArchived)
            {
                lsR = string.Empty;
                foreach (short lnKey in this.ProcessingActionIndex.Keys)
                {
                    if (!this.GetProcessingAction(lnKey))
                    {
                        bool lbAdd = true;
                        if (lnKey == 1 && lnProcessingStatus >= MaxOrderEntity.ProcessingStatus2000)
                        {
                            lbAdd = false;
                        }
                        else if (lnKey == 2 && lnProcessingStatus >= MaxOrderEntity.ProcessingStatus3000)
                        {
                            lbAdd = false;
                        }
                        else if (lnKey == 3 && lnProcessingStatus >= MaxOrderEntity.ProcessingStatus5000)
                        {
                            lbAdd = false;
                        }
                        else if (lnKey == 4 && lnProcessingStatus >= MaxOrderEntity.ProcessingStatus6000)
                        {
                            lbAdd = false;
                        }
                        else if (lnKey == 5 && lnProcessingStatus >= MaxOrderEntity.ProcessingStatus7000)
                        {
                            lbAdd = false;
                        }
                        else if (lnKey == 6 && lnProcessingStatus >= MaxOrderEntity.ProcessingStatus8000)
                        {
                            lbAdd = false;
                        }

                        if (lbAdd)
                        {
                            lsR += this.ProcessingActionIndex[lnKey] + "\r\n";
                        }
                    }
                }

                if (string.IsNullOrEmpty(lsR))
                {
                    lsR = "Complete";
                }
                else
                {
                    lsR = "Needs\r\n" + lsR;
                }
            }
            else if (this.ProcessingStatusIndex.ContainsKey(lnProcessingStatus))
            {
                lsR = this.ProcessingStatusIndex[lnProcessingStatus];
            }

            return lsR;
        }

        /// <summary>
        /// Find latest order related to the contact person and load the information from it.
        /// </summary>
        /// <param name="lsContactPersonId"></param>
        /// <returns></returns>
        public virtual bool LoadFromPersonId(string lsPersonId)
        {
            bool lbR = false;

            MaxCrmPersonEntity loPerson = MaxCrmPersonEntity.Create();
            Guid loId = MaxConvertLibrary.ConvertToGuid(typeof(object), lsPersonId);

            if (loPerson.LoadByIdCache(loId))
            {
                if (loPerson.SourceType == "MaxCatalogOrder")
                {
                    lbR = true;
                    MaxOrderViewModel loModel = new MaxOrderViewModel(loPerson.SourceId);
                    this.OrderContactPerson.CurrentFirstName = loModel.OrderContactPerson.CurrentFirstName;
                    this.OrderContactPerson.CurrentLastName = loModel.OrderContactPerson.CurrentLastName;
                    this.OrderContactPerson.Phone = loModel.OrderContactPerson.Phone;
                    this.OrderContactPerson.Email = loModel.OrderContactPerson.Email;
                    this.OrderContactAddress.DeliveryAddress = loModel.OrderContactAddress.DeliveryAddress;
                    this.OrderContactAddress.City = loModel.OrderContactAddress.City;
                    this.OrderContactAddress.StateCode = loModel.OrderContactAddress.StateCode;
                    this.OrderContactAddress.PostalCode = loModel.OrderContactAddress.PostalCode;

                    this.OrderShippingInfo.ShippingAddress.Company = loModel.OrderShippingInfo.ShippingAddress.Company;
                    this.OrderShippingInfo.ShippingAddress.Attention = loModel.OrderShippingInfo.ShippingAddress.Attention;
                    this.OrderShippingInfo.ShippingAddress.DeliveryAddress = loModel.OrderShippingInfo.ShippingAddress.DeliveryAddress;
                    this.OrderShippingInfo.ShippingAddress.City = loModel.OrderShippingInfo.ShippingAddress.City;
                    this.OrderShippingInfo.ShippingAddress.StateCode = loModel.OrderShippingInfo.ShippingAddress.StateCode;
                    this.OrderShippingInfo.ShippingAddress.PostalCode = loModel.OrderShippingInfo.ShippingAddress.PostalCode;
                    this.OrderShippingInfo.ShippingAddress.Notes = loModel.OrderShippingInfo.ShippingAddress.Notes;
                    this.OrderShippingInfo.Notes = loModel.OrderShippingInfo.Notes;

                    /*
                    foreach (MaxOrderPaymentViewModel loPayment in loModel.OrderPaymentList)
                    {
                        foreach (MaxOrderPaymentViewModel loPaymentCurrent in this.OrderPaymentList)
                        {
                            if (loPayment.PaymentDetailType == loPaymentCurrent.PaymentDetailType)
                            {
                                loPaymentCurrent.OrderPaymentDetail.CardExpireMonth = loPayment.OrderPaymentDetail.CardExpireMonth;
                                loPaymentCurrent.OrderPaymentDetail.CardExpireYear = loPayment.OrderPaymentDetail.CardExpireYear;
                                loPaymentCurrent.OrderPaymentDetail.CardName = loPayment.OrderPaymentDetail.CardName;
                                loPaymentCurrent.OrderPaymentDetail.CardNumber = loPayment.OrderPaymentDetail.CardNumber;
                                loPaymentCurrent.OrderPaymentDetail.CardNumberHidden = loPayment.OrderPaymentDetail.CardNumberHidden;
                                loPaymentCurrent.OrderPaymentDetail.CardVerification = loPayment.OrderPaymentDetail.CardVerification;
                                loPaymentCurrent.OrderPaymentDetail.CardVerificationHidden = loPayment.OrderPaymentDetail.CardVerificationHidden;
                                loPaymentCurrent.OrderPaymentDetail.Code = loPayment.OrderPaymentDetail.Code;
                                loPaymentCurrent.OrderPaymentDetail.Note = loPayment.OrderPaymentDetail.Note;
                            }
                        }
                    }
                    */
                }
                else if (loPerson.SourceType == "MaxQBCustomer")
                {
                    lbR = true;
                    this.OrderContactPerson.CurrentFirstName = loPerson.CurrentFirstName;
                    this.OrderContactPerson.CurrentLastName = loPerson.CurrentLastName;
                    this.OrderContactPerson.Phone = loPerson.MainPhone;
                    this.OrderContactPerson.Email = loPerson.MainEmail;

                    this.OrderContactAddress.DeliveryAddress = loPerson.BillAddress.DeliveryAddress;
                    this.OrderContactAddress.City = loPerson.BillAddress.City;
                    this.OrderContactAddress.StateCode = loPerson.BillAddress.StateCode;
                    this.OrderContactAddress.PostalCode = loPerson.BillAddress.PostalCode;

                    this.OrderShippingInfo.ShippingAddress.Company = loPerson.ShipAddress.Company;
                    this.OrderShippingInfo.ShippingAddress.Attention = loPerson.ShipAddress.Attention;
                    this.OrderShippingInfo.ShippingAddress.DeliveryAddress = loPerson.ShipAddress.DeliveryAddress;
                    this.OrderShippingInfo.ShippingAddress.City = loPerson.ShipAddress.City;
                    this.OrderShippingInfo.ShippingAddress.StateCode = loPerson.ShipAddress.StateCode;
                    this.OrderShippingInfo.ShippingAddress.PostalCode = loPerson.ShipAddress.PostalCode;
                }

                foreach (MaxOrderPaymentViewModel loPaymentCurrent in this.OrderPaymentList)
                {
                    loPaymentCurrent.OrderPaymentDetail.CardExpireMonth = string.Empty;
                    loPaymentCurrent.OrderPaymentDetail.CardExpireYear = string.Empty;
                    loPaymentCurrent.OrderPaymentDetail.CardName = string.Empty;
                    loPaymentCurrent.OrderPaymentDetail.CardNumber = string.Empty;
                    loPaymentCurrent.OrderPaymentDetail.CardNumberHidden = string.Empty;
                    loPaymentCurrent.OrderPaymentDetail.CardVerification = string.Empty;
                    loPaymentCurrent.OrderPaymentDetail.CardVerificationHidden = string.Empty;
                    loPaymentCurrent.OrderPaymentDetail.Code = string.Empty;
                    loPaymentCurrent.OrderPaymentDetail.Note = string.Empty;
                }
            }

            return lbR;
        }

        /// <summary>
        /// Loads the entity based on the Id property.
        /// Maps the current values of properties in the ViewModel to the Entity.
        /// </summary>
        /// <returns>True if successful. False if it cannot be mapped.</returns>
        protected override bool MapToEntity()
        {
            if (base.MapToEntity())
            {
                MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
                if (null != loEntity)
                {
                    loEntity.ProcessingStatus = this.ProcessingStatus;
                    loEntity.ExternalOrderId = this.ExternalOrderId;
                    loEntity.ExternalOrderType = this.ExternalOrderType;
                    if (null != this._oCartList)
                    {
                        string lsCartIdListText = string.Empty;
                        List<string> loCouponList = new List<string>();
                        double lnManualDiscount = 0;
                        string lsManualDiscountExplanation = string.Empty;
                        foreach (MaxCartViewModel loCartView in this._oCartList)
                        {
                            if (lsCartIdListText.Length > 0)
                            {
                                lsCartIdListText += "|";
                            }

                            lsCartIdListText += loCartView.Id;
                            if (null != loCartView.CouponCodeList)
                            {
                                loCouponList.AddRange(loCartView.CouponCodeList);
                            }

                            lnManualDiscount += MaxConvertLibrary.ConvertToDouble(typeof(object), loCartView.ManualDiscount);
                            if (lsManualDiscountExplanation.Length > 0)
                            {
                                lsManualDiscountExplanation += "\r\n";
                            }

                            lsManualDiscountExplanation += loCartView.ManualDiscountExplanation;
                            loEntity.ShippingType = loCartView.ShippingType;
                        }

                        loEntity.CouponCodeList = loCouponList.ToArray();
                        loEntity.ManualDiscount = lnManualDiscount;
                        loEntity.ManualDiscountExplanation = lsManualDiscountExplanation;
                        loEntity.CartIdListText = lsCartIdListText;


                        //// Update Order items based on cart list
                        List<MaxProductSelectionEntity> loList = new List<MaxProductSelectionEntity>();
                        //// Any order items that don't match a cart item won't get added to the new list.
                        foreach (MaxOrderItemEntity loOrderItemEntity in loEntity.ItemList)
                        {
                            bool lbFound = false;
                            foreach (MaxCartViewModel loCartView in this._oCartList)
                            {
                                foreach (MaxCartItemViewModel loCartItemView in loCartView.ItemList)
                                {
                                    if (loCartItemView.Id == loOrderItemEntity.SecondaryReferenceId.ToString())
                                    {
                                        lbFound = true;
                                        loList.Add(loOrderItemEntity);
                                        if (loCartItemView.Quantity != loOrderItemEntity.Quantity ||
                                            loCartItemView.DiscountAmount != loOrderItemEntity.DiscountAmount ||
                                            loCartItemView.DiscountReason != loOrderItemEntity.DiscountReason ||
                                            loCartItemView.ManualDiscountAmount != loOrderItemEntity.ManualDiscountAmount ||
                                            loCartItemView.ManualDiscountReason != loOrderItemEntity.ManualDiscountReason)
                                        {
                                            loOrderItemEntity.Quantity = loCartItemView.Quantity;
                                            loOrderItemEntity.DiscountAmount = loCartItemView.DiscountAmount;
                                            loOrderItemEntity.DiscountReason = loCartItemView.DiscountReason;
                                            loOrderItemEntity.ItemTotal = loCartItemView.ItemTotal;
                                            loOrderItemEntity.ManualDiscountAmount = loCartItemView.ManualDiscountAmount;
                                            loOrderItemEntity.ManualDiscountReason = loCartItemView.ManualDiscountReason;
                                            loOrderItemEntity.Update();
                                        }
                                    }
                                }
                            }

                            if (!lbFound)
                            {
                                loOrderItemEntity.Delete();
                            }
                        }

                        //// Add any new cart items to the list of order items.
                        foreach (MaxCartViewModel loCartView in this._oCartList)
                        {
                            foreach (MaxCartItemViewModel loCartItemView in loCartView.ItemList)
                            {
                                bool lbFound = false;
                                foreach (MaxOrderItemEntity loOrderItem in loList)
                                {
                                    if (loCartItemView.Id == loOrderItem.SecondaryReferenceId.ToString())
                                    {
                                        lbFound = true;
                                    }
                                }

                                if (!lbFound)
                                {
                                    MaxCartItemEntity loCartItem = loCartItemView.GetEntity();
                                    MaxOrderItemEntity loOrderItemEntity = MaxOrderItemEntity.Create();
                                    loOrderItemEntity.MapCartItem(loCartItem);
                                    loOrderItemEntity.ContainerId = loEntity.Id;
                                    loList.Add(loOrderItemEntity);
                                }
                            }
                        }

                        loEntity.ItemList = loList;
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Maps the properties of the Entity to the properties of the ViewModel.
        /// </summary>
        /// <returns>True if the entity exists.</returns>
        protected override bool MapFromEntity()
        {
            if (base.MapFromEntity())
            {
                MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
                if (null != loEntity)
                {
                    //// Same as cart start
                    double lnItemTotal = loEntity.ItemTotal;
                    if (lnItemTotal < 0)
                    {
                        lnItemTotal = 0;
                    }
                    this.ItemTotal = string.Format("{0:C}", lnItemTotal);
                    if (loEntity.DiscountTotal > 0)
                    {
                        this.DiscountTotal = string.Format("{0:C}", loEntity.DiscountTotal);
                        this.SubTotal = string.Format("{0:C}", loEntity.ItemTotal + loEntity.ShippingTotal);
                        this.DiscountTotalExplanation = loEntity.DiscountTotalExplanation;
                    }

                    double lnTotal = loEntity.Total;
                    if (lnTotal < 0)
                    {
                        lnTotal = 0;
                    }

                    this.Total = lnTotal;
                    this.ShippingType = loEntity.ShippingType;

                    this.ShippingTotal = loEntity.ShippingTotal;

                    if (loEntity.ShippingType <= 0)
                    {
                        this.ShippingTotal = double.MinValue;
                    }

                    if (loEntity.ManualDiscount > 0)
                    {
                        this.ManualDiscount = loEntity.ManualDiscount.ToString();
                        this.ManualDiscountExplanation = loEntity.ManualDiscountExplanation;
                    }

                    this.TaxTotal = loEntity.TaxTotal;

                    //// same as cart end

                    this.ItemCount = loEntity.ItemCount.ToString();
                    this.ProcessingStatus = loEntity.ProcessingStatus;
                    this.OrderPlacedDate = String.Format("{0:d} {0:t}", MaxConvertLibrary.ConvertToDateTimeFromUtc(typeof(object), loEntity.OrderPlacedDate));
                    this.UserId = loEntity.UserId;
                    this.UserName = loEntity.Username;
                    this.ProcessingStatusText = this.GetStatusText(loEntity.ProcessingStatus);
                    this.ExternalOrderId = loEntity.ExternalOrderId;
                    this.ExternalOrderType = loEntity.ExternalOrderType;
                    this.TaxableAmount = loEntity.TaxableAmount;

                    return true;
                }
            }

            return false;
        }

        public override bool Save()
        {
            bool lbR = false;
            MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
            if (null != loEntity)
            {
                //// Load an existing order if one is specified in the Id
                this.EntityLoad(this.Id);
                if (this.MapToEntity())
                {
                    loEntity.Recalculate();
                    if (loEntity.Id.Equals(Guid.Empty))
                    {
                        lbR = loEntity.Insert();
                    }
                    else
                    {
                        lbR = loEntity.Update();
                    }

                    if (lbR)
                    {
                        lbR = this.Load();
                    }
                }
            }

            if (lbR)
            {
                if (null != this._oOrderContactAddress)
                {
                    this._oOrderContactAddress.OrderId = this.Id;
                }

                if (null != this._oOrderContactPerson)
                {
                    this._oOrderContactPerson.OrderId = this.Id;
                }

                if (null != this._oOrderShippingInfo)
                {
                    this._oOrderShippingInfo.OrderId = this.Id;
                }

                if (null != this._oOrderPaymentList)
                {
                    foreach (MaxOrderPaymentViewModel loPayment in this._oOrderPaymentList)
                    {
                        loPayment.OrderId = this.Id;
                    }
                }
            }

            return lbR;
        }

        public void AddCart(MaxCartViewModel loCartViewModel, bool lbOnlyCart)
        {
            if (null == this._oCartList || lbOnlyCart)
            {
                this._oCartList = new List<MaxCartViewModel>();
            }

            this._oCartList.Add(loCartViewModel);
        }

        public void SetCurrent(bool lbIncludeCart)
        {
            MaxOrderEntity loEntity = this.Entity as MaxOrderEntity;
            if (loEntity.IsAvailable)
            {
                MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeProcess, _sEntityKey, loEntity);
                if (Guid.Empty != loEntity.Id)
                {
                    MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeProfile, _sIdKey, loEntity.Id);
                }

                if (lbIncludeCart && !string.IsNullOrEmpty(loEntity.CartIdListText))
                {
                    string[] laCartIdListText = loEntity.CartIdListText.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    if (laCartIdListText.Length == 1)
                    {
                        MaxCartViewModel loCart = new MaxCartViewModel(laCartIdListText[0]);
                        loCart.SetCurrent();
                    }
                }
            }
        }

        public bool GetCurrent()
        {
            if (null != this.Id && this.Id.Equals("new", StringComparison.InvariantCultureIgnoreCase))
            {
                MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeProfile, _sIdKey, null);
                MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeProcess, _sEntityKey, null);
                this.Id = null;
                return false;
            }

            object loObject = MaxConfigurationLibrary.GetValue(MaxEnumGroup.ScopeProcess, _sEntityKey);
            if (null != loObject && loObject is MaxOrderEntity)
            {
                if (((MaxOrderEntity)loObject).IsAvailable)
                {
                    this.Entity = (MaxOrderEntity)loObject;
                    return true;
                }
            }

            // Look up the current id in the profile.
            loObject = MaxConfigurationLibrary.GetValue(MaxEnumGroup.ScopeProfile, _sIdKey);
            if (null != loObject)
            {
                Guid loId = MaxConvertLibrary.ConvertToGuid(typeof(object), loObject);
                if (!Guid.Empty.Equals(loId))
                {
                    MaxOrderEntity loEntity = MaxOrderEntity.Create();
                    if (loEntity.LoadByIdCache(loId))
                    {
                        if (loEntity.IsAvailable)
                        {
                            MaxConfigurationLibrary.SetValue(MaxEnumGroup.ScopeProcess, _sEntityKey, loEntity);
                            this.Entity = loEntity;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void SubscribeAll()
        {
            MaxEntityList loList = MaxOrderEntity.Create().LoadAllCache();
            for (int lnE = 0; lnE < loList.Count; lnE++)
            {
                MaxOrderEntity loOrder = loList[lnE] as MaxOrderEntity;
                if (loOrder.ProcessingStatus >= MaxOrderEntity.ProcessingStatusPending)
                {
                    if (loOrder.OrderContactPerson.EmailSignup)
                    {
                        loOrder.OrderContactPerson.Subscribe("Mailing List");
                    }
                }
            }
        }
    }
}
