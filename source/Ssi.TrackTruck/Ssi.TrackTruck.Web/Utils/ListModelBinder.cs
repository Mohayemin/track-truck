using System;
using System.Linq;
using System.Web.Mvc;

namespace Ssi.TrackTruck.Web.Utils
{
    /// <summary>
    /// http://stackoverflow.com/a/9508310/887149 <br/>
    /// 
    /// Usage: <![CDATA[ModelBinders.Binders.Add(typeof(IList<string>), new ListModelBinder<string>());]]>
    /// </summary>
    public class ListModelBinder<T> : DefaultModelBinder where T : class
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value == null || string.IsNullOrEmpty(value.AttemptedValue))
            {
                return null;
            }

            return value
                .AttemptedValue
                .Split(',')
                .Select(s => Convert.ChangeType(s, typeof(T)) as T)
                .ToList();
        }
    }
}