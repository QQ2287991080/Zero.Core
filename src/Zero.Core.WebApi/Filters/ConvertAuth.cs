using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.WebApi.Filters
{
    public class ConvertAuth : IControllerModelConvention
    {

        public void Apply(ControllerModel controller)
        {
            //foreach (var item in collection)
            //{

            //}
        }
    }

    public class ConvertAction : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            if (!action.Attributes.Any(a => a is AuthorizeAttribute))
            {
               
            }
        }
    }

}
