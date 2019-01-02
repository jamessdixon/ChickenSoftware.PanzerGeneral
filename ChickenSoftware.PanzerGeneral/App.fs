
namespace ChickenSoftware.PanzerGeneral

open System
open Xamarin.Forms
open Fabulous.Core
open System.Reflection
open Fabulous.DynamicViews

open Hex
open Tile
open Board
open Nation
open Terrain
open Surface
open Scenario
open UnitMapper
open SupportData
open MovementCalculator

type App() as app =
    inherit Application()

    let assembly = IntrospectionExtensions.GetTypeInfo(typeof<App>).Assembly
    let mutable scenario = initializeScenario assembly

    let handleTapEvent (sender:Object) (e:EventArgs) =
        let tileFrame = sender :?> TileFrame
        let contentPage = app.MainPage :?> ContentPage
        scenario <- updateSurfaceForTap contentPage scenario tileFrame

    let handleTurnEndEvent (sender:Object) (e:EventArgs) =
        scenario <- updateScenarioForPlayer scenario
        let playerDesc = scenario.CurrentPlayer.ToString()
        app.MainPage.DisplayAlert("Next Turn", playerDesc, "OK") |> ignore

    let handleScenarioEndEvent (sender:Object) (e:EventArgs) =
        scenario <- initializeScenario assembly

    do 
        let surface = initializeSurface scenario handleTurnEndEvent
        base.MainPage <- ContentPage(Content = surface)
        NavigationPage.SetHasNavigationBar(base.MainPage, false)
        let tapEventHandler = new EventHandler(handleTapEvent)
        tapRecognizer.Tapped.AddHandler(tapEventHandler)


