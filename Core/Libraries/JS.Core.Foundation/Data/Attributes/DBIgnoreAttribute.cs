using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Data
{
    /// <summary>
    /// Attribute used to decorate properties that are not Database Columns
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited = true)]
    public class DBIgnoreAttribute : Attribute
    {
    }
}
