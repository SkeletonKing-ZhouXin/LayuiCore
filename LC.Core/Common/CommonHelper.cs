using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LC.Core.Common
{
    public partial class CommonHelper
    {
        #region Properties

        /// <summary>
        /// Gets or sets application base path
        /// </summary>
        internal static string BaseDirectory { get; set; }

        #endregion
    }
}
