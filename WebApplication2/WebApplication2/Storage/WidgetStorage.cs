using System;
using WebApplication2.Model;

namespace WebApplication2.Storage
{
    public static class WidgetStorage
    {
        public static List<WidgetDTO> _widgets = new List<WidgetDTO>();
        public static WidgetDTO Add(WidgetDTO widget)
        {
            WidgetDTO newDTO = new WidgetDTO
            {
                Id = Guid.NewGuid(),
                Name = widget.Name
            };
            _widgets.Add(newDTO);
            return newDTO;
        }

        public static List<WidgetDTO> GetAllWidgets()
        {
            return _widgets;
        }
        public static WidgetDTO? getWidgetByID(Guid ID)
        {
           
            return _widgets.FirstOrDefault(w => w.Id == ID);
        }
    }
}
