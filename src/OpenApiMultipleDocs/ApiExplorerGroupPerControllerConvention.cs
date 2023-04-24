using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace MultipleDocs
{
    public class ApiExplorerGroupPerControllerConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            controller.ApiExplorer.GroupName = controller.ControllerName;
        }
    }
}