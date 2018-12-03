namespace ChickenSoftware.PanzerGeneral

open System
open Xamarin.Forms
open System.Reflection

type App() as app =
    inherit Application()

    let handleTapEvent (sender:Object) (e:EventArgs) =
        //let value = sender.ToString()
        //let tileLayout = sender :?> Demo.TileFrame
        //let tileId = tileLayout.TileId.ToString()
        //app.MainPage.DisplayAlert("Tile Pressed", tileId, "OK") |> ignore
        ()

    do
        let assembly = IntrospectionExtensions.GetTypeInfo(typeof<App>).Assembly
        let scrollView = Surface.populateSurface assembly 0
        base.MainPage <- ContentPage(Content = scrollView)
        let tapEventHandler = new EventHandler(handleTapEvent)
        Demo.tapRecognizer.Tapped.AddHandler(tapEventHandler)
  
