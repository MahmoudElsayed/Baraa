using System;
using System.ComponentModel;
using System.Linq;

namespace Baraa.BLL
{
    public enum Language
    {
        Arabic, English
    }
    public enum ControllerEnum
    {
        Permission = 1,
        Role = 2,
        Cities = 3,
        Countries = 4,
    }
    public enum ActionEnum
    {
        View = 1,
        Insert = 2,
        Update = 3,
        Delete = 4,

    }
    public enum Gender
    {
        [Description("Male")]
        Male = 0,
        [Description("Female")]
        Female = 1,
    }
    public enum StatusEnable
    {
        [Description("Enable")]
        Enable = 0,
        [Description("Disable")]
        Disable = 1,
    }
    public enum ResultMessageType
    {
        success,
        error,
        info,
        warning,
        valid
    }
}