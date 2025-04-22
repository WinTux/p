namespace ejemplo.Controllers
{
    public class SolicitudDeToken
    {
        public string userId {  get; set; }
        public string email { get; set; }
        public ClaimPersonalizados claimPersonalizado { get; set; }
    }
    public class ClaimPersonalizados { 
        public bool Admin { get; set; }
    }
}