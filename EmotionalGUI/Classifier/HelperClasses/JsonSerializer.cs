using System.Web.Script.Serialization;

namespace Classifier
{
    public static class JsonSerializer
    {
        public static string serializeToJson(EmotionSpaceDTOList dto)
        {
            return new JavaScriptSerializer().Serialize(dto);
        }
    }
}
