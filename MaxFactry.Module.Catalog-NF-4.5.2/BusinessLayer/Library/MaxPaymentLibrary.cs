// <copyright file="MaxPaymentLibrary.cs" company="Lakstins Family, LLC">
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
// </changelog>
#endregion

namespace MaxFactry.Module.Catalog.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using MaxFactry.Core;
    using MaxFactry.Base.BusinessLayer;
    using MaxFactry.Base.DataLayer;
    using MaxFactry.Module.Catalog.DataLayer;

    public class MaxPaymentLibrary : MaxFactry.Core.MaxMultipleFactory
    {
        /// <summary>
        /// Internal storage of single object
        /// </summary>
        private static MaxPaymentLibrary _oInstance = null;

        /// <summary>
        /// Lock object for multi-threaded access.
        /// </summary>
        private static object _oLock = new object();

        /// <summary>
        /// Gets the single instance of this class.
        /// </summary>
        public static MaxPaymentLibrary Instance
        {
            get
            {
                if (null == _oInstance)
                {
                    lock (_oLock)
                    {
                        if (null == _oInstance)
                        {
                            _oInstance = new MaxPaymentLibrary();
                        }
                    }
                }

                return _oInstance;
            }
        }


        public static bool ProcessTransaction(MaxOrderPaymentTransactionEntity loTransaction)
        {
            IMaxProvider[] loList = Instance.GetProviderList();
            bool lbR = false;
            for (int lnP = 0; lnP < loList.Length; lnP++)
            {
                if (loList[lnP] is IMaxPaymentLibraryProvider)
                {
                    if (!lbR)
                    {
                        lbR = ((IMaxPaymentLibraryProvider)loList[lnP]).ProcessTransaction(loTransaction);
                    }
                }
            }

            return lbR;
        }

    }
}
