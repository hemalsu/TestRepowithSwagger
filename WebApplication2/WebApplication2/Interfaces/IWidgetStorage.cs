using WebApplication2.Model;

namespace WebApplication2.Interfaces
{

    public interface IWidgetStorage
    {
        WidgetDTO  Add (WidgetDTO widget);
        List<WidgetDTO> GetAllWidgets();
        WidgetDTO? GetWidgetByID(Guid ID);
    }
}
