namespace LojaDeGames.Security
{
    public class Settings
    {
        private static string secret = "8a7c7df4893d17a046a542d327da46c51587ce006ccd8223b6b1f19d34d22d77";

        public static string Secret { get => secret; set => secret = value; }
    }
}
