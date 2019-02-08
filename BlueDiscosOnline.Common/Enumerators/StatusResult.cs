using System.ComponentModel;

namespace BlueDiscosOnline.Common.Enumerators
{
    public enum StatusResult
    {
        [Description("Success")]
        Success = 0,
        [Description("Info")]
        Info = 1,
        [Description("Warning")]
        Warning = 2,
        [Description("Danger")]
        Danger = 3,
        [Description("Critical")]
        Critical = 4
    }
}
