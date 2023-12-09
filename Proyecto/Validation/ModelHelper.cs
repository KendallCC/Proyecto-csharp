using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Proyecto.Validation
{
    public static class ModelHelper
    {
        public static string GetModelError(System.Web.Mvc.ModelStateDictionary pModelStateDictionary)
        {
            StringBuilder errors = new StringBuilder();
            if (!pModelStateDictionary.IsValid)
            {
                var values = pModelStateDictionary.Values;

                foreach (var item in values)
                {
                    foreach (var error in item.Errors)
                    {
                        errors.Append($"{error.ErrorMessage}");
                    }
                }
                return $"Error de validacion {errors.ToString()}";
            }
            else
                return "";
        }
    }
}