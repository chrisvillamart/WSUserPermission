namespace WSUserPermission.Entities
{
    public class PermissionResponse
    { 
        public PermissionResponse(int code, string message)
        {
            this.code = code;
            this.message = message;
        }

        public PermissionResponse() { }
        public int code { get; set; } = 0;
        public string message { get; set; } = "Operación realizada con éxito";
        public dynamic data { get; set; }
    }
}
