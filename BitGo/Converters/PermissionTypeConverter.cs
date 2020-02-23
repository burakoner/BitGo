using BitGo.Base;
using BitGo.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Converters
{
    public class PermissionTypeConverter : BaseConverter<PermissionType>
    {
        public PermissionTypeConverter() : this(true) { }
        public PermissionTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<PermissionType, string>> Mapping => new List<KeyValuePair<PermissionType, string>>
        {
            new KeyValuePair<PermissionType, string>(PermissionType.Admin, "admin"),
            new KeyValuePair<PermissionType, string>(PermissionType.View, "view"),
            new KeyValuePair<PermissionType, string>(PermissionType.Spend, "spend"),
        };
    }
}
