namespace ChickenSoftware.PanzerGeneral

open System
open Xamarin.Forms
open Fabulous.Core
open System.Reflection
open Fabulous.DynamicViews

open UnitMapper
open Hex
open Tile
open Board
open Terrain
open Surface
open UnitMapper
open SupportData

type Msg = | Pressed

type Model = { Pressed: bool }


type App() as app =
    inherit Application()

    let init() = { Pressed = false }


    let handleTapEvent (sender:Object) (e:EventArgs) =
        let tileFrame = sender :?> TileFrame
        let tile = tileFrame.Tile
        let baseTile = getBaseTile tile
        let tileId = baseTile.Id.ToString()
        app.MainPage.DisplayAlert("Tile Pressed", tileId, "OK") |> ignore
        ()
    do
        let assembly = IntrospectionExtensions.GetTypeInfo(typeof<App>).Assembly
        let scrollView = populateSurface assembly 0
        base.MainPage <- ContentPage(Content = scrollView)
        let tapEventHandler = new EventHandler(handleTapEvent)
        tapRecognizer.Tapped.AddHandler(tapEventHandler)

