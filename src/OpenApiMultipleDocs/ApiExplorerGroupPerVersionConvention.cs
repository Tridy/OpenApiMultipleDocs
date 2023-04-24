using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace MultipleDocs
{
    public class ApiExplorerGroupPerVersionConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            controller.ApiExplorer.GroupName = controller.ControllerName;
        }
    }
}