using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration.View
{
    public class DBFieldUserType
    {
        public Dictionary<int, Type> SQLUserType = new Dictionary<int, Type>() { 
                                                   {36,typeof(System.Guid)},
                                                   {40,typeof(System.DateTime)},
                                                   {41,typeof(System.DateTime)},
                                                   {42,typeof(System.DateTime)},
                                                   {43,typeof(System.DateTime)},
                                                   {48,typeof(System.Byte)},
                                                   {52,typeof(System.Int16)},
                                                   {56,typeof(System.Int32)},
                                                   {61,typeof(System.DateTime)},
                                                   {62,typeof(System.Double)},
                                                   {104,typeof(System.Boolean)},
                                                   {106,typeof(System.Decimal)},
                                                   {127,typeof(System.Int64)},
                                                   {231,typeof(System.String)},

        };
    }
}
