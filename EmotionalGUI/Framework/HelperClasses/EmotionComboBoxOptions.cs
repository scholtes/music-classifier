using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Framework
{
    public static class EmotionComboBoxOptions
    {
        public static ObservableCollection<EmotionDTO> GetComboBoxOptions()
        {
            var result = new ObservableCollection<EmotionDTO>();
            result.Add(new EmotionDTO("Joy",1,1));
            result.Add(new EmotionDTO("Anxiety",1,0));
            result.Add(new EmotionDTO("Serenity",0,1));
            result.Add(new EmotionDTO("Anguish",0,0));
            return result;
        }
    }
}
