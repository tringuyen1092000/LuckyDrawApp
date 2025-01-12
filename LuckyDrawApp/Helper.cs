namespace LuckyDrawApp
{
   public static class Helper
   {
      // Sound effect for spinning luck draw. Only accept .wav file (.mp3/.mp4 will not work)
      public const string SPINNING_EFFECT_PATH = "./Sounds/SpinningEffect.mp3";
      public const string AFTER_SPIN_EFFECT_PATH = "./Sounds/AfterSpinEffect.mp3";
      // Background image for lucky draw application
      public const string LUCKY_DRAW_BACKGROUND_IMAGE_PATH = "./Images/LuckyDrawBG.jpg";
      // Excel file path storing lucky number list
      public const string LUCKY_NUMBER_LIST = "./Data/GuestNumbers";
      public const string CSV_EXTENSION = ".csv";
      public const string XLSX_EXTENSION = ".xlsx";
   }
}
