// <copyright file="MaxOrderShippingAddressViewModel.cs" company="Lakstins Family, LLC">
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
// <change date="2/26/2014" author="Brian A. Lakstins" description="Initial Release">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.PresentationLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.PresentationLayer;
    using MaxFactry.Module.Catalog.BusinessLayer;

    /// <summary>
    /// View Model for Order Shipping Address
    /// </summary>
    public class MaxOrderShippingAddressViewModel : MaxFactry.Base.PresentationLayer.MaxBaseUSPostalAddressViewModel
    {
        public MaxOrderShippingAddressViewModel() : base()
		{
		}

        public MaxOrderShippingAddressViewModel(MaxEntity loEntity)
            : base(loEntity)
        {
        }

        protected override void CreateEntity()
        {
            this.Entity = MaxOrderShippingAddressEntity.Create();
        }

        public string Name
        {
            get;
            set;
        }

        public string Notes
        {
            get;
            set;
        }

        public List<MaxFactry.General.BusinessLayer.MaxUSStateEntity> USStateList
        {
            get
            {
                int[] laStatus = { 0 };
                MaxFactry.General.BusinessLayer.MaxUSStateEntity loEntity = MaxFactry.General.BusinessLayer.MaxUSStateEntity.Create();
                MaxEntityList loEntityList = loEntity.LoadAllByStatus(laStatus);
                SortedList<string, MaxFactry.General.BusinessLayer.MaxUSStateEntity> loList = new SortedList<string, MaxFactry.General.BusinessLayer.MaxUSStateEntity>();
                for (int lnE = 0; lnE < loEntityList.Count; lnE++)
                {
                    MaxFactry.General.BusinessLayer.MaxUSStateEntity loEntityCurrent = loEntityList[lnE] as MaxFactry.General.BusinessLayer.MaxUSStateEntity;
                    if (!loEntityCurrent.Abbreviation.Equals("HI", StringComparison.InvariantCultureIgnoreCase)
                        && !loEntityCurrent.Abbreviation.Equals("AK", StringComparison.InvariantCultureIgnoreCase))
                    {
                        loList.Add(loEntityCurrent.GetDefaultSortString(), loEntityCurrent);
                    }
                }

                return new List<MaxFactry.General.BusinessLayer.MaxUSStateEntity>(loList.Values);
            }
        }

        public Dictionary<string, string> GetStateList()
        {
            Dictionary<string, string> loR = new Dictionary<string, string>();
            loR.Add("Select state", string.Empty);
            foreach (MaxFactry.General.BusinessLayer.MaxUSStateEntity loState in USStateList)
            {
                loR.Add(loState.Name, loState.Abbreviation);
            }

            return loR;
        }

        protected override bool MapFromEntity()
        {
            if (base.MapFromEntity())
            {
                MaxOrderShippingAddressEntity loEntity = this.Entity as MaxOrderShippingAddressEntity;
                if (null != loEntity)
                {
                    this.Name = loEntity.Name;
                    this.Notes = loEntity.Notes;
                    return true;
                }
            }

            return false;
        }

        protected override bool MapToEntity()
        {
            if (base.MapToEntity())
            {
                MaxOrderShippingAddressEntity loEntity = this.Entity as MaxOrderShippingAddressEntity;
                if (null != loEntity)
                {
                    loEntity.Name = this.Name;
                    loEntity.Notes = this.Notes;
                    return true;
                }
            }

            return false;
        }
    }
}
