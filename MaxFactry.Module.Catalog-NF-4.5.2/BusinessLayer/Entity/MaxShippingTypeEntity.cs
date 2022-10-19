// <copyright file="MaxShippingTypeEntity.cs" company="Lakstins Family, LLC">
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
// <change date="3/12/2015" author="Brian A. Lakstins" description="Initial creation">
// <change date="9/7/2018" author="Brian A. Lakstins" description="Fix issue with shipping type not being selectable">
// <change date="9/13/2018" author="Brian A. Lakstins" description="Add shipping type for pick up.">
// <change date="11/30/2018" author="Brian A. Lakstins" description="Add shipping type for special shipping that is free.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxShippingTypeEntity : MaxFactry.Base.BusinessLayer.MaxBaseIdEntity
    {
        public static readonly int ShippingTypePickup = 100;

        public static readonly int ShippingTypeCommercial = 200;

        public static readonly int ShippingTypeResidence = 400;

        public static readonly int ShippingTypeArrange = 500;

        public static readonly int ShippingTypeFree = 600;

        public static readonly int ShippingTypeStandard = 700;

        public static readonly int ShippingTypeSpecial = 800;

        /// <summary>
        /// Initializes a new instance of the MaxShippingTypeEntity class
		/// </summary>
		/// <param name="loData">object to hold data</param>
		public MaxShippingTypeEntity(MaxData loData) : base(loData)
        {
        }

        /// <summary>
        /// Initializes a new instance of the MaxShippingTypeEntity class.
        /// </summary>
        /// <param name="loDataModelType">Type of data model.</param>
        public MaxShippingTypeEntity(Type loDataModelType)
            : base(loDataModelType)
        {
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

        public string Description
        {
            get
            {
                return this.GetString(this.DataModel.Description);
            }

            set
            {
                this.Set(this.DataModel.Description, value);
            }
        }

        public int ShippingType
        {
            get
            {
                return this.GetInt(this.DataModel.ShippingType);
            }

            set
            {
                this.Set(this.DataModel.ShippingType, value);
            }
        }

        public double RelationOrder
        {
            get
            {
                return this.GetDouble(this.DataModel.RelationOrder);
            }

            set
            {
                this.Set(this.DataModel.RelationOrder, value);
            }
        }

        public string ShippingCalculation
        {
            get
            {
                return this.GetString(this.DataModel.ShippingCalculation);
            }

            set
            {
                this.Set(this.DataModel.ShippingCalculation, value);
            }
        }

        public bool IsSelectable
        {
            get
            {
                return this.GetBoolean(this.DataModel.IsSelectable);
            }

            set
            {
                this.Set(this.DataModel.IsSelectable, value);
            }
        }

        /// <summary>
        /// Gets the Data Model for this entity
        /// </summary>
        protected MaxShippingTypeDataModel DataModel
        {
            get
            {
                return (MaxShippingTypeDataModel)MaxDataLibrary.GetDataModel(this.DataModelType);
            }
        }

        public static MaxShippingTypeEntity Create()
        {
            return MaxBusinessLibrary.GetEntity(
                typeof(MaxShippingTypeEntity),
                typeof(MaxShippingTypeDataModel)) as MaxShippingTypeEntity;
        }

        /// <summary>
        /// Gets a string that can be used to sort a list of this entity.
        /// </summary>
        /// <returns>Lowercase version of Name passed to 100 characters.</returns>
        public override string GetDefaultSortString()
        {
            double lnOrder = this.RelationOrder;
            if (lnOrder < 0)
            {
                lnOrder = 0;
            }

            lnOrder = lnOrder * Math.Pow(10, 5);
            return Convert.ToInt64(lnOrder).ToString("D10") + this.Name.ToLowerInvariant().PadRight(100, ' ') + base.GetDefaultSortString();
        }

        public bool LoadByShippingType(int lnShippingType)
        {
            MaxEntityList loList = this.LoadAll();
            for (int lnE = 0; lnE < loList.Count; lnE++)
            {
                MaxShippingTypeEntity loEntity = loList[lnE] as MaxShippingTypeEntity;
                if (loEntity.ShippingType == lnShippingType)
                {
                    this.Load(loEntity.GetData());
                    return true;
                }
            }

            return false;
        }

        public override MaxEntityList LoadAll()
        {
            MaxEntityList loR = MaxEntityList.Create(typeof(MaxShippingTypeEntity));
            if (MaxClientRepository.IsTractorToolsDirect(this.Data))
            {
                MaxShippingTypeEntity loShippingType6 = MaxShippingTypeEntity.Create();
                loShippingType6.ShippingType = ShippingTypeFree;
                loShippingType6.Name = "Standard Delivery (Free)";
                loShippingType6.ShippingCalculation = "FREE";
                loShippingType6.IsSelectable = true;
                loR.Add(loShippingType6);
                MaxShippingTypeEntity loShippingType1 = MaxShippingTypeEntity.Create();
                loShippingType1.ShippingType = ShippingTypePickup;
                loShippingType1.Name = "Pick Up (Free)";
                loShippingType1.ShippingCalculation = "FREE";
                loShippingType1.IsSelectable = true;
                loR.Add(loShippingType1);
                MaxShippingTypeEntity loShippingType2 = MaxShippingTypeEntity.Create();
                loShippingType2.ShippingType = ShippingTypeCommercial;
                loShippingType2.Name = "Commercial location (has dock or forklift, no residence on site)";
                loShippingType2.ShippingCalculation = "0";
                loShippingType2.IsSelectable = true;
                loR.Add(loShippingType2);
                /*
                MaxShippingTypeEntity loShippingType3 = MaxShippingTypeEntity.Create();
                loShippingType3.ShippingType = 300;
                loShippingType3.Name = "Residence or farm with dock or forklift";
                loShippingType3.ShippingCalculation = "70";
                loShippingType3.IsSelectable = true;
                loR.Add(loShippingType3);
                */
                MaxShippingTypeEntity loShippingType4 = MaxShippingTypeEntity.Create();
                loShippingType4.ShippingType = ShippingTypeResidence;
                loShippingType4.Name = "Residence or farm with lift gate service";
                loShippingType4.ShippingCalculation = "80";
                loShippingType4.IsSelectable = true;
                loR.Add(loShippingType4);
                MaxShippingTypeEntity loShippingType5 = MaxShippingTypeEntity.Create();
                loShippingType5.ShippingType = ShippingTypeArrange;
                loShippingType5.Name = "Shipping Arrangement Required";
                loShippingType5.Description = "Please complete order and then call 260-225-3429 to arrange shipping.";
                loShippingType5.ShippingCalculation = "TBD";
                loR.Add(loShippingType5);
                MaxShippingTypeEntity loShippingType7 = MaxShippingTypeEntity.Create();
                loShippingType7.ShippingType = ShippingTypeStandard;
                loShippingType7.Name = "Shipping Standard";
                loShippingType7.ShippingCalculation = "0";
                loR.Add(loShippingType7);
            }
            else if (MaxClientRepository.IsDisplayConnection(this.Data))
            {
                MaxShippingTypeEntity loShippingType5 = MaxShippingTypeEntity.Create();
                loShippingType5.ShippingType = ShippingTypeArrange;
                loShippingType5.Name = "Shipping Arrangement Required";
                loShippingType5.Description = "We will contact you to arrange shipping.";
                loShippingType5.ShippingCalculation = "TBD";
                loShippingType5.IsSelectable = true;
                loR.Add(loShippingType5);
            }
            else
            {
                MaxShippingTypeEntity loShippingType1 = MaxShippingTypeEntity.Create();
                loShippingType1.ShippingType = ShippingTypePickup;
                loShippingType1.Name = "Pick Up (Free)";
                loShippingType1.ShippingCalculation = "FREE";
                loShippingType1.IsSelectable = true;
                loR.Add(loShippingType1);
                MaxShippingTypeEntity loShippingType6 = MaxShippingTypeEntity.Create();
                loShippingType6.ShippingType = ShippingTypeFree;
                loShippingType6.Name = "Free Shipping";
                loShippingType6.ShippingCalculation = "Free";
                loShippingType6.IsSelectable = true;
                loR.Add(loShippingType6);
                MaxShippingTypeEntity loShippingType5 = MaxShippingTypeEntity.Create();
                loShippingType5.ShippingType = ShippingTypeArrange;
                loShippingType5.Name = "Shipping Arrangement Required";
                loShippingType5.Description = "We will contact you to arrange shipping.";
                loShippingType5.ShippingCalculation = "TBD";
                loShippingType5.IsSelectable = true;
                loR.Add(loShippingType5);
                MaxShippingTypeEntity loShippingType7 = MaxShippingTypeEntity.Create();
                loShippingType7.ShippingType = ShippingTypeStandard;
                loShippingType7.Name = "Shipping Standard";
                loShippingType7.ShippingCalculation = "0";
                loShippingType7.IsSelectable = true;
                loR.Add(loShippingType7);
                MaxShippingTypeEntity loShippingType8 = MaxShippingTypeEntity.Create();
                loShippingType8.ShippingType = ShippingTypeSpecial;
                loShippingType8.Name = "Special Shipping";
                loShippingType8.ShippingCalculation = "Free";
                loShippingType8.Description = "No shipping information is needed";
                loShippingType8.IsSelectable = true;
                loR.Add(loShippingType8);
            }

            return loR;
        }


    }
}
