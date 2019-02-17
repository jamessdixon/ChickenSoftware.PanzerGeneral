namespace ChickenSoftware.PanzerGeneral

open System.IO
open FSharp.Data
open Xamarin.Forms
open System.Reflection

type TileContext = JsonProvider<"Data//Scenario_Tile.json">
type UnitContext = JsonProvider<"Data//Scenario_Unit.json">
type EquipmentContext = JsonProvider<"Data//Equipment.json">
type ContentData = {TileId: int; ColumnNumber: int; RowNumber: int; UnitId: int option}

type App() =
    inherit Application()

    let getTiles (scenarioId:int) =
        let assembly = IntrospectionExtensions.GetTypeInfo(typeof<App>).Assembly
        let stream = assembly.GetManifestResourceStream("scenariotile");
        let reader = new StreamReader(stream)
        let json = reader.ReadToEnd()
        let scenarioTile = TileContext.Parse(json)
        scenarioTile.Dataroot.ScenarioTile
        |> Array.filter(fun st -> st.ScenarioId = scenarioId)

    let getUnits (scenarioId: int) =
        let assembly = IntrospectionExtensions.GetTypeInfo(typeof<App>).Assembly
        let stream = assembly.GetManifestResourceStream("scenariounit");
        let reader = new StreamReader(stream)
        let json = reader.ReadToEnd()
        let unit = UnitContext.Parse(json)
        unit.Dataroot.ScenarioUnit
        |> Array.filter(fun su -> su.ScenarioId = scenarioId)

    let getEquipments = 
        let assembly = IntrospectionExtensions.GetTypeInfo(typeof<App>).Assembly
        let stream = assembly.GetManifestResourceStream("equipment");
        let reader = new StreamReader(stream)
        let json = reader.ReadToEnd()
        let equipment = EquipmentContext.Parse(json)
        equipment.Dataroot.Equipment

    let createImage path =
        let image = new Image()
        image.Source <- ImageSource.FromResource(path)
        image

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
        new Rectangle(x,y,width,height)

    let addTileContent (layout:AbsoluteLayout) (contentData: ContentData) (scale: float) =
        let rectangle = createRectangle contentData.ColumnNumber contentData.RowNumber scale
        let terrainImageLocator = "tacmapdry" + contentData.TileId.ToString()
        layout.Children.Add(createImage terrainImageLocator, rectangle)
        match contentData.UnitId with 
        |Some i -> 
            let unitImageLocator = "tacicons" + i.ToString()
            layout.Children.Add(createImage unitImageLocator, rectangle)
        |None -> ()

    let populateBoard =
        let tiles = getTiles 0
        let units = getUnits 0
        let equipments = getEquipments
        let numberOfTiles = tiles |> Seq.length
        let scale = 1.5
        let layout = createLayout numberOfTiles scale
        tiles
        |> Array.map(fun t -> createTileContentData t units equipments)
        |> Array.iter(fun cd -> addTileContent layout cd scale)
        let scrollView = new ScrollView()
        scrollView.Orientation <- ScrollOrientation.Both
        scrollView.Content <- layout
        base.MainPage <- ContentPage(Content = scrollView)  

    do
        populateBoard