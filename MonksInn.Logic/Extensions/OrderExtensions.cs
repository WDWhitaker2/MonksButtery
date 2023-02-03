using MonksInn.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonksInn.Logic.Extensions
{
    public static class OrderExtensions
    {
        //todo define this for dispatch screen
       public static string DispatchDetails(this Order order)
        {

            var model = "";

            model = "Ready to pour";
            model = "Ready to deliver";
            model = "Ready to be fetched";


            return model;
        }
    }
}
