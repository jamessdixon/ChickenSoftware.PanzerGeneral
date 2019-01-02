
module ChickenSoftware.PanzerGeneral.Surface

open Hex
open Tile
open Board
open System
open Terrain
open Scenario
open Xamarin.Forms
open System.Reflection
open MovementCalculator

type GreenFrame() =
    inherit Frame()
    
type TapAction =
| ActivateUnit
| DeactivateUnit
| MoveUnit 
| UnitAttack

let scale = 1.5

let createLayout numberOfTiles scale =
    let width = 60.0 * scale
    let height = 50.0 * scale
    let layout = new AbsoluteLayout()
    layout.WidthRequest <- 12.0 * width
    let rows = numberOfTiles / 20 |> float
    layout.HeightRequest <- rows * height
    layout

let createPlayingSurface (board:Tile array) (scale: float)=
    let numberOfTiles = board |> Seq.length
    let layout = createLayout numberOfTiles scale
    board 
    |> Array.iter(fun t -> createHex layout t scale)
    let scrollView = new ScrollView()
    scrollView.Orientation <- ScrollOrientation.Both
    scrollView.Content <- layout
    scrollView

let createSurfaceGrid (scrollView:ScrollView) handleEndTurnEvent =
    let grid = Grid(VerticalOptions=LayoutOptions.FillAndExpand,
                    HorizontalOptions=LayoutOptions.FillAndExpand,
                    ColumnSpacing=0.0,RowSpacing=0.0,
                    BackgroundColor=Color.Gray)
    let headerBoxView = BoxView(BackgroundColor=Color.Gray)
    let endTurnButton = Button(BackgroundColor=Color.Black, Text="End Turn")
    let endTurnHandler = new EventHandler(handleEndTurnEvent)
    endTurnButton.Clicked.AddHandler(endTurnHandler)
    let resetGameButton = Button(BackgroundColor=Color.Black, Text = "Reset Game")
    grid.RowDefinitions.Add(RowDefinition(Height=GridLength(40.0,GridUnitType.Absolute)))
    grid.RowDefinitions.Add(RowDefinition(Height=GridLength(50.0,GridUnitType.Absolute)))
    grid.ColumnDefinitions.Add(ColumnDefinition(Width=GridLength(1.0,GridUnitType.Star)))
    grid.ColumnDefinitions.Add(ColumnDefinition(Width=GridLength(1.0,GridUnitType.Star)))
    Grid.SetRow(headerBoxView,0)
    Grid.SetRow(resetGameButton,1)
    Grid.SetRow(endTurnButton,1)
    Grid.SetRow(scrollView,2)
    Grid.SetColumnSpan(headerBoxView,2)
    Grid.SetColumnSpan(scrollView,2)
    Grid.SetColumn(resetGameButton,0)
    Grid.SetColumn(endTurnButton,1)
    grid.Children.Add(headerBoxView)
    grid.Children.Add(resetGameButton)
    grid.Children.Add(endTurnButton)
    grid.Children.Add(scrollView)
    grid

let initializeSurface (scenario: Scenario) handleEndTurnEvent =
    let scrollView = createPlayingSurface scenario.Board scale
    createSurfaceGrid scrollView handleEndTurnEvent

let explodeHex (layout:AbsoluteLayout) (tileFrame:TileFrame) =
    let image = new Image()
    image.Source <- ImageSource.FromResource("explode0")
    let x = tileFrame.X
    let y = tileFrame.Y
    let height = 50.0 * scale
    let width = 60.0 * scale
    let rectangle = new Rectangle(x,y,width,height)

    let frame = new Frame()
    frame.BackgroundColor <- Color.Transparent
    frame.BorderColor <- Color.Transparent
    frame.InputTransparent <- true
    frame.Content <- image
    layout.Children.Add(frame, rectangle)

    let mutable index = 1
    let callback = new System.Func<bool>(fun _ -> 
        match index with 
        | 1 -> image.Source <- ImageSource.FromResource("explode1"); index <- 2
        | 2 -> image.Source <- ImageSource.FromResource("explode2"); index <- 3
        | 3 -> image.Source <- ImageSource.FromResource("explode3"); index <- 4
        | 4 -> image.Source <- ImageSource.FromResource("explode4"); index <- 5
        | 5 -> image.Source <- ImageSource.FromResource("explode5"); index <- 6
        | 6 -> () //should remove frame here
        | _ -> ()
        true)
    Device.StartTimer(System.TimeSpan.FromSeconds(0.25),callback)

let activateHex (layout:AbsoluteLayout) (tileFrame:TileFrame)  =
    let image = new Image()
    image.Source <- ImageSource.FromResource("green")
    image.Scale <- (scale + 0.6)

    let x = tileFrame.X
    let y = tileFrame.Y
    let height = 50.0 * scale
    let width = 60.0 * scale
    let rectangle = new Rectangle(x,y,width,height)

    let frame = new GreenFrame()
    frame.BackgroundColor <- Color.Transparent
    frame.BorderColor <- Color.Transparent
    frame.InputTransparent <- true
    frame.Content <- image
    layout.Children.Add(frame, rectangle)
    
