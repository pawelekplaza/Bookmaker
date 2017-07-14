using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class Extensions
{
    public static string GetAuthEmail(this ControllerBase controller)
    {
        try
        {
            return controller.User.Claims.Where(v => v.ToString().Contains("nameidentifier:")).FirstOrDefault().Value;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
