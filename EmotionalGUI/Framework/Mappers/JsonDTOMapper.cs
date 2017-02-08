using System.Web.Script.Serialization;

namespace Framework
{
    public static class JsonDTOMapper
    {
        public static JsonDTO getJsonDTO(string json)
        {
            JsonDTO result = new JsonDTO();
            result = new JavaScriptSerializer().Deserialize<JsonDTO>(json);
            return result;
        }
    }
}
