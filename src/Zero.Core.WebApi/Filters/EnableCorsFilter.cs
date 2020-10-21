using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zero.Core.WebApi.Filters
{
    public class EnableCorsFilter : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            var controllers = application.Controllers;
            foreach (var item in controllers)
            {
                var cors = item.Attributes.FirstOrDefault(f => f.GetType().IsDefined(typeof(EnableCorsAttribute), true));
                if (cors == null)
                { 
                 //item.Attributes
                }
            }
                
            
        }
    }
}
