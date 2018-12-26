
module ChickenSoftware.PanzerGeneral.Scenario

open Tile
open System
open Nation

type Player =
| Human of Nation
| Computer of Nation

type Scenario = {CurrentPlayer: Player; 
                Board: Tile array; 
                ActiveTile: Tile option; 
                MovableTiles: Tile array option;
                AttackableTiles: Tile array option}

