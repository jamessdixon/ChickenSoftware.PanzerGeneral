
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

    let scale = 1.5

    let initializeScenario =
        let assembly = IntrospectionExtensions.GetTypeInfo(typeof<App>).Assembly
        let board = createBoard assembly 0 LandCondition.Dry 
        let humanPlayer = Player.Human (Nation.Axis AxisNation.German)
        let computerPlayer = Player.Computer (Nation.Allied AlliedNation.Poland)

        {PlayerOne = humanPlayer;
        PlayerTwo = computerPlayer;
        CurrentPlayer=humanPlayer; 
        CurrentLandCondition = LandCondition.Dry;
        Board = board; 
        BoardMode = BoardMode.Earth;
        ActiveTile = None; 
        MovableTiles = None; 
        AttackableTiles=None}

    let mutable scenario = initializeScenario

    let updateFromTap (tileFrame:TileFrame) =
        let tile = tileFrame.Tile
        let baseTile = getBaseTile tile
        let tileId = baseTile.Id.ToString()

        let contentPage = app.MainPage :?> ContentPage
        let grid = contentPage.Content :?> Grid
        let scrollView = grid.Children.[3] :?> ScrollView
        let layout = scrollView.Content :?> AbsoluteLayout
        //explodeHex layout tileFrame scale

        match scenario.ActiveTile with
        | Some t -> 
            match scenario.BoardMode with 
            | BoardMode.Earth ->
                match baseTile.EarthUnit with 
                | Some eu ->
                    scenario <- {scenario with ActiveTile=None}
                    deactivateHexes layout
                | None -> ()
            | BoardMode.Sky ->
                match baseTile.SkyUnit with 
                | Some su ->
                    scenario <- {scenario with ActiveTile=None}
                    deactivateHexes layout
                | None -> ()
                //(unit:Unit) (baseTile:BaseTile) (landCondition: LandCondition) (board: Tile array) =
        | None -> 
            match scenario.BoardMode with 
            | BoardMode.Earth ->
                match baseTile.EarthUnit with 
                | Some eu ->
                    scenario <- {scenario with ActiveTile=Some tile}
                    activateHex layout tileFrame scale
                    let movableTiles = getMovableTiles eu tile scenario.CurrentLandCondition scenario.Board
                    movableTiles 
                    |> Array.map(fun t -> getTileFrame layout t.Id)
                    |> Array.filter(fun tf -> tf.IsSome)
                    |> Array.iter(fun tf -> activateHex layout (tf.Value) scale) 
                | None -> ()
            | BoardMode.Sky ->
                match baseTile.SkyUnit with 
                | Some su ->
                    scenario <- {scenario with ActiveTile=Some tile}
                    activateHex layout tileFrame scale
                | None -> ()

    let handleTapEvent (sender:Object) (e:EventArgs) =
        let tileFrame = sender :?> TileFrame
        updateFromTap tileFrame

    let handleTurnEndEvent (sender:Object) (e:EventArgs) =
        match scenario.CurrentPlayer with 
        | Player.Human h -> scenario <- {scenario with CurrentPlayer=scenario.PlayerTwo}
        | Player.Computer c -> scenario <- {scenario with CurrentPlayer=scenario.PlayerOne}
        let playerDesc = scenario.CurrentPlayer.ToString()
        app.MainPage.DisplayAlert("Next Turn", playerDesc, "OK") |> ignore

    do 
        let scrollView = createPlayingSurface (scenario.Board) (scale)
        let surfaceGrid = createSurfaceGrid scrollView handleTurnEndEvent
        base.MainPage <- ContentPage(Content = surfaceGrid)
        NavigationPage.SetHasNavigationBar(base.MainPage, false)
        let tapEventHandler = new EventHandler(handleTapEvent)
        tapRecognizer.Tapped.AddHandler(tapEventHandler)