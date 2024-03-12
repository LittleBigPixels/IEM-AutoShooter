using UnityEngine;

namespace Colors
{
	public static class Rainbow
	{
		public static Color White;
		public static Color Black;
		public static Color Slate;
		public static Color SlateDark;
		public static Color SlateLight;
		public static Color Red;
		public static Color Coral;
		public static Color Gold;
		public static Color Lime;
		public static Color Green;
		public static Color Aqua;
		public static Color Skyblue;
		public static Color Blue;
		public static Color Indigo;
		public static Color Lavender;
		public static Color Natik;
		public static Color Pink;
		public static Color Magenta;

		static Rainbow()
		{
			ColorUtility.TryParseHtmlString("#FFFFFF", out White);
			ColorUtility.TryParseHtmlString("#000000", out Black);
			ColorUtility.TryParseHtmlString("#222428", out Slate);
			ColorUtility.TryParseHtmlString("#0E0F10", out SlateDark);
			ColorUtility.TryParseHtmlString("#35373D", out SlateLight);
			ColorUtility.TryParseHtmlString("#E71A00", out Red);
			ColorUtility.TryParseHtmlString("#F87B4C", out Coral);
			ColorUtility.TryParseHtmlString("#FFBC00", out Gold);
			ColorUtility.TryParseHtmlString("#B6E71D", out Lime);
			ColorUtility.TryParseHtmlString("#29B92C", out Green);
			ColorUtility.TryParseHtmlString("#5FFAA5", out Aqua);
			ColorUtility.TryParseHtmlString("#5CD2FF", out Skyblue);
			ColorUtility.TryParseHtmlString("#1738C0", out Blue);
			ColorUtility.TryParseHtmlString("#00253C", out Indigo);
			ColorUtility.TryParseHtmlString("#A88DDB", out Lavender);
			ColorUtility.TryParseHtmlString("#8B2DC5", out Natik);
			ColorUtility.TryParseHtmlString("#F852AA", out Pink);
			ColorUtility.TryParseHtmlString("#FF0066", out Magenta);
		}
	}
}
