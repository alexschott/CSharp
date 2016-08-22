using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Web.Mvc;

namespace Aloe.Helper
{
    public static class UI
    {

        public static MvcHtmlString DealStatus(int Value)
        {
            return MvcHtmlString.Create( ((Aloe.Helper.Status)Value).ToString());
        }

        public static MvcHtmlString DealDateFormat(DateTime Value)
        {
            return MvcHtmlString.Create(Convert.ToString(string.Format("{0:MM/dd/yyyy HH:mm}", Value)));
        }

    }
}