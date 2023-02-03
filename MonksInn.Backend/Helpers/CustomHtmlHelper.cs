using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonksInn.Backend.Helpers
{
    public static class CustomHtmlHelper
    {

        public static string SetActiveNav(object viewData, string valueForActive)
        {
            if(viewData != null)
            {
                if(viewData.ToString().ToLower() == valueForActive.ToLower())
                {
                    return "active";
                }
            }

            return "";
        }
    }
}
