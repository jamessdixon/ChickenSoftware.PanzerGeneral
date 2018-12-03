
#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Unit.fsx"
#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Nation.fsx"
#load "/Users/jamesdixon/GitHub/ChickenSoftware.PanzerGeneral/ChickenSoftware.PanzerGeneral/Terrain.fsx"

open Unit
open Nation
open Terrain

type BaseTile = {
    Terrain: Terrain; 
    Name: string; 
    Nation: Nation option; 
    EarthUnit: Unit option; 
    SkyUnit: Unit option }

type VictoryTile = {BaseTile:BaseTile; VictoryPoints: int}

type Tile =
| Regular of BaseTile
| Victory of VictoryTile

let terrain = Land (Land.City {Id=0; Condition = LandCondition.Dry})
let baseTile = {Terrain = terrain;Name="Test";Nation=None;EarthUnit=None;SkyUnit=None}
let tile = Tile.Regular baseTile