let deactivateHexes (layout:AbsoluteLayout) =
    layout.Children
    |> Seq.filter(fun c -> c :? GreenFrame)
    |> Seq.toArray
    |> Array.iter(fun f -> layout.Children.Remove(f) |> ignore)

let updateLayout (layout:AbsoluteLayout) (tile:Tile) =
    let baseTile = getBaseTile tile
    let oldTileFrame =
        layout.Children
        |> Seq.filter(fun c -> c :? TileFrame)
        |> Seq.map(fun v -> v :?> TileFrame)
        |> Seq.map(fun tf -> tf, getTileId tf.Tile)
        |> Seq.filter(fun (tf,id) -> id = baseTile.Id)
        |> Seq.map(fun (tf,id) -> tf)
        |> Seq.head
    layout.Children.Remove(oldTileFrame) |> ignore
    createHex layout tile scale
    ()

let getTileFrame (layout:AbsoluteLayout) (id: int)=
    let tuple = 
        layout.Children
        |> Seq.filter(fun c -> c :? TileFrame)
        |> Seq.map(fun tf -> tf :?> TileFrame)
        |> Seq.map(fun tf -> tf, tf.Tile)
        |> Seq.map(fun (tf,t) -> tf, getBaseTile t)
        |> Seq.tryFind(fun (tf,bt) -> bt.Id = id)
    match tuple with 
    | Some t -> Some (fst t)
    | None -> None

let activateUnit (scenario: Scenario) (layout) (tileFrame:TileFrame) =
    let tile = tileFrame.Tile
    let baseTile = getBaseTile tile
    match scenario.BoardMode with 
    | BoardMode.Earth ->
        match baseTile.EarthUnit with 
        | Some eu ->
            activateHex layout tileFrame 
            let movableTiles = getMovableTiles scenario.Board scenario.CurrentLandCondition tile eu   
            movableTiles 
            |> Array.distinct
            |> Array.map(fun t -> t,getBaseTile t)
            |> Array.map(fun (t,bt) -> getTileFrame layout bt.Id)
            |> Array.filter(fun tf -> tf.IsSome)
            |> Array.iter(fun tf -> activateHex layout (tf.Value)) 
            {scenario with ActiveTile = Some tile; MovableTiles = Some movableTiles}
        | None -> scenario
    | BoardMode.Sky ->
        match baseTile.SkyUnit with 
        | Some su ->
            activateHex layout tileFrame
            {scenario with ActiveTile = Some tile}
        | None -> scenario

let deactivateUnit (scenario: Scenario) (layout) baseTile = 
    match scenario.BoardMode with  
    | BoardMode.Earth ->
        match baseTile.EarthUnit with 
        | Some eu ->
            deactivateHexes layout
            {scenario with ActiveTile = None}
        | None -> scenario
    | BoardMode.Sky ->
        match baseTile.SkyUnit with 
        | Some su ->
            deactivateHexes layout
            {scenario with ActiveTile = None}
        | None -> scenario

let handleUnitMovement (scenario: Scenario) (layout) (targetTile:Tile) =
    let updatedScenario = moveUnit scenario targetTile
    updateLayout layout scenario.ActiveTile.Value
    updateLayout layout updatedScenario.ActiveTile.Value
    updatedScenario

let handleUnitAttack (scenario: Scenario) =
    //explodeHex layout tileFrame scale
    scenario

let getTapAction (scenario: Scenario) (tappedTile: Tile) =
    match scenario.ActiveTile with
    | Some t ->
        match t = tappedTile with 
        | true -> DeactivateUnit
        | false ->
            match scenario.MovableTiles.IsSome, scenario.AttackableTiles.IsSome with
            | true, true -> 
                let moving = scenario.MovableTiles.Value |> Array.contains(tappedTile) 
                let attacking = scenario.AttackableTiles.Value |> Array.contains(tappedTile) 
                match moving, attacking with 
                | true, _ -> MoveUnit
                | false, true -> UnitAttack
                | false, false -> DeactivateUnit
            | true, false -> 
                let moving = scenario.MovableTiles.Value |> Array.contains(tappedTile) 
                match moving with 
                | true -> MoveUnit
                | false -> DeactivateUnit 
            | false, true -> 
                let attacking = scenario.AttackableTiles.Value |> Array.contains(tappedTile) 
                match attacking with 
                | true -> UnitAttack
                | false -> DeactivateUnit 
            | false, false -> DeactivateUnit
    | None -> ActivateUnit

let updateSurfaceForTap (contentPage: ContentPage) (scenario: Scenario) (tileFrame:TileFrame) =
    let tile = tileFrame.Tile
    let baseTile = getBaseTile tile
    let tileId = baseTile.Id.ToString()
    let grid = contentPage.Content :?> Grid
    let scrollView = grid.Children.[3] :?> ScrollView
    let layout = scrollView.Content :?> AbsoluteLayout
    let tapAction = getTapAction scenario tile

    match tapAction with 
    | ActivateUnit -> activateUnit scenario layout tileFrame
    | DeactivateUnit -> deactivateUnit scenario layout baseTile
    | MoveUnit -> handleUnitMovement scenario layout tile
    | UnitAttack -> handleUnitAttack scenario



