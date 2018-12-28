
module ChickenSoftware.PanzerGeneral.Scenario

open Tile
open System
open Nation
open Terrain

type Player =
| Human of Nation
| Computer of Nation

type BoardMode =
| Earth
| Sky

type Scenario = {
                PlayerOne: Player;
                PlayerTwo: Player;
                CurrentPlayer: Player; 
                CurrentLandCondition: LandCondition;
                Board: Tile array; 
                BoardMode: BoardMode;
                ActiveTile: Tile option; 
                MovableTiles: Tile array option;
                AttackableTiles: Tile array option}

let updateMovableTiles (scenario:Scenario) =
    scenario
    