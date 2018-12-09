module ChickenSoftware.PanzerGeneral.Demo

open System
open System.IO
open FSharp.Data
open Xamarin.Forms
open System.Reflection

type TileContext = JsonProvider<"Data//Scenario_Tile.json">
type UnitContext = JsonProvider<"Data//Scenario_Unit.json">
type EquipmentContext = JsonProvider<"Data//Equipment.json">
type ContentData = {TileId: int; ColumnNumber: int; RowNumber: int; UnitId: int option}

type TileImage(tileId:int) = 
          inherit Image()
          member this.TileId = tileId

type TileLayout(tileId:int) =
    inherit AbsoluteLayout()
    member this.TileId = tileId

type TileFrame(tileId:int) =
    inherit Frame()
    member this.TileId = tileId

let tapRecognizer = new TapGestureRecognizer()

let getJson (assembly:Assembly) (fileName:string) =
    let stream = assembly.GetManifestResourceStream(fileName);
    let reader = new StreamReader(stream)
    reader.ReadToEnd()

let getTiles (scenarioId:int) (assembly:Assembly) =
    let json = getJson assembly "scenariotile"
    let scenarioTile = TileContext.Parse(json)
    scenarioTile.Dataroot.ScenarioTile
    |> Array.filter(fun st -> st.ScenarioId = scenarioId)

let getUnits (scenarioId: int) (assembly:Assembly) =
    let json = getJson assembly "scenariounit"
    let unit = UnitContext.Parse(json)
    unit.Dataroot.ScenarioUnit
    |> Array.filter(fun su -> su.ScenarioId = scenarioId)

let getEquipments (assembly:Assembly) = 
    let json = getJson assembly "equipment"
    let unit = UnitContext.Parse(json)
    let equipment = EquipmentContext.Parse(json)
    equipment.Dataroot.Equipment

let createImage path tileId =
    let image = new TileImage(tileId)
    image.Source <- ImageSource.FromResource(path)
    image

let createRectangle columnIndex rowIndex scale =
    let height = 50.0 * scale
    let width = 60.0 * scale
    let yOffsetPlug = 25.0 * scale
    let xOffsetPlug = -15.0 * scale
    let columnIndex' = float columnIndex
    let rowIndex' = float rowIndex
    let yOffset = 
        match columnIndex % 2 = 0 with
        | true -> yOffsetPlug
        | false -> yOffsetPlug + yOffsetPlug
    let xOffset = (columnIndex' * xOffsetPlug) + xOffsetPlug
    let x = xOffset + columnIndex' * width
    let y = yOffset + rowIndex' * height
    let rectangle = new Rectangle(x,y,width,height)
    rectangle
    
let createLayout numberOfTiles scale =
    let width = 60.0 * scale
    let height = 50.0 * scale
    let layout = new AbsoluteLayout()
    layout.WidthRequest <- 12.0 * width
    let rows = numberOfTiles / 20 |> float
    layout.HeightRequest <- rows * height
    layout

let createTileContentData (tile:TileContext.ScenarioTile) (units: UnitContext.ScenarioUnit seq) (equipments: EquipmentContext.Equipment seq) =
    let scenarioUnit = 
        units
        |> Seq.tryFind(fun u -> u.StartingScenarioTileId = tile.ScenarioTileId)
    let unitId = 
        match scenarioUnit with
        | Some u -> 
            let equipment = equipments |> Seq.find(fun e -> e.EquipmentId = u.EquipmentId)
            Some equipment.Icon
        | None -> None

    {TileId = tile.TerrainId; 
    ColumnNumber = tile.ColumnNumber; 
    RowNumber = tile.RowNumber;
    UnitId = unitId}

    
let createLabel (contentData: ContentData) =
    let label = new Label()
    label.FontSize <- 8.0
    label.Text <- contentData.ColumnNumber.ToString() + ":" + contentData.RowNumber.ToString()
    label.InputTransparent <- true
    label

let createTerrainImage (contentData: ContentData) (scale: float) =
    let tileId = contentData.TileId
    let terrainImageLocator = "tacmapdry" + tileId.ToString()
    let image = createImage terrainImageLocator tileId
    image.Scale <- (scale + 0.6)
    image

let createUnitImage (contentData: ContentData) (scale: float) =
    match contentData.UnitId with 
    |Some i -> 
        let path = "tacicons" + i.ToString()
        let image = new Image()
        image.Source <- ImageSource.FromResource(path)
        image.InputTransparent <- true
        image.Scale <- (scale + 0.6)
        Some image
    |None -> None

let createFrame (contentData: ContentData) (scale: float) =
    let frame = new TileFrame(contentData.TileId)
    frame.BackgroundColor <- Color.Transparent
    frame.BorderColor <- Color.Transparent
    let content = createTerrainImage contentData scale 
    frame.Content <- content
    frame.GestureRecognizers.Add(tapRecognizer)
    frame

let createAnotherFrame (contentData: ContentData) (scale: float) =
    let frame = new TileFrame(contentData.TileId)
    frame.BackgroundColor <- Color.Transparent
    frame.BorderColor <- Color.Transparent
    frame.InputTransparent <- true
    let content = createUnitImage contentData scale
    match content.IsSome with
    | true -> frame.Content <- content.Value
    | false -> ()
    frame

let addTileContent (layout:AbsoluteLayout) (contentData: ContentData) (scale: float) =
    let rectangle = createRectangle contentData.ColumnNumber contentData.RowNumber scale
    let frame = createFrame contentData scale
    let anotherFrame = createAnotherFrame contentData scale
    layout.Children.Add(frame,rectangle)
    layout.Children.Add(anotherFrame,rectangle)

let populateSurface assembly =
    let tiles = getTiles 0 assembly
    let units = getUnits 0 assembly
    let equipments = getEquipments assembly
    let numberOfTiles = tiles |> Seq.length
    let scale = 1.5
    let layout = createLayout numberOfTiles scale
    tiles
    |> Array.map(fun t -> createTileContentData t units equipments)
    |> Array.iter(fun cd -> addTileContent layout cd scale)
    let scrollView = new ScrollView()
    scrollView.Orientation <- ScrollOrientation.Both
    scrollView.Content <- layout
    scrollView

type App() as app =
    inherit Application()

    let handleTapEvent (sender:Object) (e:EventArgs) =
        let value = sender.ToString()
        let tileLayout = sender :?> TileFrame
        let tileId = tileLayout.TileId.ToString()
        app.MainPage.DisplayAlert("Tile Pressed", tileId, "OK") |> ignore
        ()

    do
        let assembly = IntrospectionExtensions.GetTypeInfo(typeof<App>).Assembly
        let scrollView = populateSurface assembly
        base.MainPage <- ContentPage(Content = scrollView)
        let tapEventHandler = new EventHandler(handleTapEvent)
        tapRecognizer.Tapped.AddHandler(tapEventHandler)
