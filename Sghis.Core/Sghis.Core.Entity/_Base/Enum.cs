
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sghis.Core.Entity.Base
{
    public enum DbSchema
    {
        [Description("Common")]
        Common = 1,
        [Description("Master")]
        Master = 2,
        [Description("Wards")]
        Wards = 3,
        [Description("Arms")]
        Arms = 4,
        [Description("IpBilling")]
        IpBilling = 5,
        [Description("Qms")]
        Qms = 6
    }

    public enum Gender
    {
        [Description("Female")]
        Female = 1,
        [Description("Male")]
        Male = 2,
        [Description("Others")]
        Others = 3,
        [Description("Unknown")]
        Unknown = 4
    }

    public enum Religion
    {
        [Description("Hindu")]
        Hindu = 1,
        [Description("Islam")]
        Islam = 2,
        [Description("Christian")]
        Christian = 3
    }


    public enum MaritalStatus
    {
        [Description("Divorced")]
        Divorced = 1,
        [Description("Married")]
        Married = 2,
        [Description("Married Alone")]
        MarriedAlone = 3,
        [Description("Not Disclosed")]
        NotDisclosed = 4,
        [Description("Separated")]
        Separated = 5,
        [Description("Single")]
        Single = 6,
        [Description("Unknown")]
        Unknown = 7,
        [Description("Widowed")]
        Widowed = 7
    }

    public enum ApplicationTypes
    {
        WebNative = 1, //non javascript
        JavaScript = 2,
        Mobile = 3,
        Desktop = 4
    };
}
