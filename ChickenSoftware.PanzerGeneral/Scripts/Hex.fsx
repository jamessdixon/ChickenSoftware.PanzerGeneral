
#r "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/packages/Xamarin.Forms.3.0.0.482510/lib/netstandard2.0/Xamarin.Forms.Core.dll"
#r "/Users/jamesdixon/GitHub/chickensoftware.xf/Droid/obj/Debug/linksrc/netstandard.dll"

#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Unit.fsx"
#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Nation.fsx"
#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Terrain.fsx"
#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Tile.fsx"

open Tile
open Terrain
open Xamarin.Forms

let getImage path tileId =
    let image = new Image()
    image.Source <- ImageSource.FromResource(path)
    image

let getTerrainFrame (tile: Tile) (scale:float) =
    let terrain = 
        match tile with 
        | Tile.Regular bt -> bt.Terrain
        | Tile.Victory vt -> vt.BaseTile.Terrain
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
    let image = getImage terrainImageLocator tileId
    image.Scale <- (scale + 0.6)
    frame.InputTransparent <- false
    frame.Content <- image
    frame

let getSupportingFrame =
    let frame = new Frame()
    frame.BackgroundColor <- Color.Transparent
    frame.BorderColor <- Color.Transparent
    frame.InputTransparent <- true
    frame

let getUnitFrame (tile: Tile) (scale:float) =
    Some getSupportingFrame

let getStrengthFrame (tile: Tile) (scale:float) =
    Some getSupportingFrame

let getNationFrame (tile: Tile) (scale:float) =
    Some getSupportingFrame

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

let createHex (tile: Tile) (scale:float) =
    let layout = new AbsoluteLayout()
    let rectangle = getRectangle 0 0 scale
    let terrainFrame = getTerrainFrame tile scale
    layout.Children.Add(terrainFrame,rectangle)
    let unitFrame = getUnitFrame tile scale
    match unitFrame with 
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






