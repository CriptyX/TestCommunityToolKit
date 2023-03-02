using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui; 
using Maui.GoogleMaps.Hosting;


namespace TestCommunityToolKit;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>() 
            .UseMauiCommunityToolkit().UseMauiCommunityToolkitCore() 
            
            .RegisterServices();
#if ANDROID


        builder.UseGoogleMaps();
#elif IOS
        

            builder.UseGoogleMaps("No-KEY" );
#endif
        HndlerUtility.ModifyEntry();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
    private static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
      //   mauiAppBuilder.Services.AddLocalization();

        return mauiAppBuilder;
    }
    public static class HndlerUtility
    {
        public static void ModifyEntry()
        {
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("CustomEntry", (handler, view) =>
            {
                if (view is BorderlessEntry)
                {
#if ANDROID
                    handler.PlatformView.Background = null;
                    handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif IOS || MACCATALYST
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                handler.PlatformView.Layer.BorderWidth= 0;
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif WINDOWS
                            handler.PlatformView.BorderThickness= new Microsoft.Ui.Xaml.Thickness(0)
#endif
                }

            });
        }
    }
    public class BorderlessEntry : Entry
    {
        public BorderlessEntry()
        {
        }
    }
}
