using BookLibrary.Resources.Constants;

namespace BookLibrary.Resources.Fonts;

public static class FontsRegistration
{
    public static MauiAppBuilder RegisterFonts(this MauiAppBuilder builder)
    {
        builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", AppConstants.OpenSansRegular);
            fonts.AddFont("OpenSans-Semibold.ttf", AppConstants.OpenSansSemibold);
            fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", AppConstants.FontAwesomeBrandsRegular400);
            fonts.AddFont("Font Awesome 6 Duotone-Solid-900.otf", AppConstants.FontAwesomeDuotoneSolid900);
            fonts.AddFont("Font Awesome 6 Pro-Light-300.otf", AppConstants.FontAwesomeProLight300);
            fonts.AddFont("Font Awesome 6 Pro-Regular-400.otf", AppConstants.FontAwesomeProRegular400);
            fonts.AddFont("Font Awesome 6 Pro-Solid-900.otf", AppConstants.FontAwesomeProSolid900);
            fonts.AddFont("Font Awesome 6 Pro-Thin-100.otf", AppConstants.FontAwesomeProThin100);
            fonts.AddFont("Font Awesome 6 Sharp-Light-300.otf", AppConstants.FontAwesomeSharpLight300);
            fonts.AddFont("Font Awesome 6 Sharp-Regular-400.otf", AppConstants.FontAwesomeSharpRegular400);
            fonts.AddFont("Font Awesome 6 Sharp-Solid-900.otf", AppConstants.FontAwesomeSharpSolid900);
            fonts.AddFont("Font Awesome 6 Sharp-Thin-100.otf", AppConstants.FontAwesomeSharpThin100);
            fonts.AddFont("Lato-Black.ttf", AppConstants.LatoBlack);
            fonts.AddFont("Lato-BlackItalic.ttf", AppConstants.LatoBlackItalic);
            fonts.AddFont("Lato-Bold.ttf", AppConstants.LatoBold);
            fonts.AddFont("Lato-BoldItalic.ttf", AppConstants.LatoBoldItalic);
            fonts.AddFont("Lato-Hairline.ttf", AppConstants.LatoHairline);
            fonts.AddFont("Lato-HairlineItalic.ttf", AppConstants.LatoHairlineItalic);
            fonts.AddFont("Lato-Heavy.ttf", AppConstants.LatoHeavy);
            fonts.AddFont("Lato-HeavyItalic.ttf", AppConstants.LatoHeavyItalic);
            fonts.AddFont("Lato-Italic.ttf", AppConstants.LatoItalic);
            fonts.AddFont("Lato-Light.ttf", AppConstants.LatoLight);
            fonts.AddFont("Lato-LightItalic.ttf", AppConstants.LatoLightItalic);
            fonts.AddFont("Lato-Medium.ttf", AppConstants.LatoMedium);
            fonts.AddFont("Lato-MediumItalic.ttf", AppConstants.LatoMediumItalic);
            fonts.AddFont("Lato-Regular.ttf", AppConstants.LatoRegular);
            fonts.AddFont("Lato-Semibold.ttf", AppConstants.LatoSemibold);
            fonts.AddFont("Lato-SemiboldItalic.ttf", AppConstants.LatoSemiboldItalic);
            fonts.AddFont("Lato-Thin.ttf", AppConstants.LatoThin);
            fonts.AddFont("Lato-ThinItalic.ttf", AppConstants.LatoThinItalic);
        });
        return builder;
    }
}