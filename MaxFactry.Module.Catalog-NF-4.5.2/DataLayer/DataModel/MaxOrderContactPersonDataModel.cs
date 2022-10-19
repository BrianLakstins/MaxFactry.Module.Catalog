// <copyright file="MaxOrderContactPersonDataModel.cs" company="Lakstins Family, LLC">
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
// <change date="8/4/2014" author="Brian A. Lakstins" description="Initial Release">
// <change date="12/21/2016" author="Brian A. Lakstins" description="Updated to use PrimaryKey Suffix.">
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.DataLayer
{
    using System;
    using MaxFactry.Base.DataLayer;

    /// <summary>
    /// Data Model for order related information.
    /// </summary>
    public class MaxOrderContactPersonDataModel : MaxFactry.Base.DataLayer.MaxBasePersonDataModel
    {
        /// <summary>
        /// Id of the Order this Item is associated
        /// </summary>
        public readonly string OrderId = "OrderId";

        /// <summary>
        /// Phone number of the person to contact about the order
        /// </summary>
        public readonly string Phone = "Phone";

        /// <summary>
        /// Email address of the person to contact about the order
        /// </summary>
        public readonly string Email = "Email";

        /// <summary>
        /// Notes concerning this contact.
        /// </summary>
        public readonly string Notes = "Notes";

        /// <summary>
        /// Indicates that the person want to sign up for the email
        /// </summary>
        public readonly string EmailSignup = "EmailSignup";

        /// <summary>
        /// Initializes a new instance of the MaxCatalogDataModel class
        /// </summary>
        public MaxOrderContactPersonDataModel()
        {
            this.SetDataStorageName("MaxCatalogOrderContactPerson");
            this.RepositoryProviderType = typeof(MaxFactry.Module.Catalog.DataLayer.Provider.MaxCatalogRepositoryProvider);
            this.RepositoryType = typeof(MaxCatalogRepository);
            this.AddType(this.OrderId, typeof(Guid));
            this.AddType(this.Phone, typeof(MaxShortString));
            this.AddType(this.Email, typeof(MaxShortString));
            this.AddType(this.Notes, typeof(string));
            this.AddType(this.EmailSignup, typeof(bool));
        }

        /// <summary>
        /// Initializes a new instance of the MaxOrderContactPersonDataModel class.
        /// </summary>
        /// <param name="lsDataStorageName">Name to user for storage</param>
        public MaxOrderContactPersonDataModel(string lsDataStorageName) : this()
        {
            this.SetDataStorageName(lsDataStorageName);
        }

        /// <summary>
        /// Gets a suffix for the primary key based on the data to speed up future queries
        /// </summary>
        /// <param name="loData">Data to use to create the suffix</param>
        /// <returns>String to use as suffix for primary key</returns>
        public override string GetPrimaryKeySuffix(MaxData loData)
        {
            string lsR = base.GetPrimaryKeySuffix(loData);
            if (string.IsNullOrEmpty(lsR))
            {
                lsR = loData.Get(this.OrderId).ToString();
            }

            return lsR;
        }
    }
}
