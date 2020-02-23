using BitGo.Base;
using BitGo.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Converters
{
    public class KeySourceTypeConverter : BaseConverter<KeySourceType>
    {
        public KeySourceTypeConverter() : this(true) { }
        public KeySourceTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<KeySourceType, string>> Mapping => new List<KeyValuePair<KeySourceType, string>>
        {
            new KeyValuePair<KeySourceType, string>(KeySourceType.Backup, "backup"),
            new KeyValuePair<KeySourceType, string>(KeySourceType.Bitgo, "bitgo"),
            new KeyValuePair<KeySourceType, string>(KeySourceType.Cold, "cold"),
            new KeyValuePair<KeySourceType, string>(KeySourceType.User, "user"),
        };
    }
}
