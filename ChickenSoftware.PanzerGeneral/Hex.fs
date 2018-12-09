
module ChickenSoftware.PanzerGeneral.Hex

open Tile
open Terrain
open UnitMapper
open Xamarin.Forms

type TileFrame(tile:Tile) =
    inherit Frame()
    member this.Tile = tile

let tapRecognizer = new TapGestureRecognizer()

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

let getTerrainFrame (tile: Tile) (scale:float) =
    let baseTile = getBaseTile tile
    let baseTerrain = getBaseTerrainFromTerrain baseTile.Terrain
    let tileId = baseTerrain.Id
    let locatorPrefix = 
        match baseTerrain.Condition with 
        | LandCondition.Dry -> "tacmapdry"
        | LandCondition.Frozen -> "tacmapfrozen"
        | LandCondition.Muddy -> "tacmapmuddy"
    let frame = new TileFrame(tile)
    let terrainImageLocator = locatorPrefix + tileId.ToString()
    let image = getImage terrainImageLocator
    image.Scale <- (scale + 0.6)
    frame.BackgroundColor <- Color.Transparent
    frame.BorderColor <- Color.Transparent
    frame.Content <- image
    frame.GestureRecognizers.Add(tapRecognizer)
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

let createHex (layout:AbsoluteLayout) (tile: Tile) (scale:float) =
    let baseTile = getBaseTile tile
    let rectangle = getRectangle baseTile.ColumnNumber baseTile.RowNumber scale
    let terrainFrame = getTerrainFrame tile scale
    layout.Children.Add(terrainFrame,rectangle)
    let earthUnitFrame = getEarthUnitFrame baseTile scale
    match earthUnitFrame with 
    | Some f -> layout.Children.Add(f,rectangle)
    | None -> ()
    //let skyUnitFrame = getSkyUnitFrame baseTile scale
    //match skyUnitFrame with 
    //| Some f -> layout.Children.Add(f,rectangle)
    //| None -> ()
    //let strengthFrame = getStrengthFrame tile scale
    //match strengthFrame with 
    //| Some f -> layout.Children.Add(f,rectangle)
    //| None -> ()
    //let nationFrame = getNationFrame tile scale
    //match nationFrame with 
    //| Some f -> layout.Children.Add(f,rectangle)


