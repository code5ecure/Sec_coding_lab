
using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializationSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SerializationController : ControllerBase
    {
        [HttpPost("upload-binary")]
        [Consumes("application/octet-stream")]
        public IActionResult UploadBinary()
        {
            var formatter = new BinaryFormatter();
            using var stream = Request.Body;
            var userProfile = (UserProfile)formatter.Deserialize(stream);
            if (userProfile.Name.Length > 50)
                return BadRequest("Invalid name");
            return Ok($"Hello {userProfile.Name}, {userProfile.Bio}");
        }

        [HttpPost("upload-json")]
        [Consumes("application/json")]
        public IActionResult UploadJson([FromBody] string jsonPayload)
        {
            var profile = JsonConvert.DeserializeObject<UserProfile>(jsonPayload);
            if (profile.Name.Length > 50) 
                return BadRequest("Invalid name");
            return Ok($"Hello {profile.Name}, {profile.Bio}");
        }

        [HttpPost("upload-xml")]
        [Consumes("application/xml")]
        public IActionResult UploadXml()
        {
            var serializer = new XmlSerializer(typeof(UserProfile));
            using var stream = Request.Body;
            var profile = (UserProfile)serializer.Deserialize(stream);
            if (profile.Name.Length > 50) 
                return BadRequest("Invalid name");
            return Ok($"Hello {profile.Name}, {profile.Bio}");
        }

        [HttpPost("upload-protobuf")]
        [Consumes("application/x-protobuf")]
        public IActionResult UploadProtobuf()
        {
            using var stream = Request.Body;
            var profile = UserProfile.Parser.ParseFrom(stream);
            if (profile.Name.Length > 50) 
                return BadRequest("Invalid name");
            return Ok($"Hello {profile.Name}, {profile.Bio}");
        }
    }
}
