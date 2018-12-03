
module ChickenSoftware.PanzerGeneral.Hex

open Tile
open Terrain
open UnitMapper
open Xamarin.Forms

let getImage path =
    let image = new Image()
    image.Source <- ImageSource.FromResource(path)
    image

let getRectangle columnIndex rowIndex scale =
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

let getTerrainFrame (baseTile: BaseTile) (scale:float) =
    let terrain = baseTile.Terrain
    let baseTerrain =    
        match terrain with 
        | Terrain.Sea bt -> bt
        | Terrain.Port bt -> bt
        | Terrain.Land l -> 
            match l with
            | Land.Airfield bt -> bt
            | Land.Bocage (bt,i) -> bt
            | Land.City bt -> bt
            | Land.Clear (bt,i) -> bt
            | Land.Desert bt -> bt
            | Land.Escarpment bt -> bt
            | Land.Forest bt -> bt
            | Land.Fortificaiton (bt,i) -> bt
            | Land.Mountain bt -> bt
            | Land.Rough (bt,i) -> bt
            | Land.RoughDesert bt -> bt
            | Land.Swamp bt -> bt
    let tileId = baseTerrain.Id
    let locatorPrefix = 
        match baseTerrain.Condition with 
        | LandCondition.Dry -> "tacmapdry"
        | LandCondition.Frozen -> "tacmapfrozen"
        | LandCondition.Muddy -> "tacmapmuddy"
    let frame = new Frame()
    let terrainImageLocator = locatorPrefix + tileId.ToString()
    let image = getImage terrainImageLocator
    image.Scale <- (scale + 0.6)
    frame.BackgroundColor <- Color.Transparent
    frame.BorderColor <- Color.Transparent
    frame.InputTransparent <- false
    frame.Content <- image
    frame
    
let getSingleUnitFrame (iconId:int) (scale:float) =
    let frame = new Frame()
    let path = "tacicons" + iconId.ToString()
    let image = getImage path
    image.Scale <- (scale + 0.6)
    frame.BackgroundColor <- Color.Transparent
    frame.BorderColor <- Color.Transparent
    frame.InputTransparent <- true
    frame.Content <- image
    Some frame

let getMultiUnitFrame (id:int)(scale:float) =
    let frame = new Frame()
    let path = "stackicn" + id.ToString()
    let image = getImage path
    image.Scale <- (scale + 0.6)
    frame.BackgroundColor <- Color.Transparent
    frame.BorderColor <- Color.Transparent
    frame.InputTransparent <- true
    frame.Content <- image
    Some frame
    
let getEarthUnitFrame (baseTile: BaseTile) (scale:float) =
    match (baseTile.EarthUnit), (baseTile.SkyUnit) with
    | Some eu, None -> 
        let equipmentId = getUnitIconId eu
        getSingleUnitFrame equipmentId scale
    //| Some eu, Some su -> getMultiUnitFrame eu.Id scale
    | _, _ -> None

let getSkyUnitFrame (baseTile: BaseTile) (scale:float) =
    match (baseTile.EarthUnit), (baseTile.SkyUnit) with
    | None, Some su -> 
        let equipmentId = getUnitIconId su
        getSingleUnitFrame equipmentId scale
    //| Some eu, Some su -> getMultiUnitFrame su.Id scale
    | _, _ -> None

let getStrengthFrame (tile: Tile) (scale:float) =
    let frame = new Frame()
    //let path = "tacicons" + iconId.ToString()
    //let image = getImage path
    //image.Scale <- (scale + 0.6)
    frame.BackgroundColor <- Color.Transparent
    frame.BorderColor <- Color.Transparent
    frame.InputTransparent <- true
    //frame.Content <- image
    Some frame

let getNationFrame (tile: Tile) (scale:float) =
    let frame = new Frame()
    //let path = "tacicons" + iconId.ToString()
    //let image = getImage path
    //image.Scale <- (scale + 0.6)
    frame.BackgroundColor <- Color.Transparent
    frame.BorderColor <- Color.Transparent
    frame.InputTransparent <- true
    //frame.Content <- image
    Some frame

let createHex (tile: Tile) (scale:float) =
    let layout = new AbsoluteLayout()
    let baseTile = getBaseTileFromTile tile
    let rectangle = getRectangle baseTile.ColumnNumber baseTile.RowNumber scale
    let terrainFrame = getTerrainFrame baseTile scale
    layout.Children.Add(terrainFrame,rectangle)
    let earthUnitFrame = getEarthUnitFrame baseTile scale
    match earthUnitFrame with 
    | Some f -> layout.Children.Add(f,rectangle)
    | None -> ()
    let skyUnitFrame = getSkyUnitFrame baseTile scale
    match skyUnitFrame with 
    | Some f -> layout.Children.Add(f,rectangle)
    | None -> ()
    let strengthFrame = getStrengthFrame tile scale
    match strengthFrame with 
    | Some f -> layout.Children.Add(f,rectangle)
    | None -> ()
    let nationFrame = getNationFrame tile scale
    match nationFrame with 
    | Some f -> layout.Children.Add(f,rectangle)
    | None -> ()
    layout


