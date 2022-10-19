// <copyright file="MaxOrderEntity.cs" company="Lakstins Family, LLC">
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
// <change date="7/13/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="12/18/2014" author="Brian A. Lakstins" description="Updates to follow core data access patterns.">
// <change date="6/11/2015" author="Brian A. Lakstins" description="Integrating manual discount with order.">
// <change date="9/17/2015" author="Brian A. Lakstins" description="Replace notification content with notification id to help speed up order queries">
// <change date="2/5/2016" author="Brian A. Lakstins" description="Fix sales tax calculation.">
// <change date="12/21/2016" author="Brian A. Lakstins" description="Updated to use centralized StorageKey handling.">
// <change date="6/7/2017" author="Brian A. Lakstins" description="Added sales tax override.">
// <change date="10/24/2017" author="Brian A. Lakstins" description="Update to match ViewModel mapping of CartIdList.">
// <change date="11/8/2017" author="Brian A. Lakstins" description="Update to load orders based on status instead of loading them all.">
// <change date="11/8/2017" author="Brian A. Lakstins" description="Fix caching of orders by status.">
// <change date="3/7/2018" author="Brian A. Lakstins" description="Update mapping of cart to ordered item.">
// <change date="11/30/2018" author="Brian A. Lakstins" description="Updated for changes to core.">
// <change date="11/30/2018" author="Brian A. Lakstins" description="Remove unused methods for updating cart.">
// <change date="11/30/2018" author="Brian A. Lakstins" description="Update to make all payment methods available regardless of access roles.">
// <change date="11/30/2018" author="Brian A. Lakstins" description="Fix loading already created payments.">
// <change date="12/12/2019" author="Brian A. Lakstins" description="Remove secondary loading of payments.  Remove unnecessary checking of current Id.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.General.BusinessLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxOrderEntity : MaxCartEntity
    {
        private List<Guid> _oCartIdList = null;

        private List<MaxOrderProcessLogEntity> _oLogList = null;

        private MaxOrderContactPersonEntity _oOrderContactPerson = null;

        private MaxOrderContactAddressEntity _oOrderContactAddress = null;

        private MaxOrderShippingInfoEntity _oOrderShippingInfo = null;

        private List<MaxOrderPaymentEntity> _oOrderPaymentList = null;

        private const string _sIdKey = "MaxCatalogOrderIdCurrent";

        private const string _sEntityKey = "MaxCatalogOrderEntityCurrent";

        private const string _sReferenceUserKey = "MaxCurrentOrderUser";

        private const string _sReferenceProfileKey = "MaxCurrentOrderProfile";

        public const int ProcessingStatusDeleted = -2;

        public const int ProcessingStatusCancelled = -1;

        public const int ProcessingStatusStarted = 1;

        public const int ProcessingStatusPending = 500;

        public const int ProcessingStatusConfirmed = 1000;

        public const int ProcessingStatus2000 = 2000;

        public const int ProcessingStatus3000 = 3000;

        public const int ProcessingStatus4000 = 4000;

        public const int ProcessingStatus5000 = 5000;

        public const int ProcessingStatus6000 = 6000;

        public const int ProcessingStatus7000 = 7000;

        public const int ProcessingStatus8000 = 8000;

        public const int ProcessingStatus9000 = 9000;

        public const int ProcessingStatusArchived = 9999;

        /// <summary>
        /// Initializes a new instance of the MaxOrderEntity class
        /// </summary>
        /// <param name="loData">object to hold data</param>
        public MaxOrderEntity(MaxData loData) : base(loData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxOrderEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public int ProcessingStatus
        {
            get
            {
                return this.GetInt(this.DataModel.ProcessingStatus);
            }

            set
            {
                this.Set(this.DataModel.ProcessingStatus, value);
            }
        }

        public string ProcessingUserId
        {
            get
            {
                return this.GetString(this.DataModel.ProcessingUserId);
            }

            set
            {
                this.Set(this.DataModel.ProcessingUserId, value);
            }
        }

        public Guid ConfirmationNoticeId
        {
            get
            {
                return this.GetGuid(this.DataModel.ConfirmationNoticeId);
            }

            set
            {
                this.Set(this.DataModel.ConfirmationNoticeId, value);
            }
        }

        public string ConfirmationNoticeContent
        {
            get
            {
                string lsR = this.GetString(this.DataModel.ConfirmationNoticeContent);
                return lsR;
            }

            set
            {
                this.Set(this.DataModel.ConfirmationNoticeContent, value);
            }
        }

        public string ManagementNoticeContent
        {
            get
            {
                string lsR = this.GetString(this.DataModel.ManagementNoticeContent);
                return lsR;
            }

            set
            {
                this.Set(this.DataModel.ManagementNoticeContent, value);
            }
        }

        public Guid ManagementNoticeId
        {
            get
            {
                return this.GetGuid(this.DataModel.ManagementNoticeId);
            }

            set
            {
                this.Set(this.DataModel.ManagementNoticeId, value);
            }
        }

        public DateTime OrderPlacedDate
        {
            get
            {
                return this.GetDateTime(this.DataModel.OrderPlacedDate);
            }

            set
            {
                this.Set(this.DataModel.OrderPlacedDate, value);
            }
        }

        public string ExternalOrderId
        {
            get
            {
                return this.GetString(this.DataModel.ExternalOrderId);
            }

            set
            {
                this.Set(this.DataModel.ExternalOrderId, value);
            }
        }

        public string ExternalOrderType
        {
            get
            {
                return this.GetString(this.DataModel.ExternalOrderType);
            }

            set
            {
                this.Set(this.DataModel.ExternalOrderType, value);
            }
        }

        public MaxOrderContactPersonEntity OrderContactPerson
        {
            get
            {
                if (null == this._oOrderContactPerson)
                {
                    this._oOrderContactPerson = MaxOrderContactPersonEntity.Create();
                    this._oOrderContactPerson.EmailSignup = true;
                    if (!Guid.Empty.Equals(this.Id))
                    {
                        MaxEntityList loList = this._oOrderContactPerson.LoadAllByOrderId(this.Id);
                        if (loList.Count == 0)
                        {
                            this._oOrderContactPerson.StorageKey = this.StorageKey;
                            loList = this._oOrderContactPerson.LoadAllByOrderId(this.Id);
                        }

                        if (loList.Count > 0)
                        {
                            this._oOrderContactPerson = loList[0] as MaxOrderContactPersonEntity;
                        }
                    }
                }

                return this._oOrderContactPerson;
            }

            set
            {
                this._oOrderContactPerson = value;
            }
        }

        public MaxOrderContactAddressEntity OrderContactAddress
        {
            get
            {
                if (null == this._oOrderContactAddress)
                {
                    this._oOrderContactAddress = MaxOrderContactAddressEntity.Create();
                    if (!Guid.Empty.Equals(this.Id))
                    {
                        MaxEntityList loList = this._oOrderContactAddress.LoadAllByOrderId(this.Id);
                        if (loList.Count == 0)
                        {
                            this._oOrderContactAddress.StorageKey = this.StorageKey;
                            loList = this._oOrderContactAddress.LoadAllByOrderId(this.Id);
                        }

                        if (loList.Count > 0)
                        {
                            this._oOrderContactAddress = loList[0] as MaxOrderContactAddressEntity;
                        }
                    }
                }

                return this._oOrderContactAddress;
            }

            set
            {
                this._oOrderContactAddress = value;
            }
        }

        public MaxOrderShippingInfoEntity OrderShippingInfo
        {
            get
            {
                if (null == this._oOrderShippingInfo)
                {
                    this._oOrderShippingInfo = MaxOrderShippingInfoEntity.Create();
                    this._oOrderShippingInfo.OrderId = this.Id;
                    if (!Guid.Empty.Equals(this.Id))
                    {
                        MaxEntityList loList = this._oOrderShippingInfo.LoadAllByOrderId(this.Id);
                        if (loList.Count == 0)
                        {
                            this._oOrderShippingInfo.StorageKey = this.StorageKey;
                            loList = this._oOrderShippingInfo.LoadAllByOrderId(this.Id);
                        }

                        if (loList.Count > 0)
                        {
                            this._oOrderShippingInfo = loList[0] as MaxOrderShippingInfoEntity;
                        }
                    }
                }

                return this._oOrderShippingInfo;
            }

            set
            {
                this._oOrderShippingInfo = value;
            }
        }

        public List<MaxOrderPaymentEntity> OrderPaymentList
        {
            get
            {
                if (null == this._oOrderPaymentList)
                {
                    this._oOrderPaymentList = new List<MaxOrderPaymentEntity>();
                    if (Guid.Empty != this.Id)
                    {
                        MaxOrderPaymentEntity loEntity = MaxOrderPaymentEntity.Create();
                        MaxEntityList loList = loEntity.LoadAllByOrderId(this.Id);
                        for (int lnE = 0; lnE < loList.Count; lnE++)
                        {
                            loEntity = loList[lnE] as MaxOrderPaymentEntity;
                            this._oOrderPaymentList.Add(loEntity);
                        }

                        if (this.ProcessingStatus < ProcessingStatusPending)
                        {
                            if (this._oOrderPaymentList.Count.Equals(0))
                            {
                                MaxOrderPaymentEntity loOrderPaymentCard = MaxOrderPaymentEntity.Create();
                                loOrderPaymentCard.PaymentDetailType = MaxOrderPaymentDetailEntity.PaymentDetailTypeCard;
                                loOrderPaymentCard.OrderId = this.Id;
                                loOrderPaymentCard.Insert();
                                this._oOrderPaymentList.Add(loOrderPaymentCard);

                                MaxOrderPaymentEntity loOrderPaymentGeneral = MaxOrderPaymentEntity.Create();
                                loOrderPaymentGeneral.PaymentDetailType = MaxOrderPaymentDetailEntity.PaymentDetailTypeGeneral;
                                loOrderPaymentGeneral.OrderId = this.Id;
                                loOrderPaymentGeneral.Insert();
                                this._oOrderPaymentList.Add(loOrderPaymentGeneral);
                            }
                        }
                    }
                }

                return this._oOrderPaymentList;
            }

            set
            {
                this._oOrderPaymentList = value;
            }
        }

        public string CartIdListText
        {
            get
            {
                return this.GetString(this.DataModel.CartIdListText);
            }

            set
            {
                this.Set(this.DataModel.CartIdListText, value);
            }
        }

        public override double GetSalesTaxRate()
        {
            foreach (Guid loCartId in this.CartIdList)
            {
                MaxCartEntity loCart = MaxCartEntity.Create();
                loCart.LoadByIdCache(loCartId);
                if (!string.IsNullOrEmpty(loCart.TaxLocation))
                {
                    this.TaxLocation = loCart.TaxLocation;
                }
            }

            double lnR = base.GetSalesTaxRate();
            if (this.OrderContactAddress.StateCode == "IN")
            {
                lnR = SalesTaxRateIN;
            }
            else if (!string.IsNullOrEmpty(this.OrderContactAddress.StateCode))
            {
                lnR = 0;
            }

            foreach (Guid loCartId in this.CartIdList)
            {
                MaxCartEntity loCart = MaxCartEntity.Create();
                loCart.LoadByIdCache(loCartId);
                if (loCart.SalesTaxRate != lnR)
                {
                    loCart.SalesTaxRate = lnR;
                    loCart.Update();
                }
            }

            foreach (Guid loCartId in this.CartIdList)
            {
                MaxCartEntity loCart = MaxCartEntity.Create();
                loCart.LoadByIdCache(loCartId);
                if (loCart.TaxOverride == 2)
                {
                    return 0;
                }
                else if (loCart.TaxOverride == 1)
                {
                    return SalesTaxRateIN;
                }
            }

            return lnR;
        }

        public void SendConfirmationEmail(string lsFromAddress, string lsFromName, string lsSubject, string lsContent)
        {
            if (!string.IsNullOrEmpty(this.OrderContactPerson.Email))
            {
                string lsFromAddressConfig = MaxConvertLibrary.ConvertToString(typeof(object), MaxConfigurationLibrary.GetValue(MaxEnumGroup.Scope24, "MaxOrder.ConfirmationEmailFromAddress"));
                string lsFromNameConfig = MaxConvertLibrary.ConvertToString(typeof(object), MaxConfigurationLibrary.GetValue(MaxEnumGroup.Scope24, "MaxOrder.ConfirmationEmailFromName"));

                MaxOrderConfirmEmailEntity loEmail = MaxOrderConfirmEmailEntity.Create();
                loEmail.RelationId = this.Id;
                loEmail.RelationType = "MaxOrder";
                loEmail.ToAddressList.Add(this.OrderContactPerson.Email);
                loEmail.ToNameList.Add(this.OrderContactPerson.CurrentFirstName + " " + this.OrderContactPerson.CurrentLastName);

                loEmail.FromAddress = "orders@dns9.co";
                if (!string.IsNullOrEmpty(lsFromAddressConfig))
                {
                    loEmail.FromAddress = lsFromAddressConfig;
                }

                if (!string.IsNullOrEmpty(lsFromAddress))
                {
                    loEmail.FromAddress = lsFromAddress;
                }

                loEmail.FromName = "Order Process";
                if (!string.IsNullOrEmpty(lsFromNameConfig))
                {
                    loEmail.FromName = lsFromNameConfig;
                }

                if (!string.IsNullOrEmpty(lsFromName))
                {
                    loEmail.FromName = lsFromName;
                }

                loEmail.Subject = "Order Confirmation";
                if (!string.IsNullOrEmpty(lsSubject))
                {
                    loEmail.Subject = lsSubject;
                }

                loEmail.Content = lsContent;
                loEmail.Send();
                if (this.ConfirmationNoticeId.Equals(Guid.Empty))
                {
                    this.ConfirmationNoticeId = loEmail.Id;
                    this.Update();
                }
            }
        }

        public void SendNotificationEmail(string lsFromAddress, string lsFromName, string lsSubject, string lsContent, string lsToAddress, string lsToName)
        {
            string lsToAddressConfig = MaxConvertLibrary.ConvertToString(typeof(object), MaxConfigurationLibrary.GetValue(MaxEnumGroup.Scope24, "MaxOrder.NotificationEmailToAddress"));
            string lsFromAddressConfig = MaxConvertLibrary.ConvertToString(typeof(object), MaxConfigurationLibrary.GetValue(MaxEnumGroup.Scope24, "MaxOrder.NotificationEmailFromAddress"));
            string lsFromNameConfig = MaxConvertLibrary.ConvertToString(typeof(object), MaxConfigurationLibrary.GetValue(MaxEnumGroup.Scope24, "MaxOrder.NotificationEmailFromName"));

            MaxOrderNotificationEmailEntity loEmail = MaxOrderNotificationEmailEntity.Create();
            loEmail.RelationId = this.Id;
            loEmail.RelationType = "MaxOrder";
            loEmail.ToAddressList.Add("brianlakstins@gmail.com");

            if (!string.IsNullOrEmpty(lsToAddressConfig))
            {
                loEmail.ToAddressListText = loEmail.ToAddressList + ";" + lsToAddressConfig;
            }

            if (!string.IsNullOrEmpty(lsToAddress))
            {
                loEmail.ToAddressListText = loEmail.ToAddressList + ";" + lsToAddress;
            }

            loEmail.ToNameListText = "Order Manager";
            if (!string.IsNullOrEmpty(lsToName))
            {
                loEmail.ToNameListText = lsToName;
            }

            loEmail.FromAddress = "orders@dns9.co";
            if (!string.IsNullOrEmpty(lsFromAddressConfig))
            {
                loEmail.FromAddress = lsFromAddressConfig;
            }

            if (!string.IsNullOrEmpty(lsFromAddress))
            {
                loEmail.FromAddress = lsFromAddress;
            }

            loEmail.FromName = "Order Process";
            if (!string.IsNullOrEmpty(lsFromNameConfig))
            {
                loEmail.FromName = lsFromNameConfig;
            }

            if (!string.IsNullOrEmpty(lsFromName))
            {
                loEmail.FromName = lsFromName;
            }

            loEmail.Subject = "New Order Received " + this.AlternateId;
            if (!string.IsNullOrEmpty(lsSubject))
            {
                loEmail.Subject = lsSubject;
            }

            loEmail.Content = lsContent;
            loEmail.Send();
            if (this.ManagementNoticeId.Equals(Guid.Empty))
            {
                this.ManagementNoticeId = loEmail.Id;
                this.Update();
            }
        }

        protected List<Guid> CartIdList
        {
            get
            {
                if (null == this._oCartIdList)
                {
                    string lsIdList = this.GetString(this.DataModel.CartIdListText);
                    this._oCartIdList = new List<Guid>();
                    string[] laIdList = lsIdList.Split('|');
                    foreach (string lsId in laIdList)
                    {
                        if (!string.IsNullOrEmpty(lsId))
                        {
                            Guid loId = Guid.Empty;
                            if (Guid.TryParse(lsId, out loId))
                            {
                                this._oCartIdList.Add(loId);
                            }
                        }
                    }
                }

                return this._oCartIdList;
            }
        }

        public void LogMessage(string lsMessage, string lsUserName)
        {
            this.LogMessage(lsMessage, lsUserName, false);
        }

        public void LogMessage(string lsMessage, string lsUserName, bool lbIsCustomerVisible)
        {
            MaxOrderProcessLogEntity loLog = MaxOrderProcessLogEntity.Create();
            loLog.OrderId = this.Id;
            loLog.LogEntry = lsMessage;
            loLog.IsCustomerVisible = lbIsCustomerVisible;
            if (!string.IsNullOrEmpty(lsUserName))
            {
                loLog.LogEntry += " [" + lsUserName + "]";
            }

            loLog.Insert();
        }

        public List<MaxOrderProcessLogEntity> LogList
        {
            get
            {
                if (null == this._oLogList)
                {
                    if (Guid.Empty.Equals(this.Id))
                    {
                        return new List<MaxOrderProcessLogEntity>();
                    }

                    SortedList<string, MaxOrderProcessLogEntity> loSortedList = new SortedList<string, MaxOrderProcessLogEntity>();
                    MaxEntityList loList = MaxOrderProcessLogEntity.Create().LoadAllByOrderId(this.Id);
                    for (int lnE = 0; lnE < loList.Count; lnE++)
                    {
                        MaxOrderProcessLogEntity loEntity = (MaxOrderProcessLogEntity)loList[lnE];
                        loSortedList.Add(loEntity.GetDefaultSortString(), loEntity);
                    }

                    this._oLogList = new List<MaxOrderProcessLogEntity>(loSortedList.Values);
                }

                return this._oLogList;
            }

            set
            {
                this._oLogList = value;
            }
        }

        public virtual Dictionary<int, string> ProcessingStatusIndex
        {
            get
            {
                Dictionary<int, string> loR = new Dictionary<int, string>();
                loR.Add(ProcessingStatusStarted, "Started");
                loR.Add(ProcessingStatusPending, "Pending");
                loR.Add(ProcessingStatusConfirmed, "Confirmed By Customer");
                loR.Add(2000, "Order info reviewed");
                loR.Add(3000, "Item(s) in stock");
                //loR.Add(4000, "Process Payment");
                loR.Add(5000, "Shipping secured");
                loR.Add(6000, "Order shipped");
                loR.Add(7000, "Order entered in QB");
                loR.Add(8000, "Customer Added to Database");
                loR.Add(ProcessingStatusArchived, "Archive");
                loR.Add(ProcessingStatusCancelled, "Cancelled");

                return loR;
            }
        }

        public virtual Dictionary<short, string> ProcessingActionIndex
        {
            get
            {
                //// The text can change, but don't change the numbers or indication for the numbers
                Dictionary<short, string> loR = new Dictionary<short, string>();
                loR.Add(1, "Order info reviewed");
                loR.Add(2, "Item(s) in stock");
                loR.Add(3, "Shipping secured");
                loR.Add(4, "Order shipped");
                loR.Add(5, "Order entered in QB");
                loR.Add(6, "Customer Added to Database");

                return loR;
            }
        }

        public void SetProcessingAction(short lnFlag, bool lbValue)
        {
            this.SetBit(DataModel.ProcessingActionFlagList, lnFlag, lbValue);
            if (lnFlag == 4)
            {
                //// Shipping update
                if (lbValue)
                {
                    if (MaxInventoryProductEntity.UpdateInventoryAmount(this, MaxInventoryProductEntity.OnShip))
                    {
                        this.LogMessage("Updated product inventory when shipped.", null);
                    }
                }
                else
                {
                    if (MaxInventoryProductEntity.UpdateInventoryAmount(this, MaxInventoryProductEntity.OnUnship))
                    {
                        this.LogMessage("Updated product inventory when unshipped.", null);
                    }
                }
            }
        }

        public bool GetProcessingAction(short lnFlag)
        {
            return this.GetBit(DataModel.ProcessingActionFlagList, lnFlag);
        }

        public string GetStatusText()
        {
            string lsR = "Unknown";
            if (ProcessingStatusConfirmed <= this.ProcessingStatus && this.ProcessingStatus < ProcessingStatusArchived)
            {
                lsR = string.Empty;
                foreach (short lnKey in this.ProcessingActionIndex.Keys)
                {
                    if (!this.GetProcessingAction(lnKey))
                    {
                        bool lbAdd = true;
                        if (lnKey == 1 && this.ProcessingStatus >= ProcessingStatus2000)
                        {
                            lbAdd = false;
                        }
                        else if (lnKey == 2 && this.ProcessingStatus >= ProcessingStatus3000)
                        {
                            lbAdd = false;
                        }
                        else if (lnKey == 3 && this.ProcessingStatus >= ProcessingStatus5000)
                        {
                            lbAdd = false;
                        }
                        else if (lnKey == 4 && this.ProcessingStatus >= ProcessingStatus6000)
                        {
                            lbAdd = false;
                        }
                        else if (lnKey == 5 && this.ProcessingStatus >= ProcessingStatus7000)
                        {
                            lbAdd = false;
                        }
                        else if (lnKey == 6 && this.ProcessingStatus >= ProcessingStatus8000)
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
            else if (this.ProcessingStatusIndex.ContainsKey(this.ProcessingStatus))
            {
                lsR = this.ProcessingStatusIndex[this.ProcessingStatus];
            }

            return lsR;
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected new MaxOrderDataModel DataModel
        {
            get
            {
                return (MaxOrderDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public new static MaxOrderEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxOrderEntity),
                typeof(MaxOrderDataModel)) as MaxOrderEntity;
        }

        public override bool Load(MaxData loData)
        {
            this._oCartIdList = null;
            this._oOrderContactAddress = null;
            this._oOrderContactPerson = null;
            this._oOrderPaymentList = null;
            this._oOrderShippingInfo = null;
            this._oLogList = null;
            return base.Load(loData);
        }

        public override string GetDefaultSortString()
        {
            if (this.ProcessingStatus >= ProcessingStatusConfirmed || this.ProcessingStatus <= ProcessingStatusCancelled)
            {
                return string.Format("{0:yyyy}{0:MM}{0:dd}{0:HH}{0:mm}{0:ss}{0:fffffff}", this.OrderPlacedDate) + base.GetDefaultSortString();
            }

            return string.Format("{0:yyyy}{0:MM}{0:dd}{0:HH}{0:mm}{0:ss}{0:fffffff}", this.LastUpdateDate) + base.GetDefaultSortString();
        }

        public override bool LoadByIdCache(Guid loId)
        {
            string lsCacheDataKey = this.GetCacheKey() + ".LoadAllLoadAllByProcessingStatus";
            MaxDataList loDataAllList = MaxCacheRepository.Get(this.GetType(), lsCacheDataKey, typeof(MaxDataList)) as MaxDataList;
            if (null != loDataAllList)
            {
                MaxEntityList loEntityList = MaxEntityList.Create(this.GetType(), loDataAllList);
                for (int lnE = 0; lnE < loEntityList.Count; lnE++)
                {
                    MaxOrderEntity loEntity = loEntityList[lnE] as MaxOrderEntity;
                    if (loEntity.Id == loId)
                    {
                        return this.Load(loEntity.GetData());
                    }
                }
            }

            return base.LoadByIdCache(loId);
        }

        public virtual MaxEntityList LoadAllByProcessingStatusCache(int[] laProcessingStatus)
        {
            string lsCacheDataKey = this.GetCacheKey() + ".LoadAllLoadAllByProcessingStatus";
            foreach (int lnP in laProcessingStatus)
            {
                lsCacheDataKey += "_" + lnP.ToString();
            }

            MaxDataList loDataList = MaxCacheRepository.Get(this.GetType(), lsCacheDataKey, typeof(MaxDataList)) as MaxDataList;
            if (null == loDataList)
            {
                loDataList = MaxCatalogRepository.SelectAllOrderByProcessingStatus(this.Data, laProcessingStatus);
                MaxCacheRepository.Set(this.GetType(), lsCacheDataKey, loDataList);
            }

            MaxEntityList loR = MaxEntityList.Create(this.GetType(), loDataList);


            lsCacheDataKey = this.GetCacheKey() + ".LoadAllLoadAllByProcessingStatus";
            MaxDataList loDataAllList = MaxCacheRepository.Get(this.GetType(), lsCacheDataKey, typeof(MaxDataList)) as MaxDataList;
            if (null != loDataAllList)
            {
                MaxEntityList loEntityList = MaxEntityList.Create(this.GetType(), loDataAllList);
                List<Guid> loAlreadyCached = new List<Guid>();
                for (int lnE = 0; lnE < loEntityList.Count; lnE++)
                {
                    MaxOrderEntity loEntity = loEntityList[lnE] as MaxOrderEntity;
                    loAlreadyCached.Add(loEntity.Id);
                }

                for (int lnE = 0; lnE < loR.Count; lnE++)
                {
                    MaxOrderEntity loEntity = loR[lnE] as MaxOrderEntity;
                    if (!loAlreadyCached.Contains(loEntity.Id))
                    {
                        loDataAllList.Add(loEntity.GetData());
                    }
                }
            }
            else
            {
                loDataAllList = new MaxDataList(loDataList.DataModel);
                for (int lnD = 0; lnD < loDataList.Count; lnD++)
                {
                    loDataAllList.Add(loDataList[lnD].Clone());
                }
            }

            MaxCacheRepository.Set(this.GetType(), lsCacheDataKey, loDataAllList);


            return loR;
        }

        public virtual MaxEntityList LoadAllByOrderPlacedDate(DateTime ldStart, DateTime ldEnd)
        {
            MaxDataList loDataList = MaxCatalogRepository.SelectAllOrderByOrderPlacedDateRange(this.Data, ldStart, ldEnd);
            MaxEntityList loEntityList = MaxEntityList.Create(this.GetType(), loDataList);
            return loEntityList;
        }

        public virtual bool LoadByExternal(string lsExternalOrderType, string lsExternalOrderId)
        {
            MaxDataList loDataList = MaxCatalogRepository.SelectAllOrderByExternal(this.Data, lsExternalOrderType, lsExternalOrderId);
            if (loDataList.Count == 1)
            {
                this.Load(loDataList[0]);
                return true;
            }
            else if (loDataList.Count > 1)
            {
                throw new MaxException("More than one order matches external order [" + lsExternalOrderType + "][" + lsExternalOrderId + "]");
            }

            return false;
        }

        public override MaxEntityList LoadAllCache()
        {
            MaxLogLibrary.Log(MaxEnumGroup.LogInfo, "Start MaxOrderEntity.LoadAllCache");
            MaxEntityList loR = base.LoadAllCache();
            MaxLogLibrary.Log(MaxEnumGroup.LogInfo, "End MaxOrderEntity.LoadAllCache");
            return loR;
        }

        public bool ConfirmStart()
        {
            bool lbR = false;
            if (this.IsAvailable)
            {
                lbR = true;
                if (string.IsNullOrEmpty(this.AlternateId))
                {
                    MaxOrderIdEntity loIdEntity = MaxOrderIdEntity.Create();
                    loIdEntity.AlternateId = this.Id.ToString().ToLowerInvariant();
                    loIdEntity.Insert();
                    this.AlternateId = loIdEntity.Id.ToString().ToLowerInvariant();
                    this.Update();
                }
            }

            return lbR;
        }

        public bool ConfirmEnd()
        {
            if (this.IsAvailable)
            {
                this.IsAvailable = false;
                this.Set(this.DataModel.OrderPlacedDate, DateTime.UtcNow);
                double lnPaymentCollected = 0;
                foreach (MaxOrderPaymentEntity loPayment in this.OrderPaymentList)
                {
                    foreach (MaxOrderPaymentTransactionEntity loTransaction in loPayment.TransactionList)
                    {
                        if (loTransaction.IsCollected)
                        {
                            lnPaymentCollected += loTransaction.Amount;
                        }
                    }
                }

                this.ProcessingStatus = ProcessingStatusConfirmed;
                if (MaxClientRepository.IsTractorToolsDirect(this.Data))
                {
                    //// Set status to pending when full payment amount has not yet been collected at checkout.
                    if (string.Format("{0:C}", lnPaymentCollected) != string.Format("{0:C}", this.Total))
                    {
                        if (lnPaymentCollected < this.Total)
                        {
                            this.LogMessage("Payment amount collected for this order is " + string.Format("{0:C}", lnPaymentCollected) + " of " + string.Format("{0:C}", this.Total), this.Username);
                            this.ProcessingStatus = ProcessingStatusPending;
                        }
                    }
                }

                if (this.Update())
                {
                    //// Update Inventory if a payment was made before confirmation
                    if (lnPaymentCollected > 0 || this.Total == 0)
                    {
                        if (MaxInventoryProductEntity.UpdateInventoryAmount(this, MaxInventoryProductEntity.OnOrder))
                        {
                            this.LogMessage("Updated product inventory on order confirmation because payment of " + string.Format("{0:C}", lnPaymentCollected) + " was collected on a total of " + string.Format("{0:C}", this.Total) + ".", this.Username);
                        }
                    }

                    //// Update and dicounts codes used.
                    foreach (string lsDiscountCode in this.CouponCodeList)
                    {
                        Guid loId = MaxConvertLibrary.ConvertToGuid(typeof(object), lsDiscountCode);
                        if (!loId.Equals(Guid.Empty))
                        {
                            MaxDiscountCodeEntity loDiscountCodeEntity = MaxDiscountCodeEntity.Create();
                            if (loDiscountCodeEntity.LoadByIdCache(loId))
                            {
                                loDiscountCodeEntity.UsedCount += 1;
                                loDiscountCodeEntity.Update();
                            }
                        }
                    }

                    this.LogMessage("Order confirmation process was run.", this.Username);
                    if (this.ProcessingStatus == ProcessingStatusPending)
                    {
                        this.LogMessage("Order set to status Pending", this.Username);
                    }

                    foreach (Guid loCartId in this.CartIdList)
                    {
                        MaxCartEntity loCartEntity = MaxCartEntity.Create();
                        if (loCartEntity.LoadByIdCache(loCartId) && loCartEntity.IsAvailable)
                        {
                            loCartEntity.IsAvailable = false;
                            loCartEntity.Update();
                        }
                    }

                    if (this.OrderContactPerson.EmailSignup)
                    {
                        string lsKey = this.StorageKey;
                        object loList = MaxConfigurationLibrary.GetValue(MaxEnumGroup.ScopeApplication, "MaxEmailOrderList" + lsKey);
                        if (null == loList)
                        {
                            loList = MaxConfigurationLibrary.GetValue(MaxEnumGroup.ScopeApplication, "MaxEmailOrderList");
                        }

                        if (null != loList)
                        {
                            string lsList = loList.ToString();
                            if (!string.IsNullOrEmpty(this.OrderContactPerson.Email))
                            {
                                if (this.OrderContactPerson.Subscribe(lsList))
                                {
                                    this.LogMessage("Subscribed " + this.OrderContactPerson.Email + " to mailing list " + lsList, this.Username);
                                }
                            }
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        public override bool Insert()
        {
            string lsIdList = "|";
            foreach (Guid loId in this.CartIdList)
            {
                lsIdList += loId.ToString() + "|";
            }

            this.Set(this.DataModel.CartIdListText, lsIdList);

            bool lbR = base.Insert();

            if (null != this._oOrderContactAddress)
            {
                this._oOrderContactAddress.OrderId = this.Id;
            }
            else
            {
                this._oOrderContactAddress = MaxOrderContactAddressEntity.Create();
                this._oOrderContactAddress.OrderId = this.Id;
                this._oOrderContactAddress.Insert();
            }

            if (null != this._oOrderContactPerson)
            {
                this._oOrderContactPerson.OrderId = this.Id;
            }
            else
            {
                this._oOrderContactPerson = MaxOrderContactPersonEntity.Create();
                this._oOrderContactPerson.OrderId = this.Id;
                this._oOrderContactPerson.EmailSignup = true;
                this._oOrderContactPerson.Insert();
            }

            if (null != this._oOrderShippingInfo)
            {
                this._oOrderShippingInfo.OrderId = this.Id;
            }
            else
            {
                this._oOrderShippingInfo = MaxOrderShippingInfoEntity.Create();
                this._oOrderShippingInfo.OrderId = this.Id;
                this._oOrderShippingInfo.ShippingType = this.ShippingType;
                this._oOrderShippingInfo.Insert();
            }

            if (null != this._oOrderPaymentList)
            {
                foreach (MaxOrderPaymentEntity loPayment in this._oOrderPaymentList)
                {
                    loPayment.OrderId = this.Id;
                }
            }
            else
            {
                this._oOrderPaymentList = new List<MaxOrderPaymentEntity>();
                MaxOrderPaymentEntity loOrderPaymentCard = MaxOrderPaymentEntity.Create();
                loOrderPaymentCard.PaymentDetailType = MaxOrderPaymentDetailEntity.PaymentDetailTypeCard;
                loOrderPaymentCard.OrderId = this.Id;
                loOrderPaymentCard.Insert();
                this._oOrderPaymentList.Add(loOrderPaymentCard);

                MaxOrderPaymentEntity loOrderPaymentGeneral = MaxOrderPaymentEntity.Create();
                loOrderPaymentGeneral.PaymentDetailType = MaxOrderPaymentDetailEntity.PaymentDetailTypeGeneral;
                loOrderPaymentGeneral.OrderId = this.Id;
                loOrderPaymentGeneral.Insert();
                this._oOrderPaymentList.Add(loOrderPaymentGeneral);
            }

            return lbR;
        }

        public override bool Update()
        {
            if (this.CartIdList.Count > 0)
            {
                string lsIdList = string.Empty;
                foreach (Guid loId in this.CartIdList)
                {
                    if (lsIdList.Length > 0)
                    {
                        lsIdList += "|";
                    }

                    lsIdList += loId.ToString();
                }

                this.Set(this.DataModel.CartIdListText, lsIdList);
            }

            if (base.Update())
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
                    foreach (MaxOrderPaymentEntity loPayment in this._oOrderPaymentList)
                    {
                        loPayment.OrderId = this.Id;
                    }
                }

                return true;
            }

            return false;
        }

        public override int ArchiveAbandoned()
        {
            int lnR = 0;
            if (this.CanProcessArchive(new TimeSpan(24, 0, 0)))
            {
                lnR = this.Archive(DateTime.MinValue, DateTime.UtcNow.AddDays(-30), false);
            }

            return lnR;
        }

        /// <summary>
        /// Archive this cart and all items related to it.
        /// </summary>
        /// <returns></returns>
        public override bool Archive()
        {
            bool lbR = true;
            if (this.ProcessingStatus <= ProcessingStatusStarted ||
                this.ProcessingStatus == ProcessingStatusArchived)
            {
                if (!string.IsNullOrEmpty(this.AlternateId))
                {
                    MaxOrderIdEntity loOrderId = MaxOrderIdEntity.Create();
                    int lnAlternateId = MaxConvertLibrary.ConvertToInt(typeof(object), this.AlternateId);
                    if (loOrderId.LoadById(lnAlternateId))
                    {
                        loOrderId.Archive();
                    }
                }

                if (this.OrderContactAddress.Id != Guid.Empty && !this.OrderContactAddress.Archive())
                {
                    lbR = false;
                }

                if (this.OrderContactPerson.Id != Guid.Empty && !this.OrderContactPerson.Archive())
                {
                    lbR = false;
                }

                if (this.OrderShippingInfo.Id != Guid.Empty && !this.OrderShippingInfo.Archive())
                {
                    lbR = false;
                }

                foreach (MaxOrderPaymentEntity loOrderPayment in this.OrderPaymentList)
                {
                    if (!loOrderPayment.Archive())
                    {
                        lbR = false;
                    }
                }

                if (lbR)
                {
                    lbR = base.Archive();
                }
            }

            return lbR;
        }

        protected override MaxEntityList LoadAllItem()
        {
            MaxOrderItemEntity loEntity = MaxOrderItemEntity.Create();
            MaxEntityList loList = loEntity.LoadAllByContainerId(this.Id);
            if (loList.Count == 0)
            {
                loEntity.StorageKey = this.StorageKey;
                loList = loEntity.LoadAllByContainerId(this.Id);
            }

            return loList;
        }

    }
}
