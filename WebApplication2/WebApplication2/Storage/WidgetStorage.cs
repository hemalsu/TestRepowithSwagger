using System;
using WebApplication2.Interfaces;
using WebApplication2.Model;

namespace WebApplication2.Storage
{
    public  class WidgetStorage : IWidgetStorage
    {
        public  List<WidgetDTO> _widgets = new List<WidgetDTO>();
        public  WidgetDTO Add(WidgetDTO widget)
        {
            WidgetDTO newDTO = new WidgetDTO
            {
                Id = Guid.NewGuid(),
                Name = widget.Name
            };
            _widgets.Add(newDTO);
            return newDTO;
        }

        public  List<WidgetDTO> GetAllWidgets()
        {
            return _widgets;
        }
        public  WidgetDTO? GetWidgetByID(Guid ID)
        {
           
            return _widgets.FirstOrDefault(w => w.Id == ID);
        }
    }
}
