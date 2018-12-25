
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

type App() as app =
    inherit Application()

    let initializeScenario =
        let assembly = IntrospectionExtensions.GetTypeInfo(typeof<App>).Assembly
        let board = createBoard assembly 0 LandCondition.Dry 
        let humanPlayer = Nation.Axis AxisNation.German
        let currentPlayer = Player.Human humanPlayer
        let scenario = {CurrentPlayer=currentPlayer; Board = board}
        scenario

    let mutable scenario = initializeScenario

    let updateModel (tileFrame:TileFrame) =
        let tile = tileFrame.Tile
        let baseTile = getBaseTile tile
        let tileId = baseTile.Id.ToString()
        let humanPlayer = Player.Human (Nation.Axis AxisNation.German)
        let computerPlayer = Player.Computer (Nation.Allied AlliedNation.Poland)
        match scenario.CurrentPlayer with 
        | Player.Human h -> scenario <- {scenario with CurrentPlayer=computerPlayer}
        | Player.Computer c -> scenario <- {scenario with CurrentPlayer=humanPlayer}

        let playerDesc = scenario.CurrentPlayer.ToString()
        app.MainPage.DisplayAlert("Tile Pressed", playerDesc, "OK") |> ignore
        

    let updateView =
        let scrollView = createSurface (scenario.Board)
        app.MainPage <- ContentPage(Content = scrollView)

    let handleTapEvent (sender:Object) (e:EventArgs) =
        let tileFrame = sender :?> TileFrame
        updateModel tileFrame 
        updateView

    do 
        let scrollView = createSurface (scenario.Board)
        base.MainPage <- ContentPage(Content = scrollView)
        let tapEventHandler = new EventHandler(handleTapEvent)
        tapRecognizer.Tapped.AddHandler(tapEventHandler)