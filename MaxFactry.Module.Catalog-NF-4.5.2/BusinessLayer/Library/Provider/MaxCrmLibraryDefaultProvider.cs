// <copyright file="MaxCrmLibraryDefaultProvider.cs" company="Lakstins Family, LLC">
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
// <change date="11/12/2015" author="Brian A. Lakstins" description="Initial creation">
// <change date="12/4/2015" author="Brian A. Lakstins" description="Upate processing of sales type transaction to show collected.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer.Provider
{
    using System;
    using System.Collections.Generic;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;
    using MaxFactry.Module.Crm.BusinessLayer;

    public class MaxCrmLibraryDefaultProvider : MaxFactry.Core.MaxProvider, MaxFactry.Module.Catalog.BusinessLayer.IMaxCrmLibraryProvider
    {
        public virtual bool UpdateCrm(MaxOrderEntity loOrder)
        {
            bool lbR = false;
            MaxCrmPersonEntity loPerson = MaxCrmPersonEntity.Create();
            MaxEntityList loList = loPerson.LoadAllBySource("MaxCatalogOrder", loOrder.Id.ToString());
            if (loList.Count == 0)
            {
                loPerson = MaxCrmPersonEntity.Create();
                loPerson.SourceType = "MaxCatalogOrder";
                loPerson.SourceId = loOrder.Id.ToString();
                loPerson.SourceDate = loOrder.OrderPlacedDate;
                loPerson.CurrentFirstName = loOrder.OrderContactPerson.CurrentFirstName;
                loPerson.CurrentLastName = loOrder.OrderContactPerson.CurrentLastName;
                loPerson.MainEmail = loOrder.OrderContactPerson.Email;
                loPerson.MainPhone = loOrder.OrderContactPerson.Phone;
                loPerson.Insert();
            }

            return lbR;
        }
    }
}
