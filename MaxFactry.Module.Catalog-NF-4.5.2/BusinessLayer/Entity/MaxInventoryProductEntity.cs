// <copyright file="MaxInventoryProductEntity.cs" company="Lakstins Family, LLC">
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
// <change date="9/2/2016" author="Brian A. Lakstins" description="Initial creation">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxInventoryProductEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        private long _nAmountAvailable = long.MinValue;

        public const int OnOrder = 1;

        public const int OnShip = 2;

        public const int OnUnship = 3;

        public const int OnCancel = 4;

        public const int OnPayment = 5;

        public const int OnUnCancel = 6;

        /// <summary>
        /// Initializes a new instance of the MaxInventoryEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxInventoryProductEntity(MaxData loData) : base(loData)
		{
		}

        /// <summary>
        /// Initializes a new instance of the MaxInventoryEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxInventoryProductEntity(Type loDataModelType)
            : base(loDataModelType)
        {
        }

        public string ProductSku
        {
            get
            {
                return this.GetString(this.DataModel.ProductSku);
            }

            set
            {
                this.Set(this.DataModel.ProductSku, value);
            }
        }

        public string Name
        {
            get
            {
                return this.GetString(this.DataModel.Name);
            }

            set
            {
                this.Set(this.DataModel.Name, value);
            }
        }


        public string SupplySkuList
        {
            get
            {
                return this.GetString(this.DataModel.SupplySkuList);
            }

            set
            {
                this.Set(this.DataModel.SupplySkuList, value);
            }
        }


        public long AmountOnOrder
        {
            get
            {
                return this.GetLong(this.DataModel.AmountOnOrder);
            }

            set
            {
                this.Set(this.DataModel.AmountOnOrder, value);
            }
        }


        public long ReplenishAlertLevel
        {
            get
            {
                return this.GetLong(this.DataModel.ReplenishAlertLevel);
            }

            set
            {
                this.Set(this.DataModel.ReplenishAlertLevel, value);
            }
        }

        public int UnitOfMeasure
        {
            get
            {
                return this.GetInt(this.DataModel.UnitOfMeasure);
            }

            set
            {
                this.Set(this.DataModel.UnitOfMeasure, value);
            }
        }

        public long AmountAvailable
        {
            get
            {
                if (this._nAmountAvailable == long.MinValue)
                {
                    this._nAmountAvailable = 0;
                    long lnMin = long.MaxValue;
                    string[] laSupply = this.SupplySkuList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    //// TODO: Restrict supply list to only those that match this sku list and are active
                    MaxEntityList loSupplyList = MaxInventorySupplyEntity.Create().LoadAllCache();
                    //// TODO: Restrict product list to only those that have an amount on order and match this sku list and are active
                    MaxEntityList loProductList = MaxInventoryProductEntity.Create().LoadAllCache();
                    foreach (string lsSupplySku in laSupply)
                    {
                        long lnAmount = 1;
                        string lsSku = lsSupplySku;
                        if (lsSupplySku.Contains(":"))
                        {
                            string[] laSupplySku = lsSupplySku.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            lsSku = laSupplySku[0];
                            lnAmount = MaxConvertLibrary.ConvertToLong(typeof(object), laSupplySku[1]);
                        }

                        long lnTotal = 0; //// Total available from all supply sites and locations
                        for (int lnE = 0; lnE < loSupplyList.Count; lnE++)
                        {
                            MaxInventorySupplyEntity loEntity = loSupplyList[lnE] as MaxInventorySupplyEntity;
                            if (loEntity.SupplySku.Equals(lsSku, StringComparison.InvariantCultureIgnoreCase) && loEntity.IsActive)
                            {
                                lnTotal += loEntity.AmountCurrent;
                            }
                        }

                        long lnOnOrder = 0; //// Total on order from all products that include this sku
                        for (int lnE = 0; lnE < loProductList.Count; lnE++)
                        {
                            MaxInventoryProductEntity loEntity = loProductList[lnE] as MaxInventoryProductEntity;
                            if (loEntity.SupplySkuList.ToLower().Contains(lsSku.ToLower()) && loEntity.IsActive && loEntity.AmountOnOrder > 0)
                            {
                                string[] laSupplyOnOrder = loEntity.SupplySkuList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string lsSupplyOnOrderSku in laSupplyOnOrder)
                                {
                                    string lsSkuOnOrder = lsSupplyOnOrderSku;
                                    long lnAmountOnOrder = 1;
                                    if (lsSupplyOnOrderSku.Contains(":"))
                                    {
                                        string[] laSupplyOnOrderSku = lsSupplyOnOrderSku.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                        lsSkuOnOrder = laSupplyOnOrderSku[0];
                                        lnAmountOnOrder = MaxConvertLibrary.ConvertToLong(typeof(object), laSupplyOnOrderSku[1]);
                                    }

                                    if (lsSkuOnOrder.Equals(lsSku, StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        lnOnOrder += lnAmountOnOrder * loEntity.AmountOnOrder;
                                    }
                                }
                            }
                        }

                        //// Total available as a quantity of this product
                        long lnCurrent = Convert.ToInt64(Math.Floor(Convert.ToDouble(lnTotal - lnOnOrder) / Convert.ToDouble(lnAmount))); ;

                        if (lnCurrent < lnMin)
                        {
                            lnMin = lnCurrent;
                        }
                    }

                    this._nAmountAvailable = lnMin;
                }

                return this._nAmountAvailable;
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxInventoryProductDataModel DataModel
        {
            get
            {
                return (MaxInventoryProductDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxInventoryProductEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxInventoryProductEntity),
                typeof(MaxInventoryProductDataModel)) as MaxInventoryProductEntity;
        }

        public static long GetInventoryAmountAvailable(string lsProductSku)
        {
            long lnR = long.MinValue;
            MaxEntityList loProductList = MaxInventoryProductEntity.Create().LoadAllCache();
            for (int lnE = 0; lnE < loProductList.Count; lnE++)
            {
                MaxInventoryProductEntity loEntity = loProductList[lnE] as MaxInventoryProductEntity;
                if (loEntity.ProductSku.Equals(lsProductSku, StringComparison.InvariantCultureIgnoreCase))
                {
                    lnR = loEntity.AmountAvailable;
                }
            }

            return lnR;
        }

        /// <summary>
        /// Decrement Current Amount when Order Amount is created
        /// On order amount goes down when it has been shipped
        /// When undo "shipped", add back any inventory that was decremented
        /// If order cancelled, then decrement On Order - and add back current amount
        /// Change amount available when the order has had any payment
        /// </summary>
        /// <returns></returns>
        public static bool UpdateInventoryAmount(MaxOrderEntity loOrder, int lnChangeType)
        {
            bool lbR = false;
            MaxEntityList loProductList = MaxInventoryProductEntity.Create().LoadAllCache();
            MaxEntityList loSupplyList = MaxInventorySupplyEntity.Create().LoadAllCache();
            foreach (MaxOrderItemEntity loItem in loOrder.ItemList)
            {
                string lsProductSku = loItem.Sku;
                for (int lnE = 0; lnE < loProductList.Count; lnE++)
                {
                    MaxInventoryProductEntity loEntity = loProductList[lnE] as MaxInventoryProductEntity;
                    if (loEntity.ProductSku.Equals(lsProductSku, StringComparison.InvariantCultureIgnoreCase))
                    {
                        string[] laSupply = loEntity.SupplySkuList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        if (lnChangeType == OnOrder)
                        {
                            //Change amount available when the order has had any payment
                            //Decrement Current Amount when Order Amount is created
                            loEntity.AmountOnOrder = MaxInventoryLogEntity.LogInventoryChange(loEntity.Id, "Confirmed Order " + loOrder.AlternateId, loItem.Quantity, MaxInventoryLogEntity.LogEntryTypeOrder, loEntity.AmountOnOrder, loOrder.Username);
                            loEntity.Update();
                        }
                        else if (lnChangeType == OnPayment)
                        {
                            //Change amount available when the order has had any payment
                            //Decrement Current Amount when Order Amount is created
                            loEntity.AmountOnOrder = MaxInventoryLogEntity.LogInventoryChange(loEntity.Id, "Payment Order " + loOrder.AlternateId, loItem.Quantity, MaxInventoryLogEntity.LogEntryTypeOrder, loEntity.AmountOnOrder, loOrder.Username);
                            loEntity.Update();
                        }
                        else if (lnChangeType == OnShip)
                        {
                            loEntity.AmountOnOrder = MaxInventoryLogEntity.LogInventoryChange(loEntity.Id, "Ship Order " + loOrder.AlternateId, -1 * loItem.Quantity, MaxInventoryLogEntity.LogEntryTypeOrder, loEntity.AmountOnOrder, loOrder.Username);
                            loEntity.Update();
                            // Reducing supply amount from shipping location
                            foreach (string lsSupplySku in laSupply)
                            {
                                int lnAmount = 1;
                                string lsSku = lsSupplySku;
                                if (lsSupplySku.Contains(":"))
                                {
                                    string[] laSupplySku = lsSupplySku.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                    lsSku = laSupplySku[0];
                                    lnAmount = MaxConvertLibrary.ConvertToInt(typeof(object), laSupplySku[1]);
                                }

                                bool lbChanged = false;
                                for (int lnS = 0; lnS < loSupplyList.Count; lnS++)
                                {
                                    if (!lbChanged)
                                    {
                                        MaxInventorySupplyEntity loSupplyEntity = loSupplyList[lnS] as MaxInventorySupplyEntity;
                                        if (loSupplyEntity.SupplySku.Equals(lsSku, StringComparison.InvariantCultureIgnoreCase) && loSupplyEntity.IsActive)
                                        {
                                            loSupplyEntity.ChangeAmountCurrent(-1 * lnAmount, "Order " + loOrder.AlternateId + " shipped.", null);
                                            lbChanged = true;
                                        }
                                    }
                                }
                            }
                        }
                        else if (lnChangeType == OnCancel)
                        {
                            //If order cancelled, then decrement On Order - 
                            loEntity.AmountOnOrder = MaxInventoryLogEntity.LogInventoryChange(loEntity.Id, "Cancel Order " + loOrder.AlternateId, -1 * loItem.Quantity, MaxInventoryLogEntity.LogEntryTypeOrder, loEntity.AmountOnOrder, loOrder.Username);
                            loEntity.Update();
                        }
                        else if (lnChangeType == OnUnCancel)
                        {
                            //If order cancelled, then decrement On Order - 
                            loEntity.AmountOnOrder = MaxInventoryLogEntity.LogInventoryChange(loEntity.Id, "Uncancel Order " + loOrder.AlternateId, loItem.Quantity, MaxInventoryLogEntity.LogEntryTypeOrder, loEntity.AmountOnOrder, loOrder.Username);
                            loEntity.Update();
                        }
                        else if (lnChangeType == OnUnship)
                        {
                            loEntity.AmountOnOrder = MaxInventoryLogEntity.LogInventoryChange(loEntity.Id, "Unship Order " + loOrder.AlternateId, loItem.Quantity, MaxInventoryLogEntity.LogEntryTypeOrder, loEntity.AmountOnOrder, loOrder.Username);
                            loEntity.Update();
                            // Increase supply amount from shipping location
                            foreach (string lsSupplySku in laSupply)
                            {
                                int lnAmount = 1;
                                string lsSku = lsSupplySku;
                                if (lsSupplySku.Contains(":"))
                                {
                                    string[] laSupplySku = lsSupplySku.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                    lsSku = laSupplySku[0];
                                    lnAmount = MaxConvertLibrary.ConvertToInt(typeof(object), laSupplySku[1]);
                                }

                                bool lbChanged = false;
                                for (int lnS = 0; lnS < loSupplyList.Count; lnS++)
                                {
                                    if (!lbChanged)
                                    {
                                        MaxInventorySupplyEntity loSupplyEntity = loSupplyList[lnS] as MaxInventorySupplyEntity;
                                        if (loSupplyEntity.SupplySku.Equals(lsSku, StringComparison.InvariantCultureIgnoreCase) && loSupplyEntity.IsActive)
                                        {
                                            loSupplyEntity.ChangeAmountCurrent(lnAmount, "Order " + loOrder.AlternateId + " un shipped.", null);
                                            lbChanged = true;
                                        }
                                    }
                                }
                            }
                        }

                        lbR = true;
                    }
                }
            }

            return lbR;
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            return this.ProductSku.ToLowerInvariant().PadRight(100, ' ') + base.GetDefaultSortString();
        }

        public override bool Insert()
        {
            this.AmountOnOrder = 0;
            return base.Insert();
        }
    }
}
