namespace WebApi.Domain.Service.Communication
{
    public class jwtDto
    {
        public string token { get; set; }

        public jwtDto() { }

        public jwtDto(string token) { 
           this.token = token;
        }


    }
}
